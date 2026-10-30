namespace GroceryOrderingSystem
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblMembersHeader;
        private System.Windows.Forms.DataGridView dgvMembers;
        private System.Windows.Forms.TextBox txtSearchMember;
        private System.Windows.Forms.Button btnSearchMember;
        private System.Windows.Forms.Button btnClearMemberSearch;
        private System.Windows.Forms.TextBox txtMemberID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblMemID;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Button btnUpdateMemberStatus;
        private System.Windows.Forms.Label lblInventoryHeader;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.TextBox txtSearchItem;
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.Button btnClearItemSearch;
        private System.Windows.Forms.TextBox txtNewPrice;
        private System.Windows.Forms.Label lblNewPrice;
        private System.Windows.Forms.Button btnUpdatePrice;
        // Q1 controls
        private System.Windows.Forms.Button btnWeeklyOrder;
        private System.Windows.Forms.DateTimePicker dtpWeeklyOrder;
        private System.Windows.Forms.Label lblWeeklyOrder;
        // Q2 controls
        private System.Windows.Forms.Button btnMemberSummary;
        private System.Windows.Forms.DateTimePicker dtpMemberSummary;
        private System.Windows.Forms.Label lblMemberSummary;
        // Q3 controls
        private System.Windows.Forms.Button btnItemTrends;
        private System.Windows.Forms.DateTimePicker dtpTrendsFrom;
        private System.Windows.Forms.DateTimePicker dtpTrendsTo;
        private System.Windows.Forms.Label lblItemTrends;
        private System.Windows.Forms.Label lblTrendsTo;
        // Q4 controls
        private System.Windows.Forms.Button btnParticipation;
        private System.Windows.Forms.DateTimePicker dtpPartFrom;
        private System.Windows.Forms.DateTimePicker dtpPartTo;
        private System.Windows.Forms.Label lblParticipation;
        private System.Windows.Forms.Label lblPartTo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblMembersHeader = new System.Windows.Forms.Label();
            this.dgvMembers = new System.Windows.Forms.DataGridView();
            this.txtSearchMember = new System.Windows.Forms.TextBox();
            this.btnSearchMember = new System.Windows.Forms.Button();
            this.btnClearMemberSearch = new System.Windows.Forms.Button();
            this.txtMemberID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblMemID = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSurname = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.btnUpdateMemberStatus = new System.Windows.Forms.Button();
            this.lblInventoryHeader = new System.Windows.Forms.Label();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.txtSearchItem = new System.Windows.Forms.TextBox();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.btnClearItemSearch = new System.Windows.Forms.Button();
            this.txtNewPrice = new System.Windows.Forms.TextBox();
            this.lblNewPrice = new System.Windows.Forms.Label();
            this.btnUpdatePrice = new System.Windows.Forms.Button();
            this.lblWeeklyOrder = new System.Windows.Forms.Label();
            this.dtpWeeklyOrder = new System.Windows.Forms.DateTimePicker();
            this.btnWeeklyOrder = new System.Windows.Forms.Button();
            this.lblMemberSummary = new System.Windows.Forms.Label();
            this.dtpMemberSummary = new System.Windows.Forms.DateTimePicker();
            this.btnMemberSummary = new System.Windows.Forms.Button();
            this.lblItemTrends = new System.Windows.Forms.Label();
            this.dtpTrendsFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTrendsTo = new System.Windows.Forms.Label();
            this.dtpTrendsTo = new System.Windows.Forms.DateTimePicker();
            this.btnItemTrends = new System.Windows.Forms.Button();
            this.lblParticipation = new System.Windows.Forms.Label();
            this.dtpPartFrom = new System.Windows.Forms.DateTimePicker();
            this.lblPartTo = new System.Windows.Forms.Label();
            this.dtpPartTo = new System.Windows.Forms.DateTimePicker();
            this.btnParticipation = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMembersHeader
            // 
            this.lblMembersHeader.AutoSize = true;
            this.lblMembersHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMembersHeader.Location = new System.Drawing.Point(20, 20);
            this.lblMembersHeader.Name = "lblMembersHeader";
            this.lblMembersHeader.Size = new System.Drawing.Size(225, 28);
            this.lblMembersHeader.TabIndex = 0;
            this.lblMembersHeader.Text = "Resident Management";
            // 
            // dgvMembers
            // 
            this.dgvMembers.ColumnHeadersHeight = 29;
            this.dgvMembers.Location = new System.Drawing.Point(20, 50);
            this.dgvMembers.Name = "dgvMembers";
            this.dgvMembers.RowHeadersWidth = 51;
            this.dgvMembers.Size = new System.Drawing.Size(500, 200);
            this.dgvMembers.TabIndex = 1;
            // 
            // txtSearchMember
            // 
            this.txtSearchMember.Location = new System.Drawing.Point(260, 24);
            this.txtSearchMember.Name = "txtSearchMember";
            this.txtSearchMember.Size = new System.Drawing.Size(140, 22);
            this.txtSearchMember.TabIndex = 35;
            // 
            // btnSearchMember
            // 
            this.btnSearchMember.Location = new System.Drawing.Point(405, 22);
            this.btnSearchMember.Name = "btnSearchMember";
            this.btnSearchMember.Size = new System.Drawing.Size(65, 25);
            this.btnSearchMember.TabIndex = 36;
            this.btnSearchMember.Text = "Search";
            this.btnSearchMember.Click += new System.EventHandler(this.btnSearchMember_Click);
            // 
            // btnClearMemberSearch
            // 
            this.btnClearMemberSearch.Location = new System.Drawing.Point(475, 22);
            this.btnClearMemberSearch.Name = "btnClearMemberSearch";
            this.btnClearMemberSearch.Size = new System.Drawing.Size(45, 25);
            this.btnClearMemberSearch.TabIndex = 37;
            this.btnClearMemberSearch.Text = "Clear";
            this.btnClearMemberSearch.Click += new System.EventHandler(this.btnClearMemberSearch_Click);
            // 
            // txtMemberID
            // 
            this.txtMemberID.Location = new System.Drawing.Point(120, 270);
            this.txtMemberID.Name = "txtMemberID";
            this.txtMemberID.Size = new System.Drawing.Size(150, 22);
            this.txtMemberID.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 300);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 22);
            this.txtName.TabIndex = 5;
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(120, 330);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(150, 22);
            this.txtSurname.TabIndex = 7;
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(120, 360);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(150, 22);
            this.txtContact.TabIndex = 9;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(120, 390);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(150, 22);
            this.txtEmail.TabIndex = 11;
            // 
            // lblMemID
            // 
            this.lblMemID.Location = new System.Drawing.Point(20, 270);
            this.lblMemID.Name = "lblMemID";
            this.lblMemID.Size = new System.Drawing.Size(100, 23);
            this.lblMemID.TabIndex = 2;
            this.lblMemID.Text = "Member ID:";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(20, 300);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(100, 23);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Name:";
            // 
            // lblSurname
            // 
            this.lblSurname.Location = new System.Drawing.Point(20, 330);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(100, 23);
            this.lblSurname.TabIndex = 6;
            this.lblSurname.Text = "Surname:";
            // 
            // lblContact
            // 
            this.lblContact.Location = new System.Drawing.Point(20, 360);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(100, 23);
            this.lblContact.TabIndex = 8;
            this.lblContact.Text = "Contact:";
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(20, 390);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(100, 23);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "Email:";
            // 
            // btnAddMember
            // 
            this.btnAddMember.Location = new System.Drawing.Point(20, 420);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(120, 30);
            this.btnAddMember.TabIndex = 12;
            this.btnAddMember.Text = "Add Resident";
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            // 
            // btnUpdateMemberStatus
            // 
            this.btnUpdateMemberStatus.Location = new System.Drawing.Point(150, 420);
            this.btnUpdateMemberStatus.Name = "btnUpdateMemberStatus";
            this.btnUpdateMemberStatus.Size = new System.Drawing.Size(120, 30);
            this.btnUpdateMemberStatus.TabIndex = 13;
            this.btnUpdateMemberStatus.Text = "Toggle Active";
            this.btnUpdateMemberStatus.Click += new System.EventHandler(this.btnUpdateMemberStatus_Click);
            // 
            // lblInventoryHeader
            // 
            this.lblInventoryHeader.AutoSize = true;
            this.lblInventoryHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblInventoryHeader.Location = new System.Drawing.Point(550, 20);
            this.lblInventoryHeader.Name = "lblInventoryHeader";
            this.lblInventoryHeader.Size = new System.Drawing.Size(238, 28);
            this.lblInventoryHeader.TabIndex = 14;
            this.lblInventoryHeader.Text = "Catalogue Management";
            // 
            // dgvInventory
            // 
            this.dgvInventory.ColumnHeadersHeight = 29;
            this.dgvInventory.Location = new System.Drawing.Point(550, 50);
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.RowHeadersWidth = 51;
            this.dgvInventory.Size = new System.Drawing.Size(400, 200);
            this.dgvInventory.TabIndex = 15;
            // 
            // txtSearchItem
            // 
            this.txtSearchItem.Location = new System.Drawing.Point(795, 24);
            this.txtSearchItem.Name = "txtSearchItem";
            this.txtSearchItem.Size = new System.Drawing.Size(90, 22);
            this.txtSearchItem.TabIndex = 38;
            // 
            // btnSearchItem
            // 
            this.btnSearchItem.Location = new System.Drawing.Point(890, 22);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(50, 25);
            this.btnSearchItem.TabIndex = 39;
            this.btnSearchItem.Text = "Find";
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearchItem_Click);
            // 
            // btnClearItemSearch
            // 
            this.btnClearItemSearch.Location = new System.Drawing.Point(895, 50);
            this.btnClearItemSearch.Name = "btnClearItemSearch";
            this.btnClearItemSearch.Size = new System.Drawing.Size(50, 25);
            this.btnClearItemSearch.TabIndex = 40;
            this.btnClearItemSearch.Text = "Clear";
            this.btnClearItemSearch.Click += new System.EventHandler(this.btnClearItemSearch_Click);
            // 
            // txtNewPrice
            // 
            this.txtNewPrice.Location = new System.Drawing.Point(650, 267);
            this.txtNewPrice.Name = "txtNewPrice";
            this.txtNewPrice.Size = new System.Drawing.Size(100, 22);
            this.txtNewPrice.TabIndex = 17;
            // 
            // lblNewPrice
            // 
            this.lblNewPrice.Location = new System.Drawing.Point(550, 270);
            this.lblNewPrice.Name = "lblNewPrice";
            this.lblNewPrice.Size = new System.Drawing.Size(100, 23);
            this.lblNewPrice.TabIndex = 16;
            this.lblNewPrice.Text = "New Price (R):";
            // 
            // btnUpdatePrice
            // 
            this.btnUpdatePrice.Location = new System.Drawing.Point(550, 300);
            this.btnUpdatePrice.Name = "btnUpdatePrice";
            this.btnUpdatePrice.Size = new System.Drawing.Size(180, 30);
            this.btnUpdatePrice.TabIndex = 18;
            this.btnUpdatePrice.Text = "Update Item Price";
            this.btnUpdatePrice.Click += new System.EventHandler(this.btnUpdatePrice_Click);
            // 
            // lblWeeklyOrder
            // 
            this.lblWeeklyOrder.AutoSize = true;
            this.lblWeeklyOrder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblWeeklyOrder.Location = new System.Drawing.Point(20, 510);
            this.lblWeeklyOrder.Name = "lblWeeklyOrder";
            this.lblWeeklyOrder.Size = new System.Drawing.Size(210, 20);
            this.lblWeeklyOrder.TabIndex = 19;
            this.lblWeeklyOrder.Text = "Weekly Order - Select Friday:";
            // 
            // dtpWeeklyOrder
            // 
            this.dtpWeeklyOrder.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpWeeklyOrder.Location = new System.Drawing.Point(236, 512);
            this.dtpWeeklyOrder.Name = "dtpWeeklyOrder";
            this.dtpWeeklyOrder.Size = new System.Drawing.Size(150, 22);
            this.dtpWeeklyOrder.TabIndex = 20;
            this.dtpWeeklyOrder.Value = new System.DateTime(2026, 7, 24, 0, 0, 0, 0);
            // 
            // btnWeeklyOrder
            // 
            this.btnWeeklyOrder.BackColor = System.Drawing.Color.LightBlue;
            this.btnWeeklyOrder.Location = new System.Drawing.Point(408, 511);
            this.btnWeeklyOrder.Name = "btnWeeklyOrder";
            this.btnWeeklyOrder.Size = new System.Drawing.Size(200, 28);
            this.btnWeeklyOrder.TabIndex = 21;
            this.btnWeeklyOrder.Text = "Generate Weekly Order List";
            this.btnWeeklyOrder.UseVisualStyleBackColor = false;
            this.btnWeeklyOrder.Click += new System.EventHandler(this.btnWeeklyOrder_Click);
            // 
            // lblMemberSummary
            // 
            this.lblMemberSummary.AutoSize = true;
            this.lblMemberSummary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMemberSummary.Location = new System.Drawing.Point(20, 548);
            this.lblMemberSummary.Name = "lblMemberSummary";
            this.lblMemberSummary.Size = new System.Drawing.Size(256, 20);
            this.lblMemberSummary.TabIndex = 22;
            this.lblMemberSummary.Text = "Member Summaries - Select Friday:";
            // 
            // dtpMemberSummary
            // 
            this.dtpMemberSummary.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpMemberSummary.Location = new System.Drawing.Point(282, 548);
            this.dtpMemberSummary.Name = "dtpMemberSummary";
            this.dtpMemberSummary.Size = new System.Drawing.Size(150, 22);
            this.dtpMemberSummary.TabIndex = 23;
            this.dtpMemberSummary.Value = new System.DateTime(2026, 7, 24, 0, 0, 0, 0);
            // 
            // btnMemberSummary
            // 
            this.btnMemberSummary.BackColor = System.Drawing.Color.LightSalmon;
            this.btnMemberSummary.Location = new System.Drawing.Point(449, 545);
            this.btnMemberSummary.Name = "btnMemberSummary";
            this.btnMemberSummary.Size = new System.Drawing.Size(220, 28);
            this.btnMemberSummary.TabIndex = 24;
            this.btnMemberSummary.Text = "View & Export Member Summaries";
            this.btnMemberSummary.UseVisualStyleBackColor = false;
            this.btnMemberSummary.Click += new System.EventHandler(this.btnMemberSummary_Click);
            // 
            // lblItemTrends
            // 
            this.lblItemTrends.AutoSize = true;
            this.lblItemTrends.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblItemTrends.Location = new System.Drawing.Point(20, 588);
            this.lblItemTrends.Name = "lblItemTrends";
            this.lblItemTrends.Size = new System.Drawing.Size(148, 20);
            this.lblItemTrends.TabIndex = 25;
            this.lblItemTrends.Text = "Item Trends - From:";
            // 
            // dtpTrendsFrom
            // 
            this.dtpTrendsFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTrendsFrom.Location = new System.Drawing.Point(174, 588);
            this.dtpTrendsFrom.Name = "dtpTrendsFrom";
            this.dtpTrendsFrom.Size = new System.Drawing.Size(130, 22);
            this.dtpTrendsFrom.TabIndex = 26;
            this.dtpTrendsFrom.Value = new System.DateTime(2025, 11, 1, 0, 0, 0, 0);
            // 
            // lblTrendsTo
            // 
            this.lblTrendsTo.AutoSize = true;
            this.lblTrendsTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTrendsTo.Location = new System.Drawing.Point(324, 592);
            this.lblTrendsTo.Name = "lblTrendsTo";
            this.lblTrendsTo.Size = new System.Drawing.Size(30, 20);
            this.lblTrendsTo.TabIndex = 27;
            this.lblTrendsTo.Text = "To:";
            // 
            // dtpTrendsTo
            // 
            this.dtpTrendsTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTrendsTo.Location = new System.Drawing.Point(361, 588);
            this.dtpTrendsTo.Name = "dtpTrendsTo";
            this.dtpTrendsTo.Size = new System.Drawing.Size(130, 22);
            this.dtpTrendsTo.TabIndex = 28;
            this.dtpTrendsTo.Value = new System.DateTime(2026, 7, 24, 0, 0, 0, 0);
            // 
            // btnItemTrends
            // 
            this.btnItemTrends.BackColor = System.Drawing.Color.Plum;
            this.btnItemTrends.Location = new System.Drawing.Point(511, 584);
            this.btnItemTrends.Name = "btnItemTrends";
            this.btnItemTrends.Size = new System.Drawing.Size(200, 28);
            this.btnItemTrends.TabIndex = 29;
            this.btnItemTrends.Text = "Generate Item Trends Report";
            this.btnItemTrends.UseVisualStyleBackColor = false;
            this.btnItemTrends.Click += new System.EventHandler(this.btnItemTrends_Click);
            // 
            // lblParticipation
            // 
            this.lblParticipation.AutoSize = true;
            this.lblParticipation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblParticipation.Location = new System.Drawing.Point(20, 628);
            this.lblParticipation.Name = "lblParticipation";
            this.lblParticipation.Size = new System.Drawing.Size(153, 20);
            this.lblParticipation.TabIndex = 30;
            this.lblParticipation.Text = "Participation - From:";
            // 
            // dtpPartFrom
            // 
            this.dtpPartFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPartFrom.Location = new System.Drawing.Point(179, 628);
            this.dtpPartFrom.Name = "dtpPartFrom";
            this.dtpPartFrom.Size = new System.Drawing.Size(130, 22);
            this.dtpPartFrom.TabIndex = 31;
            this.dtpPartFrom.Value = new System.DateTime(2025, 11, 1, 0, 0, 0, 0);
            // 
            // lblPartTo
            // 
            this.lblPartTo.AutoSize = true;
            this.lblPartTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPartTo.Location = new System.Drawing.Point(324, 628);
            this.lblPartTo.Name = "lblPartTo";
            this.lblPartTo.Size = new System.Drawing.Size(30, 20);
            this.lblPartTo.TabIndex = 32;
            this.lblPartTo.Text = "To:";
            // 
            // dtpPartTo
            // 
            this.dtpPartTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPartTo.Location = new System.Drawing.Point(361, 628);
            this.dtpPartTo.Name = "dtpPartTo";
            this.dtpPartTo.Size = new System.Drawing.Size(130, 22);
            this.dtpPartTo.TabIndex = 33;
            this.dtpPartTo.Value = new System.DateTime(2026, 7, 24, 0, 0, 0, 0);
            // 
            // btnParticipation
            // 
            this.btnParticipation.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.btnParticipation.Location = new System.Drawing.Point(511, 622);
            this.btnParticipation.Name = "btnParticipation";
            this.btnParticipation.Size = new System.Drawing.Size(210, 28);
            this.btnParticipation.TabIndex = 34;
            this.btnParticipation.Text = "Generate Participation Report";
            this.btnParticipation.UseVisualStyleBackColor = false;
            this.btnParticipation.Click += new System.EventHandler(this.btnParticipation_Click);
            // 
            // AdminForm
            // 
            this.ClientSize = new System.Drawing.Size(980, 680);
            this.Controls.Add(this.lblMembersHeader);
            this.Controls.Add(this.dgvMembers);
            this.Controls.Add(this.txtSearchMember);
            this.Controls.Add(this.btnSearchMember);
            this.Controls.Add(this.btnClearMemberSearch);
            this.Controls.Add(this.lblMemID);
            this.Controls.Add(this.txtMemberID);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblSurname);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnAddMember);
            this.Controls.Add(this.btnUpdateMemberStatus);
            this.Controls.Add(this.lblInventoryHeader);
            this.Controls.Add(this.dgvInventory);
            this.Controls.Add(this.txtSearchItem);
            this.Controls.Add(this.btnSearchItem);
            this.Controls.Add(this.btnClearItemSearch);
            this.Controls.Add(this.lblNewPrice);
            this.Controls.Add(this.txtNewPrice);
            this.Controls.Add(this.btnUpdatePrice);
            this.Controls.Add(this.lblWeeklyOrder);
            this.Controls.Add(this.dtpWeeklyOrder);
            this.Controls.Add(this.btnWeeklyOrder);
            this.Controls.Add(this.lblMemberSummary);
            this.Controls.Add(this.dtpMemberSummary);
            this.Controls.Add(this.btnMemberSummary);
            this.Controls.Add(this.lblItemTrends);
            this.Controls.Add(this.dtpTrendsFrom);
            this.Controls.Add(this.lblTrendsTo);
            this.Controls.Add(this.dtpTrendsTo);
            this.Controls.Add(this.btnItemTrends);
            this.Controls.Add(this.lblParticipation);
            this.Controls.Add(this.dtpPartFrom);
            this.Controls.Add(this.lblPartTo);
            this.Controls.Add(this.dtpPartTo);
            this.Controls.Add(this.btnParticipation);
            this.Name = "AdminForm";
            this.Text = "Matron Dashboard";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}