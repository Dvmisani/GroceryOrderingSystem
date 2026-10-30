using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace GroceryOrderingSystem
{
    public partial class AdminForm : Form
    {
        private TextBox txtNewItemID;
        private TextBox txtNewItemDesc;
        private TextBox txtNewItemPrice;

        public AdminForm()
        {
            InitializeComponent();
            SetupProgrammaticUI();
            LoadMembersGrid();
            LoadInventoryGrid();
        }

        // --- PROGRAMMATIC UI SETUP ---
        private void SetupProgrammaticUI()
        {
            Button btnUpdateMemberDetails = new Button()
            {
                Text = "Update Member",
                Location = new System.Drawing.Point(20, 450),
                Size = new System.Drawing.Size(120, 30)
            };
            btnUpdateMemberDetails.Click += btnUpdateMemberDetails_Click;
            this.Controls.Add(btnUpdateMemberDetails);

            txtNewItemID = new TextBox() { Location = new System.Drawing.Point(550, 340), Width = 80, Text = "Item ID" };
            txtNewItemDesc = new TextBox() { Location = new System.Drawing.Point(640, 340), Width = 150, Text = "Description" };
            txtNewItemPrice = new TextBox() { Location = new System.Drawing.Point(800, 340), Width = 80, Text = "Price" };

            Button btnAddNewItem = new Button()
            {
                Text = "Add New Item",
                Location = new System.Drawing.Point(890, 338),
                Size = new System.Drawing.Size(100, 30),
                BackColor = System.Drawing.Color.LightGreen
            };
            btnAddNewItem.Click += btnAddNewItem_Click;

            this.Controls.Add(txtNewItemID);
            this.Controls.Add(txtNewItemDesc);
            this.Controls.Add(txtNewItemPrice);
            this.Controls.Add(btnAddNewItem);
        }

        // --- LOAD DATA GRIDS ---
        private void LoadMembersGrid(string searchTerm = null)
        {
            using (MySqlConnection conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT MemberID, Name, Surname, Contact, Email, Active FROM tblMembers";
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += " WHERE MemberID LIKE @Term OR Name LIKE @Term OR Surname LIKE @Term";
                }
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@Term", "%" + searchTerm.Trim() + "%");
                }
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvMembers.DataSource = dt;
            }
        }

        // --- SEARCH RESIDENTS ---
        private void btnSearchMember_Click(object sender, EventArgs e)
        {
            LoadMembersGrid(txtSearchMember.Text);
        }

        private void btnClearMemberSearch_Click(object sender, EventArgs e)
        {
            txtSearchMember.Text = "";
            LoadMembersGrid();
        }

        private void LoadInventoryGrid(string searchTerm = null)
        {
            using (MySqlConnection conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT ItemID, Description, Price, Available FROM tblGroceryItems";
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += " WHERE ItemID LIKE @Term OR Description LIKE @Term";
                }
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@Term", "%" + searchTerm.Trim() + "%");
                }
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvInventory.DataSource = dt;
            }
        }

        // --- SEARCH GROCERY ITEMS ---
        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            LoadInventoryGrid(txtSearchItem.Text);
        }

        private void btnClearItemSearch_Click(object sender, EventArgs e)
        {
            txtSearchItem.Text = "";
            LoadInventoryGrid();
        }

        // --- ADD NEW MEMBER ---
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMemberID.Text) || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSurname.Text))
            {
                MessageBox.Show("MemberID, Name, and Surname fields are strictly mandatory.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = DatabaseManager.GetConnection())
            {
                string query = "INSERT INTO tblMembers (MemberID, Name, Surname, Contact, Email, Active) VALUES (@ID, @Name, @Surname, @Contact, @Email, 'Y')";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", txtMemberID.Text.Trim());
                    cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Surname", txtSurname.Text.Trim());
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Resident account initialized successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMembersGrid();
                    }
                    catch (MySqlException ex) when (ex.Number == 1062)
                    {
                        MessageBox.Show("Error: A member with this ID already exists.", "Data Duplication", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- TOGGLE MEMBER ACTIVE STATUS ---
        private void btnUpdateMemberStatus_Click(object sender, EventArgs e)
        {
            if (dgvMembers.CurrentRow == null)
            {
                MessageBox.Show("Please select a member to toggle their status.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string memberId = dgvMembers.CurrentRow.Cells["MemberID"].Value.ToString();
            string currentStatus = dgvMembers.CurrentRow.Cells["Active"].Value.ToString();
            string newStatus = (currentStatus == "Y") ? "N" : "Y";

            using (MySqlConnection conn = DatabaseManager.GetConnection())
            {
                string query = "UPDATE tblMembers SET Active = @Status WHERE MemberID = @ID";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@ID", memberId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    LoadMembersGrid();
                }
            }
        }

        // --- UPDATE EXISTING MEMBER ---
        private void btnUpdateMemberDetails_Click(object sender, EventArgs e)
        {
            if (dgvMembers.CurrentRow == null) return;

            string memberId = dgvMembers.CurrentRow.Cells["MemberID"].Value.ToString();

            if (string.IsNullOrWhiteSpace(txtContact.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Contact and Email cannot be blank when updating a member profile.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = DatabaseManager.GetConnection())
            {
                string query = "UPDATE tblMembers SET Contact = @Contact, Email = @Email WHERE MemberID = @ID";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID", memberId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Member contact details updated successfully.", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMembersGrid();
                }
            }
        }

        // --- UPDATE ITEM PRICE ---
        private void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow == null || !decimal.TryParse(txtNewPrice.Text, out decimal updatedPrice) || updatedPrice < 0)
            {
                MessageBox.Show("Please enter a valid positive numerical price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedItemID = dgvInventory.CurrentRow.Cells["ItemID"].Value.ToString();
            using (MySqlConnection conn = DatabaseManager.GetConnection())
            {
                string query = "UPDATE tblGroceryItems SET Price = @Price WHERE ItemID = @ItemID";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Price", updatedPrice);
                    cmd.Parameters.AddWithValue("@ItemID", selectedItemID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    LoadInventoryGrid();
                }
            }
        }

        // --- ADD NEW GROCERY ITEM ---
        private void btnAddNewItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewItemID.Text) || txtNewItemID.Text == "Item ID" ||
                string.IsNullOrWhiteSpace(txtNewItemDesc.Text) || txtNewItemDesc.Text == "Description" ||
                !decimal.TryParse(txtNewItemPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Please provide a valid Item ID, Description, and a positive numerical Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = DatabaseManager.GetConnection())
            {
                string query = "INSERT INTO tblGroceryItems (ItemID, Description, Price, Available) VALUES (@ID, @Desc, @Price, 'Y')";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", txtNewItemID.Text.Trim());
                    cmd.Parameters.AddWithValue("@Desc", txtNewItemDesc.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", price);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("New grocery item added to the catalog.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNewItemID.Text = "";
                        txtNewItemDesc.Text = "";
                        txtNewItemPrice.Text = "";
                        LoadInventoryGrid();
                    }
                    catch (MySqlException ex) when (ex.Number == 1062)
                    {
                        MessageBox.Show("Error: An item with this ID already exists.", "Duplicate Key", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // =====================================================================
        // Q1: CONSOLIDATED WEEKLY ORDER LIST
        // =====================================================================
        private void btnWeeklyOrder_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dtpWeeklyOrder.Value.Date;

            if (selectedDate.DayOfWeek != DayOfWeek.Friday)
            {
                MessageBox.Show(
                    "Please select a valid Friday date. Orders are only processed on Fridays.",
                    "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT 
                            gi.ItemID,
                            gi.Description,
                            SUM(mo.Quantity) AS TotalQuantity,
                            gi.Price AS UnitPrice,
                            SUM(mo.TotalPrice) AS TotalCost
                        FROM tblMemberOrders mo
                        INNER JOIN tblGroceryItems gi ON mo.ItemID = gi.ItemID
                        WHERE mo.OrderDate = @SelectedDate
                        GROUP BY gi.ItemID, gi.Description, gi.Price
                        ORDER BY TotalQuantity DESC";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@SelectedDate", selectedDate);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show($"No orders found for {selectedDate:dd MMMM yyyy}.",
                            "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    ShowWeeklyOrderReport(dt, selectedDate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowWeeklyOrderReport(DataTable dt, DateTime friday)
        {
            Form reportForm = new Form();
            reportForm.Text = $"Weekly Consolidated Order — {friday:dd MMMM yyyy}";
            reportForm.Size = new System.Drawing.Size(700, 530);
            reportForm.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label();
            lblTitle.Text = $"Consolidated Weekly Grocery Order — Processing Date: {friday:dd MMMM yyyy}";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(10, 10);
            lblTitle.AutoSize = true;
            reportForm.Controls.Add(lblTitle);

            DataGridView dgv = new DataGridView();
            dgv.Location = new System.Drawing.Point(10, 40);
            dgv.Size = new System.Drawing.Size(660, 370);
            dgv.DataSource = dt;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            reportForm.Controls.Add(dgv);

            decimal grandTotal = 0;
            int grandQty = 0;
            foreach (DataRow row in dt.Rows)
            {
                grandTotal += Convert.ToDecimal(row["TotalCost"]);
                grandQty += Convert.ToInt32(row["TotalQuantity"]);
            }

            Label lblTotals = new Label();
            lblTotals.Text = $"Total Product Lines: {dt.Rows.Count}   |   " +
                             $"Total Units: {grandQty}   |   " +
                             $"Grand Total: R{grandTotal:F2}";
            lblTotals.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblTotals.Location = new System.Drawing.Point(10, 420);
            lblTotals.AutoSize = true;
            reportForm.Controls.Add(lblTotals);

            Button btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new System.Drawing.Point(590, 450);
            btnClose.Size = new System.Drawing.Size(80, 30);
            btnClose.Click += (s, ev) => reportForm.Close();
            reportForm.Controls.Add(btnClose);

            reportForm.ShowDialog();
        }

        // =====================================================================
        // Q2: INDIVIDUAL MEMBER ORDER SUMMARIES + PDF EXPORT
        // =====================================================================
        private void btnMemberSummary_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dtpMemberSummary.Value.Date;

            if (selectedDate.DayOfWeek != DayOfWeek.Friday)
            {
                MessageBox.Show(
                    "Please select a valid Friday date. Orders are only processed on Fridays.",
                    "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT 
                            m.MemberID,
                            m.Name,
                            m.Surname,
                            gi.Description,
                            mo.Quantity,
                            mo.TotalPrice
                        FROM tblMemberOrders mo
                        INNER JOIN tblMembers m ON mo.MemberID = m.MemberID
                        INNER JOIN tblGroceryItems gi ON mo.ItemID = gi.ItemID
                        WHERE mo.OrderDate = @SelectedDate
                        ORDER BY m.MemberID, gi.Description";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@SelectedDate", selectedDate);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show($"No member orders found for {selectedDate:dd MMMM yyyy}.",
                            "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    ShowMemberSummaryReport(dt, selectedDate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating member summaries: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowMemberSummaryReport(DataTable dt, DateTime friday)
        {
            Form reportForm = new Form();
            reportForm.Text = $"Individual Member Order Summaries — {friday:dd MMMM yyyy}";
            reportForm.Size = new System.Drawing.Size(750, 580);
            reportForm.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label();
            lblTitle.Text = $"Individual Member Order Summaries — {friday:dd MMMM yyyy}";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(10, 10);
            lblTitle.AutoSize = true;
            reportForm.Controls.Add(lblTitle);

            DataGridView dgv = new DataGridView();
            dgv.Location = new System.Drawing.Point(10, 40);
            dgv.Size = new System.Drawing.Size(715, 420);
            dgv.DataSource = dt;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            reportForm.Controls.Add(dgv);

            Button btnExportPdf = new Button();
            btnExportPdf.Text = "Export to PDF";
            btnExportPdf.Location = new System.Drawing.Point(10, 475);
            btnExportPdf.Size = new System.Drawing.Size(130, 35);
            btnExportPdf.BackColor = System.Drawing.Color.LightSalmon;
            btnExportPdf.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnExportPdf.Click += (s, ev) => ExportMemberSummaryToPdf(dt, friday);
            reportForm.Controls.Add(btnExportPdf);

            Button btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new System.Drawing.Point(640, 475);
            btnClose.Size = new System.Drawing.Size(80, 35);
            btnClose.Click += (s, ev) => reportForm.Close();
            reportForm.Controls.Add(btnClose);

            reportForm.ShowDialog();
        }

        // --- Simple footer that stamps "Page X of Y" on every page ---
        private class PdfPageFooter : PdfPageEventHelper
        {
            private Font footerFont = FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.GRAY);

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                PdfContentByte cb = writer.DirectContent;
                Phrase footer = new Phrase($"Page {document.PageNumber}", footerFont);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, footer,
                    (document.Left + document.Right) / 2, document.Bottom - 10, 0);
            }
        }

        private void ExportMemberSummaryToPdf(DataTable dt, DateTime friday)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveDialog.FileName = $"MemberOrderSummaries_{friday:yyyy-MM-dd}.pdf";
            saveDialog.Title = "Save Member Order Summaries PDF";

            if (saveDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                Document pdfDoc = new Document(PageSize.A4, 40f, 40f, 50f, 50f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(saveDialog.FileName, FileMode.Create));
                writer.PageEvent = new PdfPageFooter();
                pdfDoc.Open();

                // --- Palette: swap these two colours to re-theme the whole document ---
                BaseColor brandColor = new BaseColor(41, 84, 122);      // header / accent colour
                BaseColor altRowColor = new BaseColor(240, 245, 250);   // light stripe for alternating rows

                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, brandColor);
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.DARK_GRAY);
                Font bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 9);
                Font memberFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, brandColor);
                Font totalFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.BLACK);
                Font whiteFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);
                Font smallGray = FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.GRAY);

                Paragraph docTitle = new Paragraph("Grocery Ordering and Management System", titleFont);
                docTitle.Alignment = Element.ALIGN_CENTER;
                pdfDoc.Add(docTitle);

                // Thin coloured rule under the title for a bit of visual identity
                PdfPTable ruleTable = new PdfPTable(1);
                ruleTable.WidthPercentage = 100;
                ruleTable.SpacingBefore = 4f;
                ruleTable.SpacingAfter = 10f;
                PdfPCell ruleCell = new PdfPCell();
                ruleCell.FixedHeight = 3f;
                ruleCell.BackgroundColor = brandColor;
                ruleCell.Border = Rectangle.NO_BORDER;
                ruleTable.AddCell(ruleCell);
                pdfDoc.Add(ruleTable);

                Paragraph subTitle = new Paragraph(
                    $"Individual Member Order Summaries — {friday:dd MMMM yyyy}", headerFont);
                subTitle.Alignment = Element.ALIGN_CENTER;
                subTitle.SpacingAfter = 6f;
                pdfDoc.Add(subTitle);

                Paragraph generated = new Paragraph(
                    $"Generated: {DateTime.Now:dd MMM yyyy HH:mm}   |   " +
                    $"Student: Dumisani Abrahm Baloyi   |   No: 10715096", smallGray);
                generated.Alignment = Element.ALIGN_CENTER;
                generated.SpacingAfter = 20f;
                pdfDoc.Add(generated);

                string currentMember = "";
                PdfPTable memberTable = null;
                decimal memberTotal = 0;
                int rowIndex = 0;

                foreach (DataRow row in dt.Rows)
                {
                    string memberId = row["MemberID"].ToString();
                    string fullName = $"{row["Name"]} {row["Surname"]}";

                    if (memberId != currentMember)
                    {
                        if (memberTable != null)
                        {
                            PdfPCell totalLabelCell = new PdfPCell(new Phrase($"TOTAL FOR {currentMember}", totalFont));
                            totalLabelCell.Colspan = 2;
                            totalLabelCell.BackgroundColor = new BaseColor(225, 230, 235);
                            totalLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            totalLabelCell.Padding = 6f;
                            memberTable.AddCell(totalLabelCell);

                            PdfPCell totalValueCell = new PdfPCell(new Phrase($"R{memberTotal:F2}", totalFont));
                            totalValueCell.BackgroundColor = new BaseColor(225, 230, 235);
                            totalValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            totalValueCell.Padding = 6f;
                            memberTable.AddCell(totalValueCell);

                            pdfDoc.Add(memberTable);
                            pdfDoc.Add(new Paragraph(" "));
                            memberTotal = 0;
                        }

                        // Member heading with a light background band instead of plain text
                        PdfPTable headingBand = new PdfPTable(1);
                        headingBand.WidthPercentage = 100;
                        headingBand.SpacingBefore = 12f;
                        headingBand.SpacingAfter = 4f;
                        PdfPCell headingCell = new PdfPCell(new Phrase($"Member: {memberId} — {fullName}", memberFont));
                        headingCell.BackgroundColor = altRowColor;
                        headingCell.Border = Rectangle.NO_BORDER;
                        headingCell.Padding = 6f;
                        headingBand.AddCell(headingCell);
                        pdfDoc.Add(headingBand);

                        memberTable = new PdfPTable(3);
                        memberTable.WidthPercentage = 100;
                        memberTable.SetWidths(new float[] { 50f, 20f, 30f });

                        PdfPCell h1 = new PdfPCell(new Phrase("Item Description", whiteFont));
                        PdfPCell h2 = new PdfPCell(new Phrase("Quantity", whiteFont));
                        PdfPCell h3 = new PdfPCell(new Phrase("Total Price (R)", whiteFont));
                        h1.BackgroundColor = brandColor;
                        h2.BackgroundColor = brandColor;
                        h3.BackgroundColor = brandColor;
                        h1.Padding = 5f;
                        h2.Padding = 5f;
                        h3.Padding = 5f;
                        h2.HorizontalAlignment = Element.ALIGN_CENTER;
                        h3.HorizontalAlignment = Element.ALIGN_RIGHT;
                        memberTable.AddCell(h1);
                        memberTable.AddCell(h2);
                        memberTable.AddCell(h3);

                        currentMember = memberId;
                        rowIndex = 0;
                    }

                    decimal lineTotal = Convert.ToDecimal(row["TotalPrice"]);
                    memberTotal += lineTotal;

                    BaseColor rowColor = (rowIndex % 2 == 0) ? BaseColor.WHITE : altRowColor;

                    PdfPCell c1 = new PdfPCell(new Phrase(row["Description"].ToString(), bodyFont));
                    PdfPCell c2 = new PdfPCell(new Phrase(row["Quantity"].ToString(), bodyFont));
                    PdfPCell c3 = new PdfPCell(new Phrase($"R{lineTotal:F2}", bodyFont));
                    c1.BackgroundColor = rowColor;
                    c2.BackgroundColor = rowColor;
                    c3.BackgroundColor = rowColor;
                    c1.Padding = 5f;
                    c2.Padding = 5f;
                    c3.Padding = 5f;
                    c2.HorizontalAlignment = Element.ALIGN_CENTER;
                    c3.HorizontalAlignment = Element.ALIGN_RIGHT;
                    memberTable.AddCell(c1);
                    memberTable.AddCell(c2);
                    memberTable.AddCell(c3);

                    rowIndex++;
                }

                if (memberTable != null)
                {
                    PdfPCell totalLabelCell = new PdfPCell(new Phrase($"TOTAL FOR {currentMember}", totalFont));
                    totalLabelCell.Colspan = 2;
                    totalLabelCell.BackgroundColor = new BaseColor(225, 230, 235);
                    totalLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    totalLabelCell.Padding = 6f;
                    memberTable.AddCell(totalLabelCell);

                    PdfPCell totalValueCell = new PdfPCell(new Phrase($"R{memberTotal:F2}", totalFont));
                    totalValueCell.BackgroundColor = new BaseColor(225, 230, 235);
                    totalValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    totalValueCell.Padding = 6f;
                    memberTable.AddCell(totalValueCell);

                    pdfDoc.Add(memberTable);
                }

                pdfDoc.Close();

                MessageBox.Show($"PDF saved successfully to:\n{saveDialog.FileName}",
                    "PDF Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                System.Diagnostics.Process.Start(saveDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating PDF: " + ex.Message,
                    "PDF Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // =====================================================================
        // Q3: MIS REPORT — ITEM ORDERING TRENDS
        // =====================================================================
        private void btnItemTrends_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtpTrendsFrom.Value.Date;
            DateTime toDate = dtpTrendsTo.Value.Date;

            if (fromDate > toDate)
            {
                MessageBox.Show(
                    "The From date cannot be later than the To date. Please correct the date range.",
                    "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((toDate - fromDate).TotalDays > 365)
            {
                MessageBox.Show(
                    "Date range cannot exceed 12 months. Please narrow your selection.",
                    "Date Range Too Wide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT 
                            gi.ItemID,
                            gi.Description,
                            COUNT(DISTINCT mo.OrderDate) AS WeeksOrdered,
                            SUM(mo.Quantity)             AS TotalUnits,
                            SUM(mo.TotalPrice)           AS TotalRevenue,
                            ROUND(AVG(mo.Quantity), 1)   AS AvgUnitsPerOrder
                        FROM tblMemberOrders mo
                        INNER JOIN tblGroceryItems gi ON mo.ItemID = gi.ItemID
                        WHERE mo.OrderDate BETWEEN @FromDate AND @ToDate
                        GROUP BY gi.ItemID, gi.Description
                        ORDER BY TotalUnits DESC";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    adapter.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show(
                            $"No order data found between {fromDate:dd MMM yyyy} and {toDate:dd MMM yyyy}.",
                            "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    ShowItemTrendsReport(dt, fromDate, toDate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating item trends report: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowItemTrendsReport(DataTable dt, DateTime from, DateTime to)
        {
            Form reportForm = new Form();
            reportForm.Text = $"MIS Report — Item Trends ({from:dd MMM yyyy} to {to:dd MMM yyyy})";
            reportForm.Size = new System.Drawing.Size(820, 620);
            reportForm.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label();
            lblTitle.Text = "MIS Report: Item Ordering Trends";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(10, 10);
            lblTitle.AutoSize = true;
            reportForm.Controls.Add(lblTitle);

            Label lblRange = new Label();
            lblRange.Text = $"Date Range: {from:dd MMMM yyyy} to {to:dd MMMM yyyy}   |   Items analysed: {dt.Rows.Count}";
            lblRange.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblRange.Location = new System.Drawing.Point(10, 35);
            lblRange.AutoSize = true;
            reportForm.Controls.Add(lblRange);

            Label lblHighlight = new Label();
            lblHighlight.Text = $"Most Ordered: {dt.Rows[0]["Description"]}   |   " +
                                $"Least Ordered: {dt.Rows[dt.Rows.Count - 1]["Description"]}";
            lblHighlight.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblHighlight.ForeColor = System.Drawing.Color.DarkBlue;
            lblHighlight.Location = new System.Drawing.Point(10, 58);
            lblHighlight.AutoSize = true;
            reportForm.Controls.Add(lblHighlight);

            DataGridView dgv = new DataGridView();
            dgv.Location = new System.Drawing.Point(10, 85);
            dgv.Size = new System.Drawing.Size(785, 420);
            dgv.DataSource = dt;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dgv.DataBindingComplete += (s, ev) =>
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (i < 3)
                        dgv.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                    else if (i >= dgv.Rows.Count - 3)
                        dgv.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                }
            };
            reportForm.Controls.Add(dgv);

            Label lblLegend = new Label();
            lblLegend.Text = "Green = Top 3 most ordered   |   Red = Bottom 3 least ordered";
            lblLegend.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            lblLegend.Location = new System.Drawing.Point(10, 515);
            lblLegend.AutoSize = true;
            reportForm.Controls.Add(lblLegend);

            Button btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new System.Drawing.Point(710, 540);
            btnClose.Size = new System.Drawing.Size(80, 30);
            btnClose.Click += (s, ev) => reportForm.Close();
            reportForm.Controls.Add(btnClose);

            reportForm.ShowDialog();
        }

        // =====================================================================
        // Q4: MIS REPORT — MEMBER PARTICIPATION
        // =====================================================================
        private void btnParticipation_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtpPartFrom.Value.Date;
            DateTime toDate = dtpPartTo.Value.Date;

            if (fromDate > toDate)
            {
                MessageBox.Show(
                    "The From date cannot be later than the To date. Please correct the date range.",
                    "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((toDate - fromDate).TotalDays > 365)
            {
                MessageBox.Show(
                    "Date range cannot exceed 12 months. Please narrow your selection.",
                    "Date Range Too Wide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = DatabaseManager.GetConnection())
                {
                    // Count total available Fridays in the range
                    string fridayCountQuery = @"
                        SELECT COUNT(DISTINCT OrderDate) AS TotalFridays
                        FROM tblMemberOrders
                        WHERE OrderDate BETWEEN @FromDate AND @ToDate";

                    MySqlCommand fridayCmd = new MySqlCommand(fridayCountQuery, conn);
                    fridayCmd.Parameters.AddWithValue("@FromDate", fromDate);
                    fridayCmd.Parameters.AddWithValue("@ToDate", toDate);
                    conn.Open();
                    int totalFridays = Convert.ToInt32(fridayCmd.ExecuteScalar());
                    conn.Close();

                    if (totalFridays == 0)
                    {
                        MessageBox.Show(
                            $"No order data found between {fromDate:dd MMM yyyy} and {toDate:dd MMM yyyy}.",
                            "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Get participation per member
                    string query = @"
                        SELECT 
                            m.MemberID,
                            m.Name,
                            m.Surname,
                            m.Active,
                            COUNT(DISTINCT mo.OrderDate)                        AS WeeksParticipated,
                            ROUND(COUNT(DISTINCT mo.OrderDate) * 100.0 / @TotalFridays, 1) AS ParticipationRate,
                            SUM(mo.TotalPrice)                                  AS TotalSpend
                        FROM tblMembers m
                        LEFT JOIN tblMemberOrders mo 
                            ON m.MemberID = mo.MemberID 
                            AND mo.OrderDate BETWEEN @FromDate AND @ToDate
                        GROUP BY m.MemberID, m.Name, m.Surname, m.Active
                        ORDER BY WeeksParticipated DESC, m.MemberID";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@TotalFridays", totalFridays);
                    adapter.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    adapter.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ShowParticipationReport(dt, fromDate, toDate, totalFridays);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating participation report: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowParticipationReport(DataTable dt, DateTime from, DateTime to, int totalFridays)
        {
            Form reportForm = new Form();
            reportForm.Text = $"MIS Report — Member Participation ({from:dd MMM yyyy} to {to:dd MMM yyyy})";
            reportForm.Size = new System.Drawing.Size(860, 660);
            reportForm.StartPosition = FormStartPosition.CenterScreen;

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "MIS Report: Member Participation Analysis";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(10, 10);
            lblTitle.AutoSize = true;
            reportForm.Controls.Add(lblTitle);

            // Date range and summary
            int activeParticipants = 0;
            int neverOrdered = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt32(row["WeeksParticipated"]) > 0) activeParticipants++;
                else neverOrdered++;
            }

            Label lblRange = new Label();
            lblRange.Text = $"Date Range: {from:dd MMMM yyyy} to {to:dd MMMM yyyy}   |   " +
                            $"Total Fridays: {totalFridays}   |   " +
                            $"Total Members: {dt.Rows.Count}";
            lblRange.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblRange.Location = new System.Drawing.Point(10, 35);
            lblRange.AutoSize = true;
            reportForm.Controls.Add(lblRange);

            Label lblSummary = new Label();
            lblSummary.Text = $"Active Participants: {activeParticipants}   |   " +
                              $"Never Ordered: {neverOrdered}   |   " +
                              $"Participation Rate: {Math.Round(activeParticipants * 100.0 / dt.Rows.Count, 1)}%";
            lblSummary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblSummary.ForeColor = System.Drawing.Color.DarkGreen;
            lblSummary.Location = new System.Drawing.Point(10, 58);
            lblSummary.AutoSize = true;
            reportForm.Controls.Add(lblSummary);

            // Main grid
            DataGridView dgv = new DataGridView();
            dgv.Location = new System.Drawing.Point(10, 85);
            dgv.Size = new System.Drawing.Size(825, 460);
            dgv.DataSource = dt;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // Colour code by participation rate
            dgv.DataBindingComplete += (s, ev) =>
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells["WeeksParticipated"].Value == null) continue;
                    int weeks = Convert.ToInt32(row.Cells["WeeksParticipated"].Value);
                    double rate = totalFridays > 0 ? weeks * 100.0 / totalFridays : 0;

                    if (rate >= 75)
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                    else if (rate >= 40)
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                    else if (rate == 0)
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                }
            };
            reportForm.Controls.Add(dgv);

            // Legend
            Label lblLegend = new Label();
            lblLegend.Text = "Green = 75%+ participation   |   Yellow = 40-74%   |   Red = Never ordered";
            lblLegend.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            lblLegend.Location = new System.Drawing.Point(10, 555);
            lblLegend.AutoSize = true;
            reportForm.Controls.Add(lblLegend);

            Button btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new System.Drawing.Point(755, 580);
            btnClose.Size = new System.Drawing.Size(80, 30);
            btnClose.Click += (s, ev) => reportForm.Close();
            reportForm.Controls.Add(btnClose);

            reportForm.ShowDialog();
        }
    }
}
