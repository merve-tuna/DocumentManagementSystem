using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentManagementSystem
{
    public partial class FrmBelgelerim : Form
    {
        public FrmBelgelerim()
        {
            InitializeComponent();
            SetupGrid();
            LoadDummyData();
        }

//

        private void SetupGrid()
        {
            // Grid kolonlarını manuel oluşturuyoruz
            dgvMyDocs.ColumnCount = 4;
            dgvMyDocs.Columns[0].Name = "ID";
            dgvMyDocs.Columns[0].Visible = false; // ID gizli olsun
            dgvMyDocs.Columns[1].Name = "Belge Adı";
            dgvMyDocs.Columns[2].Name = "Tarih";
            dgvMyDocs.Columns[3].Name = "Durum"; // Taslak, Onaylandı vs.

            dgvMyDocs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMyDocs.ReadOnly = true;
            dgvMyDocs.AllowUserToAddRows = false;
        }

        private void LoadDummyData()
        {
            // Grid'e sahte veriler ekliyoruz
            dgvMyDocs.Rows.Add("1", "Proje Gereksinimleri.docx", "20.12.2025", "Taslak");
            dgvMyDocs.Rows.Add("2", "Yıllık Bütçe Raporu.xlsx", "18.12.2025", "Onaylandı");
            dgvMyDocs.Rows.Add("3", "Eski Logo Tasarımı.png", "10.11.2025", "Silindi");
            dgvMyDocs.Rows.Add("4", "Müşteri Teklifi v1.pdf", "15.12.2025", "Red");
            dgvMyDocs.Rows.Add("5", "İK Yönetmeliği.pdf", "19.12.2025", "Onay Bekliyor");

            // Renklendirme (Görsellik için)
            ColorizeRows();
        }

        private void ColorizeRows()
        {
            foreach (DataGridViewRow row in dgvMyDocs.Rows)
            {
                string status = row.Cells["Durum"].Value.ToString();

                // Duruma göre satır veya hücre rengini değiştirme
                if (status == "Onaylandı") row.Cells["Durum"].Style.ForeColor = Color.Green;
                else if (status == "Red") row.Cells["Durum"].Style.ForeColor = Color.Red;
                else if (status == "Silindi") row.Cells["Durum"].Style.ForeColor = Color.Gray;
                else if (status == "Taslak") row.Cells["Durum"].Style.ForeColor = Color.Blue;
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
        private void PerformAction(string actionName)
        {
            if (dgvMyDocs.SelectedRows.Count > 0)
            {
                string docName = dgvMyDocs.SelectedRows[0].Cells["Belge Adı"].Value.ToString();
                MessageBox.Show($"{docName} için '{actionName}' işlemi yapılıyor.", "İşlem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)

        {

            this.Close(); // Bu sayfayı kapatır (Böylece ana sayfa otomatik açılır)

        }
    }
}