namespace DocumentManagementSystem
{
    partial class FrmBelgeEkle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBelgeEkle));
            btnBack = new Button();
            pnlDropZone = new Panel();
            label6 = new Label();
            pictureBox1 = new PictureBox();
            lblSelectedFile = new Label();
            panel1 = new Panel();
            btnDraft = new Button();
            txtDescription = new RichTextBox();
            cmbDepartment = new ComboBox();
            btnClear = new Button();
            cmbCategory = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtDocName = new TextBox();
            btnAction = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            label5 = new Label();
            pnlDropZone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(8, 15);
            btnBack.Margin = new Padding(4, 4, 4, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(119, 38);
            btnBack.TabIndex = 0;
            btnBack.Text = "Geri";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += button1_Click;
            // 
            // pnlDropZone
            // 
            pnlDropZone.AllowDrop = true;
            pnlDropZone.BorderStyle = BorderStyle.FixedSingle;
            pnlDropZone.Controls.Add(label6);
            pnlDropZone.Controls.Add(pictureBox1);
            pnlDropZone.Location = new Point(4, 100);
            pnlDropZone.Margin = new Padding(4, 4, 4, 4);
            pnlDropZone.Name = "pnlDropZone";
            pnlDropZone.Size = new Size(418, 318);
            pnlDropZone.TabIndex = 1;
            pnlDropZone.Click += pnlDropZone_Click;
            pnlDropZone.Paint += panel1_Paint;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(92, 191);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(217, 75);
            label6.TabIndex = 1;
            label6.Text = "Belgeyi Buraya Sürükleyin \r\nveya \r\nTıklayıp Seçin";
            label6.TextAlign = ContentAlignment.TopCenter;
            label6.Click += pnlDropZone_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.ErrorImage = (Image)resources.GetObject("pictureBox1.ErrorImage");
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(125, 106);
            pictureBox1.Margin = new Padding(4, 4, 4, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(162, 81);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pnlDropZone_Click;
            // 
            // lblSelectedFile
            // 
            lblSelectedFile.AutoSize = true;
            lblSelectedFile.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblSelectedFile.Location = new Point(4, 422);
            lblSelectedFile.Margin = new Padding(4, 0, 4, 0);
            lblSelectedFile.Name = "lblSelectedFile";
            lblSelectedFile.Size = new Size(59, 25);
            lblSelectedFile.TabIndex = 2;
            lblSelectedFile.Text = "label1";
            lblSelectedFile.Click += lblSelectedFile_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnDraft);
            panel1.Controls.Add(txtDescription);
            panel1.Controls.Add(cmbDepartment);
            panel1.Controls.Add(btnClear);
            panel1.Controls.Add(cmbCategory);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtDocName);
            panel1.Controls.Add(btnAction);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(468, 0);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(532, 562);
            panel1.TabIndex = 3;
            // 
            // btnDraft
            // 
            btnDraft.Location = new Point(84, 510);
            btnDraft.Margin = new Padding(4, 4, 4, 4);
            btnDraft.Name = "btnDraft";
            btnDraft.Size = new Size(150, 38);
            btnDraft.TabIndex = 14;
            btnDraft.Text = "Taslağa Kaydet";
            btnDraft.UseVisualStyleBackColor = true;
            btnDraft.Click += btnDraft_Click;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(8, 301);
            txtDescription.Margin = new Padding(4, 4, 4, 4);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(478, 116);
            txtDescription.TabIndex = 13;
            txtDescription.Text = "";
            // 
            // cmbDepartment
            // 
            cmbDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Location = new Point(8, 166);
            cmbDepartment.Margin = new Padding(4, 4, 4, 4);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(478, 33);
            cmbDepartment.TabIndex = 12;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(241, 510);
            btnClear.Margin = new Padding(4, 4, 4, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(119, 38);
            btnClear.TabIndex = 4;
            btnClear.Text = "Temizle";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(8, 234);
            cmbCategory.Margin = new Padding(4, 4, 4, 4);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(478, 33);
            cmbCategory.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 272);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(83, 25);
            label4.TabIndex = 10;
            label4.Text = "Açıklama";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 205);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(91, 25);
            label3.TabIndex = 9;
            label3.Text = "Kategori *";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 138);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(114, 25);
            label2.TabIndex = 8;
            label2.Text = "Departman *";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 71);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 25);
            label1.TabIndex = 7;
            label1.Text = "Belge Adı *";
            label1.Click += label1_Click;
            // 
            // txtDocName
            // 
            txtDocName.Location = new Point(4, 100);
            txtDocName.Margin = new Padding(4, 4, 4, 4);
            txtDocName.Name = "txtDocName";
            txtDocName.Size = new Size(482, 31);
            txtDocName.TabIndex = 6;
            // 
            // btnAction
            // 
            btnAction.Location = new Point(368, 510);
            btnAction.Margin = new Padding(4, 4, 4, 4);
            btnAction.Name = "btnAction";
            btnAction.Size = new Size(119, 38);
            btnAction.TabIndex = 5;
            btnAction.Text = "btnSave";
            btnAction.UseVisualStyleBackColor = true;
            btnAction.Click += btnAction_Click;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(4, 4, 4, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(38, 562);
            panel2.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnBack);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(pnlDropZone);
            panel3.Controls.Add(lblSelectedFile);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(38, 0);
            panel3.Margin = new Padding(4, 4, 4, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(430, 562);
            panel3.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label5.Location = new Point(4, 65);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(116, 30);
            label5.TabIndex = 3;
            label5.Text = "Belge Ekle";
            // 
            // FrmBelgeEkle
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 562);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(4, 4, 4, 4);
            Name = "FrmBelgeEkle";
            Text = "FrmBelgeEkle";
            pnlDropZone.ResumeLayout(false);
            pnlDropZone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnBack;
        private Panel pnlDropZone;
        private PictureBox pictureBox1;
        private Label lblSelectedFile;
        private Panel panel1;
        private Button btnAction;
        private Button btnClear;
        private Panel panel2;
        private Panel panel3;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtDocName;
        private Label label5;
        private RichTextBox txtDescription;
        private ComboBox cmbDepartment;
        private ComboBox cmbCategory;
        private Button btnDraft;
        private Label label6;
    }
}