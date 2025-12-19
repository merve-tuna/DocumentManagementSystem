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
            pnlHeader = new Panel();
            dgvDocuments = new DataGridView();
            btnDocumentAdd = new Button();
            btnMyDocuments = new Button();
            btnPendingApproval = new Button();
            btnTrash = new Button();
            btnHelp = new Button();
            cmbUserRole = new ComboBox();
            lblUserIcon = new Label();
            dtpStartDate = new DateTimePicker();
            dtpEndDate = new DateTimePicker();
            cmbDepartment = new ComboBox();
            cmbCategory = new ComboBox();
            button1 = new Button();
            btnClear = new Button();
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
            pnlSidebar.Size = new Size(203, 450);
            pnlSidebar.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(btnClear);
            pnlHeader.Controls.Add(button1);
            pnlHeader.Controls.Add(cmbCategory);
            pnlHeader.Controls.Add(cmbDepartment);
            pnlHeader.Controls.Add(dtpEndDate);
            pnlHeader.Controls.Add(dtpStartDate);
            pnlHeader.Controls.Add(lblUserIcon);
            pnlHeader.Controls.Add(cmbUserRole);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(203, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(597, 115);
            pnlHeader.TabIndex = 1;
            // 
            // dgvDocuments
            // 
            dgvDocuments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDocuments.Dock = DockStyle.Fill;
            dgvDocuments.Location = new Point(203, 115);
            dgvDocuments.Name = "dgvDocuments";
            dgvDocuments.RowHeadersWidth = 51;
            dgvDocuments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDocuments.Size = new Size(597, 335);
            dgvDocuments.TabIndex = 2;
            // 
            // btnDocumentAdd
            // 
            btnDocumentAdd.Location = new Point(36, 86);
            btnDocumentAdd.Name = "btnDocumentAdd";
            btnDocumentAdd.Size = new Size(94, 29);
            btnDocumentAdd.TabIndex = 0;
            btnDocumentAdd.Text = "button1";
            btnDocumentAdd.UseVisualStyleBackColor = true;
            // 
            // btnMyDocuments
            // 
            btnMyDocuments.Location = new Point(50, 159);
            btnMyDocuments.Name = "btnMyDocuments";
            btnMyDocuments.Size = new Size(94, 29);
            btnMyDocuments.TabIndex = 1;
            btnMyDocuments.Text = "button2";
            btnMyDocuments.UseVisualStyleBackColor = true;
            // 
            // btnPendingApproval
            // 
            btnPendingApproval.Location = new Point(42, 219);
            btnPendingApproval.Name = "btnPendingApproval";
            btnPendingApproval.Size = new Size(94, 29);
            btnPendingApproval.TabIndex = 2;
            btnPendingApproval.Text = "button3";
            btnPendingApproval.UseVisualStyleBackColor = true;
            // 
            // btnTrash
            // 
            btnTrash.Location = new Point(69, 281);
            btnTrash.Name = "btnTrash";
            btnTrash.Size = new Size(94, 29);
            btnTrash.TabIndex = 3;
            btnTrash.Text = "button4";
            btnTrash.UseVisualStyleBackColor = true;
            // 
            // btnHelp
            // 
            btnHelp.Location = new Point(88, 327);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(94, 29);
            btnHelp.TabIndex = 4;
            btnHelp.Text = "button5";
            btnHelp.UseVisualStyleBackColor = true;
            // 
            // cmbUserRole
            // 
            cmbUserRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbUserRole.FormattingEnabled = true;
            cmbUserRole.Location = new Point(434, 12);
            cmbUserRole.Name = "cmbUserRole";
            cmbUserRole.Size = new Size(151, 28);
            cmbUserRole.TabIndex = 0;
            // 
            // lblUserIcon
            // 
            lblUserIcon.AutoSize = true;
            lblUserIcon.Location = new Point(333, 21);
            lblUserIcon.Name = "lblUserIcon";
            lblUserIcon.Size = new Size(65, 20);
            lblUserIcon.TabIndex = 1;
            lblUserIcon.Text = "Kullanıcı";
            // 
            // dtpStartDate
            // 
            dtpStartDate.Location = new Point(3, 50);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(250, 27);
            dtpStartDate.TabIndex = 2;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Location = new Point(3, 82);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(250, 27);
            dtpEndDate.TabIndex = 3;
            // 
            // cmbDepartment
            // 
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Location = new Point(259, 49);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(151, 28);
            cmbDepartment.TabIndex = 4;
            // 
            // cmbCategory
            // 
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(259, 87);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(151, 28);
            cmbCategory.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(469, 51);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 6;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(490, 82);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 29);
            btnClear.TabIndex = 7;
            btnClear.Text = "button2";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvDocuments);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);
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
        private Label lblUserIcon;
        private ComboBox cmbUserRole;
        private Button btnClear;
        private Button button1;
        private ComboBox cmbCategory;
        private ComboBox cmbDepartment;
        private DateTimePicker dtpEndDate;
        private DateTimePicker dtpStartDate;
    }
}
