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
            pnlTopMenu.Controls.Add(button1);
            pnlTopMenu.Dock = DockStyle.Top;
            pnlTopMenu.Location = new Point(0, 0);
            pnlTopMenu.Name = "pnlTopMenu";
            pnlTopMenu.Size = new Size(800, 54);
            pnlTopMenu.TabIndex = 1;
            // 
            // dgvMyDocs
            // 
            dgvMyDocs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMyDocs.Dock = DockStyle.Fill;
            dgvMyDocs.Location = new Point(0, 54);
            dgvMyDocs.Name = "dgvMyDocs";
            dgvMyDocs.RowHeadersWidth = 51;
            dgvMyDocs.Size = new Size(800, 396);
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
    }
}