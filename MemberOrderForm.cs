using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace GroceryOrderingSystem
{
    public partial class MemberOrderForm : Form
    {
        private string ActiveMemberID;
        private string ActiveMemberFullName;
        private Dictionary<string, int> ProposedOrderLines = new Dictionary<string, int>();
        private DateTime DesignatedFridayCycle;
        private bool IsEditingExistingOrder = false;

        public MemberOrderForm(string memberId)
        {
            InitializeComponent();
            ActiveMemberID = memberId;
            this.Load += MemberOrderForm_Load;
        }

        // --- ENFORCING THE ORDERING WINDOW & FRIDAY CYCLE ---
        private bool IsOrderingWindowOpen()
        {
            DateTime today = DateTime.Today;
            int currentDay = (int)today.DayOfWeek; // Sunday = 0, Monday = 1, ..., Thursday = 4

            if (currentDay == 0 || currentDay > 4)
            {
                MessageBox.Show("The ordering window is currently closed. Orders can only be placed Monday through Thursday.",
                                "Window Closed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            int daysUntilFriday = ((int)DayOfWeek.Friday - currentDay + 7) % 7;
            DesignatedFridayCycle = today.AddDays(daysUntilFriday);
            return true;
        }

        private void MemberOrderForm_Load(object sender, EventArgs e)
        {
            if (!IsOrderingWindowOpen())
            {
                this.BeginInvoke(new Action(this.Close));
            }
            else
            {
                lblTargetCycle.Text = $"Target Friday Cycle: {DesignatedFridayCycle:yyyy-MM-dd}";
                LoadMemberDetails();
                LoadAvailableCatalog();
                LoadExistingOrderIfAny();
            }
        }

        // --- LOAD MEMBER DETAIL (date, member detail requirement) ---
        private void LoadMemberDetails()
        {
            using (MySqlConnection conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT Name, Surname FROM tblMembers WHERE MemberID = @ID";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", ActiveMemberID);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ActiveMemberFullName = $"{reader["Name"]} {reader["Surname"]}";
                        }
                    }
                }
            }
            lblMemberDetail.Text = $"Member: {ActiveMemberID} - {ActiveMemberFullName}";
        }

        // --- LOAD ANY EXISTING ORDER FOR THIS MEMBER + FRIDAY (retrieval + Update support) ---
        private void LoadExistingOrderIfAny()
        {
            ProposedOrderLines.Clear();

            using (MySqlConnection conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT ItemID, Quantity FROM tblMemberOrders WHERE MemberID = @ID AND OrderDate = @OrdDate";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", ActiveMemberID);
                    cmd.Parameters.AddWithValue("@OrdDate", DesignatedFridayCycle);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProposedOrderLines[reader["ItemID"].ToString()] = Convert.ToInt32(reader["Quantity"]);
                        }
                    }
                }
            }

            IsEditingExistingOrder = ProposedOrderLines.Count > 0;
            btnFinalizeSubmission.Text = IsEditingExistingOrder
                ? "Update Existing Order"
                : "Finalize & Submit Weekly Order";

            RefreshOrderSummary();
        }

        // --- CATALOG LOADING ---
        private void LoadAvailableCatalog()
        {
            using (MySqlConnection conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT ItemID, Description, Price FROM tblGroceryItems WHERE Available = 'Y'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvCatalog.DataSource = dt;
            }
        }

        // --- HELPER METHOD: FETCH ITEM PRICE ---
        private decimal GetCatalogPrice(string itemID)
        {
            decimal price = 0;
            using (MySqlConnection conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT Price FROM tblGroceryItems WHERE ItemID = @ItemID";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ItemID", itemID);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        price = Convert.ToDecimal(result);
                    }
                }
            }
            return price;
        }

        // --- ADDING ITEMS TO BASKET ---
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (dgvCatalog.CurrentRow == null || !int.TryParse(txtQuantity.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Please select an item and provide a valid quantity.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string itemID = dgvCatalog.CurrentRow.Cells["ItemID"].Value.ToString();

            if (!ProposedOrderLines.ContainsKey(itemID) && ProposedOrderLines.Count >= 12)
            {
                MessageBox.Show("Business Rule Violation: Maximum 12 distinct items allowed.",
                                "Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProposedOrderLines[itemID] = qty;
            RefreshOrderSummary();
        }

        // --- BASKET CALCULATION ---
        private void RefreshOrderSummary()
        {
            lstBasketSummary.Items.Clear();
            decimal runningTotal = 0;

            foreach (var line in ProposedOrderLines)
            {
                decimal unitPrice = GetCatalogPrice(line.Key);
                decimal cost = unitPrice * line.Value;
                runningTotal += cost;
                lstBasketSummary.Items.Add($"{line.Key} | Qty: {line.Value} | R{cost:F2}");
            }
            lblRunningTotal.Text = $"Total: R{runningTotal:F2}";
        }

        // --- FINAL SUBMISSION (TRANSACTION) ---
        private void btnFinalizeSubmission_Click(object sender, EventArgs e)
        {
            if (ProposedOrderLines.Count == 0) return;

            using (MySqlConnection conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // If this member already has an order for this Friday, clear the old
                        // lines first so the new selections replace them (Update behaviour).
                        // If no prior order exists, this DELETE simply affects zero rows,
                        // and the inserts below behave as a fresh Add.
                        string deleteQuery = "DELETE FROM tblMemberOrders WHERE MemberID = @MemID AND OrderDate = @OrdDate";
                        using (MySqlCommand delCmd = new MySqlCommand(deleteQuery, conn, trans))
                        {
                            delCmd.Parameters.AddWithValue("@MemID", ActiveMemberID);
                            delCmd.Parameters.AddWithValue("@OrdDate", DesignatedFridayCycle);
                            delCmd.ExecuteNonQuery();
                        }

                        foreach (var line in ProposedOrderLines)
                        {
                            decimal price = GetCatalogPrice(line.Key);
                            string query = "INSERT INTO tblMemberOrders (MemberID, OrderDate, ItemID, Quantity, TotalPrice) VALUES (@MemID, @OrdDate, @ItmID, @Qty, @TotPrice)";

                            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@MemID", ActiveMemberID);
                                cmd.Parameters.AddWithValue("@OrdDate", DesignatedFridayCycle);
                                cmd.Parameters.AddWithValue("@ItmID", line.Key);
                                cmd.Parameters.AddWithValue("@Qty", line.Value);
                                cmd.Parameters.AddWithValue("@TotPrice", price * line.Value);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        trans.Commit();

                        string message = IsEditingExistingOrder
                            ? "Order updated successfully!"
                            : "Order submitted successfully!";
                        MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ProposedOrderLines.Clear();
                        IsEditingExistingOrder = false;
                        btnFinalizeSubmission.Text = "Finalize & Submit Weekly Order";
                        RefreshOrderSummary();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show($"Error saving order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}