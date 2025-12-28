namespace DocumentManagementSystem
{
    partial class FrmCopKutusu
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
            dgvTrash = new DataGridView();
            panel1 = new Panel();
            btnRestore = new DataGridViewButtonColumn();
            btnHardDelete = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)dgvTrash).BeginInit();
            panel1.SuspendLayout();
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
            // dgvTrash
            // 
            dgvTrash.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTrash.Columns.AddRange(new DataGridViewColumn[] { btnRestore, btnHardDelete });
            dgvTrash.Location = new Point(0, 60);
            dgvTrash.Name = "dgvTrash";
            dgvTrash.ReadOnly = true;
            dgvTrash.RowHeadersWidth = 51;
            dgvTrash.Size = new Size(800, 389);
            dgvTrash.TabIndex = 1;
            dgvTrash.CellContentClick += dgvTrash_CellContentClick;
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 54);
            panel1.TabIndex = 2;
            // 
            // btnRestore
            // 
            btnRestore.HeaderText = "Geri Getir";
            btnRestore.MinimumWidth = 6;
            btnRestore.Name = "btnRestore";
            btnRestore.ReadOnly = true;
            btnRestore.Text = "🔄";
            btnRestore.UseColumnTextForButtonValue = true;
            btnRestore.Width = 125;
            // 
            // btnHardDelete
            // 
            btnHardDelete.HeaderText = "Kalıcı Sil";
            btnHardDelete.MinimumWidth = 6;
            btnHardDelete.Name = "btnHardDelete";
            btnHardDelete.ReadOnly = true;
            btnHardDelete.Text = "🚮";
            btnHardDelete.UseColumnTextForButtonValue = true;
            btnHardDelete.Width = 125;
            // 
            // FrmCopKutusu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(dgvTrash);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCopKutusu";
            Text = "Çöp Kutusu";
            ((System.ComponentModel.ISupportInitialize)dgvTrash).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private DataGridView dgvTrash;
        private Panel panel1;
        private DataGridViewButtonColumn btnRestore;
        private DataGridViewButtonColumn btnHardDelete;
    }
}