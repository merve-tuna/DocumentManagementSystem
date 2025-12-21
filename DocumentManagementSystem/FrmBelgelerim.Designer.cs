namespace DocumentManagementSystem
{
    partial class FrmBelgelerim
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
            button1 = new Button();
            pnlTopMenu = new Panel();
            btnDelete = new Button();
            btnPrint = new Button();
            btnUpdate = new Button();
            btnDownload = new Button();
            dgvMyDocs = new DataGridView();
            pnlTopMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMyDocs).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(95, 30);
            button1.TabIndex = 0;
            button1.Text = "Geri";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pnlTopMenu
            // 
            pnlTopMenu.Controls.Add(btnDelete);
            pnlTopMenu.Controls.Add(btnPrint);
            pnlTopMenu.Controls.Add(btnUpdate);
            pnlTopMenu.Controls.Add(btnDownload);
            pnlTopMenu.Controls.Add(button1);
            pnlTopMenu.Dock = DockStyle.Top;
            pnlTopMenu.Location = new Point(0, 0);
            pnlTopMenu.Name = "pnlTopMenu";
            pnlTopMenu.Size = new Size(800, 111);
            pnlTopMenu.TabIndex = 1;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(583, 61);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "button5";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(436, 61);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(94, 29);
            btnPrint.TabIndex = 10;
            btnPrint.Text = "button4";
            btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(311, 60);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 29);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "button2";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDownload
            // 
            btnDownload.Location = new Point(13, 60);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(94, 29);
            btnDownload.TabIndex = 7;
            btnDownload.Text = "button2";
            btnDownload.UseVisualStyleBackColor = true;
            // 
            // dgvMyDocs
            // 
            dgvMyDocs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMyDocs.Dock = DockStyle.Fill;
            dgvMyDocs.Location = new Point(0, 111);
            dgvMyDocs.Name = "dgvMyDocs";
            dgvMyDocs.RowHeadersWidth = 51;
            dgvMyDocs.Size = new Size(800, 339);
            dgvMyDocs.TabIndex = 2;
            // 
            // FrmBelgelerim
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvMyDocs);
            Controls.Add(pnlTopMenu);
            Name = "FrmBelgelerim";
            Text = "FrmBelgelerim";
            pnlTopMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMyDocs).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Panel pnlTopMenu;
        private DataGridView dgvMyDocs;
        private Button btnDelete;
        private Button btnPrint;
        private Button btnUpdate;
        private Button btnDownload;
    }
}