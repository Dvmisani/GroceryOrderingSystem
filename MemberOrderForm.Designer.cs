namespace GroceryOrderingSystem
{
    partial class MemberOrderForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTargetCycle;
        private System.Windows.Forms.Label lblMemberDetail;
        private System.Windows.Forms.DataGridView dgvCatalog;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.ListBox lstBasketSummary;
        private System.Windows.Forms.Label lblRunningTotal;
        private System.Windows.Forms.Button btnFinalizeSubmission;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTargetCycle = new System.Windows.Forms.Label();
            this.lblMemberDetail = new System.Windows.Forms.Label();
            this.dgvCatalog = new System.Windows.Forms.DataGridView();
            this.lblQty = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.lstBasketSummary = new System.Windows.Forms.ListBox();
            this.lblRunningTotal = new System.Windows.Forms.Label();
            this.btnFinalizeSubmission = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatalog)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTargetCycle
            // 
            this.lblTargetCycle.AutoSize = true;
            this.lblTargetCycle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTargetCycle.Location = new System.Drawing.Point(20, 20);
            this.lblTargetCycle.Name = "lblTargetCycle";
            this.lblTargetCycle.Size = new System.Drawing.Size(0, 28);
            this.lblTargetCycle.TabIndex = 7;
            // 
            // lblMemberDetail
            // 
            this.lblMemberDetail.AutoSize = true;
            this.lblMemberDetail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblMemberDetail.Location = new System.Drawing.Point(20, 48);
            this.lblMemberDetail.Name = "lblMemberDetail";
            this.lblMemberDetail.Size = new System.Drawing.Size(0, 20);
            this.lblMemberDetail.TabIndex = 8;
            // 
            // dgvCatalog
            // 
            this.dgvCatalog.ColumnHeadersHeight = 29;
            this.dgvCatalog.Location = new System.Drawing.Point(20, 75);
            this.dgvCatalog.Name = "dgvCatalog";
            this.dgvCatalog.RowHeadersWidth = 51;
            this.dgvCatalog.Size = new System.Drawing.Size(400, 235);
            this.dgvCatalog.TabIndex = 6;
            // 
            // lblQty
            // 
            this.lblQty.Location = new System.Drawing.Point(20, 330);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(100, 23);
            this.lblQty.TabIndex = 5;
            this.lblQty.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(75, 327);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(60, 22);
            this.txtQuantity.TabIndex = 4;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(150, 325);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(270, 25);
            this.btnAddItem.TabIndex = 3;
            this.btnAddItem.Text = "Add Selected Item to Basket";
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // lstBasketSummary
            // 
            this.lstBasketSummary.ItemHeight = 16;
            this.lstBasketSummary.Location = new System.Drawing.Point(450, 60);
            this.lstBasketSummary.Name = "lstBasketSummary";
            this.lstBasketSummary.Size = new System.Drawing.Size(300, 196);
            this.lstBasketSummary.TabIndex = 2;
            // 
            // lblRunningTotal
            // 
            this.lblRunningTotal.AutoSize = true;
            this.lblRunningTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRunningTotal.Location = new System.Drawing.Point(450, 275);
            this.lblRunningTotal.Name = "lblRunningTotal";
            this.lblRunningTotal.Size = new System.Drawing.Size(0, 23);
            this.lblRunningTotal.TabIndex = 1;
            // 
            // btnFinalizeSubmission
            // 
            this.btnFinalizeSubmission.BackColor = System.Drawing.Color.LightGreen;
            this.btnFinalizeSubmission.Location = new System.Drawing.Point(450, 310);
            this.btnFinalizeSubmission.Name = "btnFinalizeSubmission";
            this.btnFinalizeSubmission.Size = new System.Drawing.Size(300, 40);
            this.btnFinalizeSubmission.TabIndex = 0;
            this.btnFinalizeSubmission.Text = "Finalize & Submit Weekly Order";
            this.btnFinalizeSubmission.UseVisualStyleBackColor = false;
            this.btnFinalizeSubmission.Click += new System.EventHandler(this.btnFinalizeSubmission_Click);
            // 
            // MemberOrderForm
            // 
            this.ClientSize = new System.Drawing.Size(780, 374);
            this.Controls.Add(this.btnFinalizeSubmission);
            this.Controls.Add(this.lblRunningTotal);
            this.Controls.Add(this.lstBasketSummary);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.dgvCatalog);
            this.Controls.Add(this.lblMemberDetail);
            this.Controls.Add(this.lblTargetCycle);
            this.Name = "MemberOrderForm";
            this.Text = "Resident Ordering Area";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatalog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}