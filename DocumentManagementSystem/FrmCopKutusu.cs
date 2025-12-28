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
using static System.Collections.Specialized.BitVector32;

namespace DocumentManagementSystem
{
    public partial class FrmCopKutusu : Form
    {
      
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DocumentManagementSystem;Integrated Security=True;TrustServerCertificate=True";

        public FrmCopKutusu()
        {
            InitializeComponent();
            LoadTrashData();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void FrmCopKutusu_Load(object sender, EventArgs e)
        {
            LoadTrashData();
        }


        private void LoadTrashData()
        {
            // Veritabanı bağlantı sınıfını kullandığını varsayıyorum (SqlHelper veya benzeri)
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetDeletedDocuments", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvTrash.DataSource = dt;

                    if (dgvTrash.Columns["DocumentID"] != null) dgvTrash.Columns["DocumentID"].Visible = false;
                    if (dgvTrash.Columns["DeletedByUserID"] != null) dgvTrash.Columns["DeletedByUserID"].Visible = false;
                    if (dgvTrash.Columns["DeleterRoleID"] != null) dgvTrash.Columns["DeleterRoleID"].Visible = false;

                    dgvTrash.Columns["DocumentID"].Visible = false;
                    dgvTrash.Columns["DocumentName"].HeaderText = "Belge Adı";
                    dgvTrash.Columns["DeletedBy"].HeaderText = "Silen Kişi";
                    dgvTrash.Columns["DepartmentName"].HeaderText = "Departman";
                    dgvTrash.Columns["CategoryName"].HeaderText = "Kategori";
                    dgvTrash.Columns["DeletedDate"].HeaderText = "Silinme Tarihi";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }


        private void dgvTrash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sadece satır içi tıklamaları al (Header hariç)
            if (e.RowIndex < 0) return;

            // Tıklanan sütunun adı (Buton mu kontrolü)
            string colName = dgvTrash.Columns[e.ColumnIndex].Name;

            // Buton değilse işlem yapma
            if (colName != "btnRestore" && colName != "btnHardDelete") return;

            // Seçilen satırdaki verileri al
            int documentId = Convert.ToInt32(dgvTrash.Rows[e.RowIndex].Cells["DocumentID"].Value);

            // ÖNEMLİ: RoleID = 3 (ÜRETİCİ) hiçbir işlem yapamaz - EN BAŞTA KONTROL ET
            if (UserSession.RoleId == UserSession.CALISAN_ID)
            {
                MessageBox.Show("Çalışan rolündeki kullanıcılar Çöp Kutusu işlemi yapamaz!",
                                "Yetkisiz İşlem",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
                return;
            }

            // 1. GERİ YÜKLEME
            if (colName == "btnRestore")
            {
                if (MessageBox.Show("Bu belgeyi geri yüklemek istiyor musunuz?",
                                    "Onay",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ExecuteProcedure("sp_RestoreDocument", documentId, UserSession.UserId);
                    LoadTrashData();
                }
            }
            // 2. KALICI SİLME
            else if (colName == "btnHardDelete")
            {
                if (MessageBox.Show("DİKKAT! Bu belge tamamen silinecek. Onaylıyor musunuz?",
                                    "Kritik Uyarı",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    ExecuteProcedure("sp_HardDeleteDocument", documentId, UserSession.UserId);
                    LoadTrashData();
                }
            }
        }


        private void ExecuteProcedure(string spName, int docId, int? userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(spName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DocumentID", docId);

                    if (userId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@CurrentUserID", userId.Value);
                    }

                    // ExecuteScalar ile SQL'den dönen Result sonucunu yakalıyoruz
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        int resultCode = Convert.ToInt32(result);
                        if (resultCode == 0)
                        {
                            MessageBox.Show("Bu işlem için yetkiniz bulunmuyor!",
                                            "Yetkisiz İşlem",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        }
                        else if (resultCode == 1)
                        {
                            MessageBox.Show("Tamalandı.",
                                            "İşlem Durumu",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İşlem hatası: " + ex.Message,
                                    "Hata",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }
    }

}
