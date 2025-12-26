using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentManagementSystem
{
    public enum DocumentStatus
    {
        Taslak = 1,
        EditörOnayıBekleniyor = 2,
        AdminOnayıBekleniyor = 3,
        Yayında = 4,
        Reddedildi = 5,
        İmhaEdildi = 6

    }
    public partial class FrmBelgelerim : Form
    {
        public FrmBelgelerim()
        {
            InitializeComponent();
            // SetupGrid(); // Eğer grid ayarların (ReadOnly vb.) lazımsa kalsın
            LoadRealData(); // DummyData yerine artık bu çalışacak
        }

        //


        private void ColorizeRows()
        {
            foreach (DataGridViewRow row in dgvMyDocs.Rows)
            {
                // Artık StatusName ile kontrol ediyoruz
                if (row.Cells["StatusName"].Value == null) continue;

                string status = row.Cells["StatusName"].Value.ToString();

                if (status == "Yayında")
                    row.Cells["StatusName"].Style.ForeColor = Color.Green;
                else if (status == "Reddedildi")
                    row.Cells["StatusName"].Style.ForeColor = Color.Red;
                else if (status == "Taslak")
                    row.Cells["StatusName"].Style.ForeColor = Color.Blue;
                else if (status == "Editör Onayı Bekleniyor" || status == "Admin Onayı Bekleniyor")
                    row.Cells["StatusName"].Style.ForeColor = Color.Orange;
            }
        }

        private void LoadRealData()
        {
            try
            {
                SqlParameter[] p = { new SqlParameter("@UserID", UserSession.UserId) };
                DataTable dt = SqlHelper.GetDataByProcedure("sp_GetMyDocuments", p);

                if (dt != null)
                {
                    dgvMyDocs.DataSource = dt; // Veriyi bağla

                    // BURADA ÇAĞIRMALISIN:
                    ConfigureMyDocsGrid();

                    ColorizeRows(); // Renklendirmeyi de ardından yapabilirsin
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void SetHeader(DataGridView dgv, string dbColumnName, string headerText, bool isVisible)
        {
            if (dgv.Columns.Contains(dbColumnName))
            {
                dgv.Columns[dbColumnName].HeaderText = headerText;
                dgv.Columns[dbColumnName].Visible = isVisible;
            }
        }

        private void ConfigureMyDocsGrid()
        {
            if (dgvMyDocs == null || dgvMyDocs.Columns.Count == 0) return;

            try
            {
                // Artık İngilizce isimlerle çalış
                SetHeader(dgvMyDocs, "DocumentName", "Belge Adı", true);
                SetHeader(dgvMyDocs, "StatusName", "Durum", true);

                if (dgvMyDocs.Columns.Contains("UploadDate"))
                {
                    dgvMyDocs.Columns["UploadDate"].HeaderText = "Tarih";
                    dgvMyDocs.Columns["UploadDate"].DefaultCellStyle.Format = "dd.MM.yyyy";
                }

                dgvMyDocs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Grid ayarlanırken hata: {ex.Message}");
            }
        }





        // Bir satır seçildiğinde çalışır
        //
        // Çift tıklama (İçine girme)
        private void DgvMyDocs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string docName = dgvMyDocs.Rows[e.RowIndex].Cells["Belge Adı"].Value.ToString();
                // Burada detay sayfasını açabilirsin veya belgeyi açabilirsin
                MessageBox.Show($"{docName} dosyasının detayına giriliyor...", "Detay", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Buton aksiyonları için yardımcı metod
        //private void PerformAction(string actionName)
        //{
        //    if (dgvMyDocs.SelectedRows.Count > 0)
        //    {
        //        string docName = dgvMyDocs.SelectedRows[0].Cells["Belge Adı"].Value.ToString();
        //        MessageBox.Show($"{docName} için '{actionName}' işlemi yapılıyor.", "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)

        {

            this.Close(); // Bu sayfayı kapatır (Böylece ana sayfa otomatik açılır)

        }


    }
}