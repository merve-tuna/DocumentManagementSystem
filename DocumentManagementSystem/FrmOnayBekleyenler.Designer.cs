namespace DocumentManagementSystem
{
    partial class FrmOnayBekleyenler
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
            panel1 = new Panel();
            dgvOnayBekleyenler = new DataGridView();
            btnOnayla = new DataGridViewButtonColumn();
            btnReddet = new DataGridViewButtonColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOnayBekleyenler).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "Geri";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 54);
            panel1.TabIndex = 1;
            // 
            // dgvOnayBekleyenler
            // 
            dgvOnayBekleyenler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOnayBekleyenler.Columns.AddRange(new DataGridViewColumn[] { btnOnayla, btnReddet });
            dgvOnayBekleyenler.Location = new Point(0, 60);
            dgvOnayBekleyenler.MultiSelect = false;
            dgvOnayBekleyenler.Name = "dgvOnayBekleyenler";
            dgvOnayBekleyenler.ReadOnly = true;
            dgvOnayBekleyenler.RowHeadersWidth = 51;
            dgvOnayBekleyenler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOnayBekleyenler.Size = new Size(800, 396);
            dgvOnayBekleyenler.TabIndex = 2;
            // 
            // btnOnayla
            // 
            btnOnayla.HeaderText = "Onayla";
            btnOnayla.MinimumWidth = 6;
            btnOnayla.Name = "btnOnayla";
            btnOnayla.ReadOnly = true;
            btnOnayla.Width = 125;
            // 
            // btnReddet
            // 
            btnReddet.HeaderText = "Reddet";
            btnReddet.MinimumWidth = 6;
            btnReddet.Name = "btnReddet";
            btnReddet.ReadOnly = true;
            btnReddet.Width = 125;
            // 
            // FrmOnayBekleyenler
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvOnayBekleyenler);
            Controls.Add(panel1);
            MinimizeBox = false;
            Name = "FrmOnayBekleyenler";
            Text = "Onay Sayfası";
            Load += FrmOnayBekleyenler_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOnayBekleyenler).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Panel panel1;
        private DataGridView dgvOnayBekleyenler;
        private DataGridViewButtonColumn btnOnayla;
        private DataGridViewButtonColumn btnReddet;
    }
}