namespace DocumentManagementSystem
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlSidebar = new Panel();
            btnHelp = new Button();
            btnTrash = new Button();
            btnPendingApproval = new Button();
            btnMyDocuments = new Button();
            btnDocumentAdd = new Button();
            pnlHeader = new Panel();
            dgvDocuments = new DataGridView();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            btnClear = new Button();
            btnFilter = new Button();
            cmbUserRole = new ComboBox();
            cmbCategory = new ComboBox();
            cmbDepartment = new ComboBox();
            lblUserIcon = new Label();
            dtpEndDate = new DateTimePicker();
            dtpStartDate = new DateTimePicker();
            pnlSidebar.SuspendLayout();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDocuments).BeginInit();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.Controls.Add(btnHelp);
            pnlSidebar.Controls.Add(btnTrash);
            pnlSidebar.Controls.Add(btnPendingApproval);
            pnlSidebar.Controls.Add(btnMyDocuments);
            pnlSidebar.Controls.Add(btnDocumentAdd);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(200, 450);
            pnlSidebar.TabIndex = 0;
            // 
            // btnHelp
            // 
            btnHelp.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnHelp.Location = new Point(12, 276);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(170, 35);
            btnHelp.TabIndex = 4;
            btnHelp.Text = "❓ Yardım";
            btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnTrash
            // 
            btnTrash.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnTrash.Location = new Point(12, 235);
            btnTrash.Name = "btnTrash";
            btnTrash.Size = new Size(170, 35);
            btnTrash.TabIndex = 3;
            btnTrash.Text = "🗑️ Çöp Kutusu";
            btnTrash.UseVisualStyleBackColor = true;
            // 
            // btnPendingApproval
            // 
            btnPendingApproval.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnPendingApproval.Location = new Point(12, 194);
            btnPendingApproval.Name = "btnPendingApproval";
            btnPendingApproval.Size = new Size(170, 35);
            btnPendingApproval.TabIndex = 2;
            btnPendingApproval.Text = "⏳ Onay Bekleyenler";
            btnPendingApproval.UseVisualStyleBackColor = true;
            // 
            // btnMyDocuments
            // 
            btnMyDocuments.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnMyDocuments.Location = new Point(12, 153);
            btnMyDocuments.Name = "btnMyDocuments";
            btnMyDocuments.Size = new Size(170, 35);
            btnMyDocuments.TabIndex = 1;
            btnMyDocuments.Text = "📂 Belgelerim";
            btnMyDocuments.UseVisualStyleBackColor = true;
            // 
            // btnDocumentAdd
            // 
            btnDocumentAdd.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnDocumentAdd.Location = new Point(12, 112);
            btnDocumentAdd.Name = "btnDocumentAdd";
            btnDocumentAdd.Size = new Size(170, 35);
            btnDocumentAdd.TabIndex = 0;
            btnDocumentAdd.Text = "📄 Belge Ekle";
            btnDocumentAdd.UseVisualStyleBackColor = true;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(label5);
            pnlHeader.Controls.Add(label6);
            pnlHeader.Controls.Add(label7);
            pnlHeader.Controls.Add(label8);
            pnlHeader.Controls.Add(btnClear);
            pnlHeader.Controls.Add(btnFilter);
            pnlHeader.Controls.Add(cmbUserRole);
            pnlHeader.Controls.Add(cmbCategory);
            pnlHeader.Controls.Add(cmbDepartment);
            pnlHeader.Controls.Add(lblUserIcon);
            pnlHeader.Controls.Add(dtpEndDate);
            pnlHeader.Controls.Add(dtpStartDate);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(200, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(600, 147);
            pnlHeader.TabIndex = 1;
            // 
            // dgvDocuments
            // 
            dgvDocuments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDocuments.Dock = DockStyle.Fill;
            dgvDocuments.Location = new Point(0, 0);
            dgvDocuments.Name = "dgvDocuments";
            dgvDocuments.RowHeadersWidth = 51;
            dgvDocuments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDocuments.Size = new Size(800, 450);
            dgvDocuments.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Arial", 9F);
            label5.Location = new Point(387, 53);
            label5.Name = "label5";
            label5.Size = new Size(62, 17);
            label5.TabIndex = 23;
            label5.Text = "Kategori";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial", 9F);
            label6.Location = new Point(261, 53);
            label6.Name = "label6";
            label6.Size = new Size(81, 17);
            label6.TabIndex = 22;
            label6.Text = "Departman";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial", 9F);
            label7.Location = new Point(135, 53);
            label7.Name = "label7";
            label7.Size = new Size(74, 17);
            label7.TabIndex = 21;
            label7.Text = "Bitiş Tarihi";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Arial", 9F);
            label8.Location = new Point(9, 53);
            label8.Name = "label8";
            label8.Size = new Size(110, 17);
            label8.TabIndex = 20;
            label8.Text = "Başlangıç Tarihi";
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Arial", 9F);
            btnClear.Location = new Point(496, 104);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(95, 30);
            btnClear.TabIndex = 19;
            btnClear.Text = "Temizle";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnFilter
            // 
            btnFilter.Font = new Font("Arial", 9F);
            btnFilter.Location = new Point(387, 104);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(95, 30);
            btnFilter.TabIndex = 18;
            btnFilter.Text = "Filtrele";
            btnFilter.UseVisualStyleBackColor = true;
            // 
            // cmbUserRole
            // 
            cmbUserRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbUserRole.Font = new Font("Arial", 9F);
            cmbUserRole.FormattingEnabled = true;
            cmbUserRole.Location = new Point(468, 13);
            cmbUserRole.Name = "cmbUserRole";
            cmbUserRole.Size = new Size(120, 25);
            cmbUserRole.TabIndex = 12;
            // 
            // cmbCategory
            // 
            cmbCategory.Font = new Font("Arial", 9F);
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(387, 73);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(120, 25);
            cmbCategory.TabIndex = 17;
            // 
            // cmbDepartment
            // 
            cmbDepartment.Font = new Font("Arial", 9F);
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Location = new Point(261, 73);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(120, 25);
            cmbDepartment.TabIndex = 16;
            // 
            // lblUserIcon
            // 
            lblUserIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserIcon.AutoSize = true;
            lblUserIcon.Font = new Font("Arial", 9F);
            lblUserIcon.Location = new Point(396, 16);
            lblUserIcon.Name = "lblUserIcon";
            lblUserIcon.Size = new Size(66, 17);
            lblUserIcon.TabIndex = 13;
            lblUserIcon.Text = "Kullanıcı:";
            // 
            // dtpEndDate
            // 
            dtpEndDate.Font = new Font("Arial", 9F);
            dtpEndDate.Location = new Point(135, 73);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(120, 25);
            dtpEndDate.TabIndex = 15;
            // 
            // dtpStartDate
            // 
            dtpStartDate.Font = new Font("Arial", 9F);
            dtpStartDate.Location = new Point(9, 73);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(120, 25);
            dtpStartDate.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);
            Controls.Add(dgvDocuments);
            Name = "Form1";
            Text = "Form1";
            pnlSidebar.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDocuments).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Panel pnlHeader;
        private DataGridView dgvDocuments;
        private Button btnHelp;
        private Button btnTrash;
        private Button btnPendingApproval;
        private Button btnMyDocuments;
        private Button btnDocumentAdd;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Button btnClear;
        private Button btnFilter;
        private ComboBox cmbUserRole;
        private ComboBox cmbCategory;
        private ComboBox cmbDepartment;
        private Label lblUserIcon;
        private DateTimePicker dtpEndDate;
        private DateTimePicker dtpStartDate;
    }
}
