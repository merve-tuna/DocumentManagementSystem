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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pnlSidebar = new Panel();
            lblUserIcon = new PictureBox();
            btnHelp = new Button();
            btnTrash = new Button();
            btnPendingApproval = new Button();
            btnMyDocuments = new Button();
            btnDocumentAdd = new Button();
            cmbUserRole = new ComboBox();
            pnlHeader = new Panel();
            lblRecordCount = new Label();
            panel1 = new Panel();
            btnSearch = new Button();
            txtSearch = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            btnClear = new Button();
            btnFilter = new Button();
            cmbCategory = new ComboBox();
            cmbDepartment = new ComboBox();
            dtpEndDate = new DateTimePicker();
            dtpStartDate = new DateTimePicker();
            dgvDocuments = new DataGridView();
            btnDownload = new DataGridViewButtonColumn();
            btnDelete = new DataGridViewButtonColumn();
            pnlSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lblUserIcon).BeginInit();
            pnlHeader.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDocuments).BeginInit();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.Controls.Add(lblUserIcon);
            pnlSidebar.Controls.Add(btnHelp);
            pnlSidebar.Controls.Add(btnTrash);
            pnlSidebar.Controls.Add(btnPendingApproval);
            pnlSidebar.Controls.Add(btnMyDocuments);
            pnlSidebar.Controls.Add(btnDocumentAdd);
            pnlSidebar.Controls.Add(cmbUserRole);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(200, 450);
            pnlSidebar.TabIndex = 0;
            // 
            // lblUserIcon
            // 
            lblUserIcon.Image = (Image)resources.GetObject("lblUserIcon.Image");
            lblUserIcon.Location = new Point(16, 12);
            lblUserIcon.Name = "lblUserIcon";
            lblUserIcon.Size = new Size(40, 31);
            lblUserIcon.SizeMode = PictureBoxSizeMode.Zoom;
            lblUserIcon.TabIndex = 5;
            lblUserIcon.TabStop = false;
            // 
            // btnHelp
            // 
            btnHelp.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnHelp.Location = new Point(16, 276);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(170, 35);
            btnHelp.TabIndex = 4;
            btnHelp.Text = "❓ Yardım";
            btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnTrash
            // 
            btnTrash.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnTrash.Location = new Point(16, 235);
            btnTrash.Name = "btnTrash";
            btnTrash.Size = new Size(170, 35);
            btnTrash.TabIndex = 3;
            btnTrash.Text = "🗑️ Çöp Kutusu";
            btnTrash.UseVisualStyleBackColor = true;
            // 
            // btnPendingApproval
            // 
            btnPendingApproval.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnPendingApproval.Location = new Point(16, 194);
            btnPendingApproval.Name = "btnPendingApproval";
            btnPendingApproval.Size = new Size(170, 35);
            btnPendingApproval.TabIndex = 2;
            btnPendingApproval.Text = "⏳ Onay Bekleyenler";
            btnPendingApproval.UseVisualStyleBackColor = true;
            // 
            // btnMyDocuments
            // 
            btnMyDocuments.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnMyDocuments.Location = new Point(16, 153);
            btnMyDocuments.Name = "btnMyDocuments";
            btnMyDocuments.Size = new Size(170, 35);
            btnMyDocuments.TabIndex = 1;
            btnMyDocuments.Text = "📂 Belgelerim";
            btnMyDocuments.UseVisualStyleBackColor = true;
            // 
            // btnDocumentAdd
            // 
            btnDocumentAdd.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnDocumentAdd.Location = new Point(16, 112);
            btnDocumentAdd.Name = "btnDocumentAdd";
            btnDocumentAdd.Size = new Size(170, 35);
            btnDocumentAdd.TabIndex = 0;
            btnDocumentAdd.Text = "📄 Belge Ekle";
            btnDocumentAdd.UseVisualStyleBackColor = true;
            // 
            // cmbUserRole
            // 
            cmbUserRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbUserRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUserRole.Font = new Font("Arial", 9F);
            cmbUserRole.FormattingEnabled = true;
            cmbUserRole.Location = new Point(62, 16);
            cmbUserRole.Name = "cmbUserRole";
            cmbUserRole.Size = new Size(120, 25);
            cmbUserRole.TabIndex = 12;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblRecordCount);
            pnlHeader.Controls.Add(panel1);
            pnlHeader.Controls.Add(label5);
            pnlHeader.Controls.Add(label6);
            pnlHeader.Controls.Add(label7);
            pnlHeader.Controls.Add(label8);
            pnlHeader.Controls.Add(btnClear);
            pnlHeader.Controls.Add(btnFilter);
            pnlHeader.Controls.Add(cmbCategory);
            pnlHeader.Controls.Add(cmbDepartment);
            pnlHeader.Controls.Add(dtpEndDate);
            pnlHeader.Controls.Add(dtpStartDate);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(200, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(600, 147);
            pnlHeader.TabIndex = 1;
            // 
            // lblRecordCount
            // 
            lblRecordCount.AutoSize = true;
            lblRecordCount.Location = new Point(9, 122);
            lblRecordCount.Name = "lblRecordCount";
            lblRecordCount.Size = new Size(50, 20);
            lblRecordCount.TabIndex = 26;
            lblRecordCount.Text = "label1";
            lblRecordCount.Click += lblRecordCount_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(txtSearch);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(600, 43);
            panel1.TabIndex = 25;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(502, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(95, 30);
            btnSearch.TabIndex = 26;
            btnSearch.Text = "Ara";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click_1;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(9, 12);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(490, 27);
            txtSearch.TabIndex = 25;
            txtSearch.KeyDown += txtSearch_KeyDown;
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
            btnClear.Location = new Point(502, 112);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(95, 30);
            btnClear.TabIndex = 19;
            btnClear.Text = "Temizle";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnFilter
            // 
            btnFilter.Font = new Font("Arial", 9F);
            btnFilter.Location = new Point(502, 69);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(95, 30);
            btnFilter.TabIndex = 18;
            btnFilter.Text = "Filtrele";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Font = new Font("Arial", 9F);
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(387, 73);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(112, 25);
            cmbCategory.TabIndex = 17;
            // 
            // cmbDepartment
            // 
            cmbDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDepartment.Font = new Font("Arial", 9F);
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Location = new Point(261, 73);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(112, 25);
            cmbDepartment.TabIndex = 16;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Font = new Font("Arial", 9F);
            dtpEndDate.Location = new Point(135, 73);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(112, 25);
            dtpEndDate.TabIndex = 15;
            // 
            // dtpStartDate
            // 
            dtpStartDate.Font = new Font("Arial", 9F);
            dtpStartDate.Location = new Point(9, 73);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(112, 25);
            dtpStartDate.TabIndex = 14;
            // 
            // dgvDocuments
            // 
            dgvDocuments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDocuments.Columns.AddRange(new DataGridViewColumn[] { btnDownload, btnDelete });
            dgvDocuments.Location = new Point(200, 148);
            dgvDocuments.Name = "dgvDocuments";
            dgvDocuments.ReadOnly = true;
            dgvDocuments.RowHeadersWidth = 51;
            dgvDocuments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDocuments.Size = new Size(600, 301);
            dgvDocuments.TabIndex = 2;
            dgvDocuments.CellContentClick += dgvDocuments_CellContentClick;
            dgvDocuments.CellMouseDoubleClick += dgvDocuments_CellMouseDoubleClick;
            // 
            // btnDownload
            // 
            btnDownload.HeaderText = "İndir";
            btnDownload.MinimumWidth = 6;
            btnDownload.Name = "btnDownload";
            btnDownload.ReadOnly = true;
            btnDownload.UseColumnTextForButtonValue = true;
            btnDownload.Width = 125;
            // 
            // btnDelete
            // 
            btnDelete.HeaderText = "Sil";
            btnDelete.MinimumWidth = 6;
            btnDelete.Name = "btnDelete";
            btnDelete.ReadOnly = true;
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.Width = 125;
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
            ((System.ComponentModel.ISupportInitialize)lblUserIcon).EndInit();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private DateTimePicker dtpEndDate;
        private DateTimePicker dtpStartDate;
        private PictureBox lblUserIcon;
        private Panel panel1;
        private TextBox txtSearch;
        private Button btnSearch;
        private Label lblRecordCount;
        private DataGridViewButtonColumn btnDownload;
        private DataGridViewButtonColumn btnDelete;
    }
}
