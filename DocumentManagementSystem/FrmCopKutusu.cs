using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentManagementSystem
{
    public partial class FrmCopKutusu : Form
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DocumentManagementSystem;Integrated Security=True;TrustServerCertificate=True";
        public FrmCopKutusu()
        {
            InitializeComponent();
        }

        private void FrmCopKutusu_Load(object sender, EventArgs e)
        {
            LoadTrashData();
            CustomizeTrashGrid(); // Grid ayarlarını yapalım
        }

        private void LoadTrashData()
        {
            try
            {
                SqlParameter[] p = { new SqlParameter("@UserID", UserSession.UserId) };
                DataTable dt = SqlHelper.GetDataByProcedure("sp_GetRecycleBinDocuments", p);

                // --- DEĞİŞİKLİK BURADA ---
                // 1. Grid'in tasarım ayarlarını sıfırla (Böylece veriyi olduğu gibi kabul eder)
                dgvTrash.AutoGenerateColumns = true;
                dgvTrash.Columns.Clear();

                // 2. Veriyi bas
                dgvTrash.DataSource = dt;

                // 3. Veri var mı kontrol et (Emin olmak için)
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Veritabanından veri döndü ama liste BOŞ (0 kayıt).");
                }
                else
                {
                    // Veri geldikten sonra başlıkları ve gizlemeyi kodla yap
                    // ID'yi gizle
                    if (dgvTrash.Columns.Contains("DocumentID"))
                        dgvTrash.Columns["DocumentID"].Visible = false;

                    // Başlıkları düzelt
                    if (dgvTrash.Columns.Contains("DocumentName"))
                        dgvTrash.Columns["DocumentName"].HeaderText = "Belge Adı";

                    if (dgvTrash.Columns.Contains("DeletedBy"))
                        dgvTrash.Columns["DeletedBy"].HeaderText = "Silen Kişi";

                    // Butonu tekrar ekle (Columns.Clear() butonu da siler çünkü)
                    AddRestoreButton();
                }
                // -------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // Buton ekleme işini ayrı bir metoda aldım, temiz olsun
        private void AddRestoreButton()
        {
            if (!dgvTrash.Columns.Contains("btnRestore"))
            {
                DataGridViewButtonColumn btnRestore = new DataGridViewButtonColumn();
                btnRestore.Name = "btnRestore";
                btnRestore.HeaderText = "İşlem";
                btnRestore.Text = "Geri Yükle";
                btnRestore.UseColumnTextForButtonValue = true;
                dgvTrash.Columns.Add(btnRestore);
            }
        }

        private void CustomizeTrashGrid()
        {
            // Grid boşsa hata vermemesi için kontrol
            if (dgvTrash.Columns.Count == 0) return;

            // Gereksiz kolonları gizle veya isimlendir
            if (dgvTrash.Columns.Contains("DocumentID"))
                dgvTrash.Columns["DocumentID"].Visible = false;

            if (dgvTrash.Columns.Contains("DocumentName"))
                dgvTrash.Columns["DocumentName"].HeaderText = "Belge Adı";

            if (dgvTrash.Columns.Contains("FileType"))
                dgvTrash.Columns["FileType"].HeaderText = "Dosya Türü";

            if (dgvTrash.Columns.Contains("DeletedDate"))
                dgvTrash.Columns["DeletedDate"].HeaderText = "Silinme Tarihi";

            if (dgvTrash.Columns.Contains("DeletedBy"))
                dgvTrash.Columns["DeletedBy"].HeaderText = "Silen Kişi";

            // Geri Yükle butonu eklemek istersen (Grid'in içinde yoksa):
            if (!dgvTrash.Columns.Contains("btnRestore"))
            {
                DataGridViewButtonColumn btnRestore = new DataGridViewButtonColumn();
                btnRestore.Name = "btnRestore";
                btnRestore.HeaderText = "İşlem";
                btnRestore.Text = "Geri Yükle";
                btnRestore.UseColumnTextForButtonValue = true;
                dgvTrash.Columns.Add(btnRestore);
            }
        }

        // Geri yükleme butonu tıklama olayı
        private void dgvTrash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvTrash.Columns[e.ColumnIndex].Name == "btnRestore")
            {
                // Burada geri yükleme kodları olacak
                int docId = Convert.ToInt32(dgvTrash.Rows[e.RowIndex].Cells["DocumentID"].Value);
                RestoreDocument(docId);
            }
        }

        private void RestoreDocument(int docId)
        {
            // Geri yükleme (IsDeleted = 0 yapma) işlemini buraya yazabilirsin.
            MessageBox.Show("Geri yükleme özelliği eklenecek. ID: " + docId);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); // Bu sayfayı kapatır (Böylece ana sayfa otomatik açılır)
        }


    }

}
