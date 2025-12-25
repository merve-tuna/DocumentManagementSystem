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
            lblUserIconA = new Label();
            pnlHeader = new Panel();
            lblSelectionCount = new Label();
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
            pnlSidebar.Controls.Add(lblUserIconA);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Margin = new Padding(4, 4, 4, 4);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(250, 562);
            pnlSidebar.TabIndex = 0;
            // 
            // lblUserIcon
            // 
            lblUserIcon.Image = (Image)resources.GetObject("lblUserIcon.Image");
            lblUserIcon.Location = new Point(20, 15);
            lblUserIcon.Margin = new Padding(4, 4, 4, 4);
            lblUserIcon.Name = "lblUserIcon";
            lblUserIcon.Size = new Size(50, 39);
            lblUserIcon.SizeMode = PictureBoxSizeMode.Zoom;
            lblUserIcon.TabIndex = 5;
            lblUserIcon.TabStop = false;
            // 
            // btnHelp
            // 
            btnHelp.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnHelp.Location = new Point(15, 345);
            btnHelp.Margin = new Padding(4, 4, 4, 4);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(212, 44);
            btnHelp.TabIndex = 4;
            btnHelp.Text = "❓ Yardım";
            btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnTrash
            // 
            btnTrash.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnTrash.Location = new Point(15, 294);
            btnTrash.Margin = new Padding(4, 4, 4, 4);
            btnTrash.Name = "btnTrash";
            btnTrash.Size = new Size(212, 44);
            btnTrash.TabIndex = 3;
            btnTrash.Text = "🗑️ Çöp Kutusu";
            btnTrash.UseVisualStyleBackColor = true;
            // 
            // btnPendingApproval
            // 
            btnPendingApproval.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnPendingApproval.Location = new Point(15, 242);
            btnPendingApproval.Margin = new Padding(4, 4, 4, 4);
            btnPendingApproval.Name = "btnPendingApproval";
            btnPendingApproval.Size = new Size(212, 44);
            btnPendingApproval.TabIndex = 2;
            btnPendingApproval.Text = "⏳ Onay Bekleyenler";
            btnPendingApproval.UseVisualStyleBackColor = true;
            // 
            // btnMyDocuments
            // 
            btnMyDocuments.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnMyDocuments.Location = new Point(15, 191);
            btnMyDocuments.Margin = new Padding(4, 4, 4, 4);
            btnMyDocuments.Name = "btnMyDocuments";
            btnMyDocuments.Size = new Size(212, 44);
            btnMyDocuments.TabIndex = 1;
            btnMyDocuments.Text = "📂 Belgelerim";
            btnMyDocuments.UseVisualStyleBackColor = true;
            // 
            // btnDocumentAdd
            // 
            btnDocumentAdd.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnDocumentAdd.Location = new Point(15, 140);
            btnDocumentAdd.Margin = new Padding(4, 4, 4, 4);
            btnDocumentAdd.Name = "btnDocumentAdd";
            btnDocumentAdd.Size = new Size(212, 44);
            btnDocumentAdd.TabIndex = 0;
            btnDocumentAdd.Text = "📄 Belge Ekle";
            btnDocumentAdd.UseVisualStyleBackColor = true;
            // 
            // cmbUserRole
            // 
            cmbUserRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbUserRole.Font = new Font("Arial", 9F);
            cmbUserRole.FormattingEnabled = true;
            cmbUserRole.Location = new Point(78, 20);
            cmbUserRole.Margin = new Padding(4, 4, 4, 4);
            cmbUserRole.Name = "cmbUserRole";
            cmbUserRole.Size = new Size(149, 29);
            cmbUserRole.TabIndex = 12;
            // 
            // lblUserIconA
            // 
            lblUserIconA.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserIconA.AutoSize = true;
            lblUserIconA.Font = new Font("Arial", 9F);
            lblUserIconA.ForeColor = SystemColors.Control;
            lblUserIconA.Location = new Point(109, 55);
            lblUserIconA.Margin = new Padding(4, 0, 4, 0);
            lblUserIconA.Name = "lblUserIconA";
            lblUserIconA.Size = new Size(86, 21);
            lblUserIconA.TabIndex = 13;
            lblUserIconA.Text = "Kullanıcı:";
            lblUserIconA.Click += lblUserIcon_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblSelectionCount);
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
            pnlHeader.Location = new Point(250, 0);
            pnlHeader.Margin = new Padding(4, 4, 4, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(750, 184);
            pnlHeader.TabIndex = 1;
            // 
            // lblSelectionCount
            // 
            lblSelectionCount.AutoSize = true;
            lblSelectionCount.Location = new Point(272, 146);
            lblSelectionCount.Margin = new Padding(4, 0, 4, 0);
            lblSelectionCount.Name = "lblSelectionCount";
            lblSelectionCount.Size = new Size(59, 25);
            lblSelectionCount.TabIndex = 27;
            lblSelectionCount.Text = "label1";
            // 
            // lblRecordCount
            // 
            lblRecordCount.AutoSize = true;
            lblRecordCount.Location = new Point(59, 146);
            lblRecordCount.Margin = new Padding(4, 0, 4, 0);
            lblRecordCount.Name = "lblRecordCount";
            lblRecordCount.Size = new Size(59, 25);
            lblRecordCount.TabIndex = 26;
            lblRecordCount.Text = "label1";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(txtSearch);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(750, 54);
            panel1.TabIndex = 25;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(628, 15);
            btnSearch.Margin = new Padding(4, 4, 4, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(119, 38);
            btnSearch.TabIndex = 26;
            btnSearch.Text = "Ara";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click_1;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(11, 15);
            txtSearch.Margin = new Padding(4, 4, 4, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(612, 31);
            txtSearch.TabIndex = 25;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Arial", 9F);
            label5.Location = new Point(484, 66);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(77, 21);
            label5.TabIndex = 23;
            label5.Text = "Kategori";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial", 9F);
            label6.Location = new Point(326, 66);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(98, 21);
            label6.TabIndex = 22;
            label6.Text = "Departman";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial", 9F);
            label7.Location = new Point(169, 66);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(93, 21);
            label7.TabIndex = 21;
            label7.Text = "Bitiş Tarihi";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Arial", 9F);
            label8.Location = new Point(11, 66);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(139, 21);
            label8.TabIndex = 20;
            label8.Text = "Başlangıç Tarihi";
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Arial", 9F);
            btnClear.Location = new Point(628, 140);
            btnClear.Margin = new Padding(4, 4, 4, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(119, 38);
            btnClear.TabIndex = 19;
            btnClear.Text = "Temizle";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnFilter
            // 
            btnFilter.Font = new Font("Arial", 9F);
            btnFilter.Location = new Point(628, 86);
            btnFilter.Margin = new Padding(4, 4, 4, 4);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(119, 38);
            btnFilter.TabIndex = 18;
            btnFilter.Text = "Filtrele";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // cmbCategory
            // 
            cmbCategory.Font = new Font("Arial", 9F);
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(484, 91);
            cmbCategory.Margin = new Padding(4, 4, 4, 4);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(139, 29);
            cmbCategory.TabIndex = 17;
            // 
            // cmbDepartment
            // 
            cmbDepartment.Font = new Font("Arial", 9F);
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Location = new Point(326, 91);
            cmbDepartment.Margin = new Padding(4, 4, 4, 4);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(139, 29);
            cmbDepartment.TabIndex = 16;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Font = new Font("Arial", 9F);
            dtpEndDate.Location = new Point(169, 91);
            dtpEndDate.Margin = new Padding(4, 4, 4, 4);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(139, 28);
            dtpEndDate.TabIndex = 15;
            // 
            // dtpStartDate
            // 
            dtpStartDate.Font = new Font("Arial", 9F);
            dtpStartDate.Location = new Point(11, 91);
            dtpStartDate.Margin = new Padding(4, 4, 4, 4);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(139, 28);
            dtpStartDate.TabIndex = 14;
            // 
            // dgvDocuments
            // 
            dgvDocuments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDocuments.Dock = DockStyle.Fill;
            dgvDocuments.Location = new Point(0, 0);
            dgvDocuments.Margin = new Padding(4, 4, 4, 4);
            dgvDocuments.Name = "dgvDocuments";
            dgvDocuments.RowHeadersWidth = 51;
            dgvDocuments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDocuments.Size = new Size(1000, 562);
            dgvDocuments.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 562);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);
            Controls.Add(dgvDocuments);
            Margin = new Padding(4, 4, 4, 4);
            Name = "Form1";
            Text = "Form1";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
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
        private Label lblUserIconA;
        private DateTimePicker dtpEndDate;
        private DateTimePicker dtpStartDate;
        private PictureBox lblUserIcon;
        private Panel panel1;
        private TextBox txtSearch;
        private Button btnSearch;
        private Label lblRecordCount;
        private Label lblSelectionCount;
    }
}
