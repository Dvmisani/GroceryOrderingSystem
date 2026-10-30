using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace GroceryOrderingSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupLoginScreen();
        }

        private void SetupLoginScreen()
        {
            this.Text = "Old-Age Home Login Portal";
            this.Size = new System.Drawing.Size(450, 350);

            Label lblTitle = new Label()
            {
                Text = "Grocery Ordering System",
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                AutoSize = true,
                Location = new System.Drawing.Point(100, 20)
            };

            // Phase 2 Requirement: Password-protected administrative access
            Label lblAdminPwd = new Label()
            {
                Text = "Matron Password:",
                Location = new System.Drawing.Point(50, 80),
                AutoSize = true
            };

            TextBox txtAdminPwd = new TextBox()
            {
                PasswordChar = '*',
                Location = new System.Drawing.Point(160, 77),
                Size = new System.Drawing.Size(120, 20)
            };

            Button btnAdmin = new Button()
            {
                Text = "Login as Matron (Admin)",
                Location = new System.Drawing.Point(100, 110),
                Size = new System.Drawing.Size(180, 40)
            };

            btnAdmin.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtAdminPwd.Text))
                {
                    MessageBox.Show("Please enter the Matron password.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ValidateMatronLogin(txtAdminPwd.Text))
                {
                    AdminForm adminView = new AdminForm();
                    adminView.Show();
                }
                else
                {
                    MessageBox.Show("Invalid password. Access denied.",
                        "Security Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAdminPwd.Clear();
                    txtAdminPwd.Focus();
                }
            };

            Button btnMember = new Button()
            {
                Text = "Login as Resident",
                Location = new System.Drawing.Point(100, 170),
                Size = new System.Drawing.Size(180, 40)
            };

            btnMember.Click += (s, e) =>
            {
                MemberOrderForm memberView = new MemberOrderForm("01-01");
                memberView.Show();
            };

            // Phase 2 Requirement: About Section identifying student-developed solution
            Button btnAbout = new Button()
            {
                Text = "About System",
                Location = new System.Drawing.Point(100, 230),
                Size = new System.Drawing.Size(180, 30)
            };

            btnAbout.Click += (s, e) =>
            {
                MessageBox.Show(
                    "Grocery Ordering and Management System\n\n" +
                    "Developed by: Dumisani Abrahm Baloyi\n" +
                    "Student no: 10715096\n" +
                    "Module: ICT3715\n\n" +
                    "Purpose: To manage resident grocery orders and " +
                    "administration securely through role-based access.",
                    "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblAdminPwd);
            this.Controls.Add(txtAdminPwd);
            this.Controls.Add(btnAdmin);
            this.Controls.Add(btnMember);
            this.Controls.Add(btnAbout);
        }

        // --- Database-driven password validation ---
        private bool ValidateMatronLogin(string plainPassword)
        {
            // Hash the entered password using SHA-256 to match tblUsers
            string hashedPassword = HashPassword(plainPassword);

            try
            {
                using (MySqlConnection conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM tblUsers " +
                                   "WHERE Username = @user AND Password = @pass";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", "matron");
                        cmd.Parameters.AddWithValue("@pass", hashedPassword);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error during login: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // --- SHA-256 hash — matches MySQL's SHA2(value, 256) ---
        private string HashPassword(string plainText)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}