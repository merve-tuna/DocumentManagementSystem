using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentManagementSystem
{
    public partial class FrmBelgeEkle : Form
    {
        public FrmBelgeEkle()
        {
            InitializeComponent();
            LoadDepartments();
            LoadCategories();
        }

        private void LoadDepartments()
        {
            try
            {
                // ComboBox'ı temizle
                cmbDepartment.Items.Clear();
                cmbDepartment.Items.Add("Seçiniz..."); // İlk boş seçenek

                // Stored Procedure kullanarak verileri çek
                DataTable dt = SqlHelper.GetDataByProcedure("sp_GetDepartments");

                foreach (DataRow row in dt.Rows)
                {
                    cmbDepartment.Items.Add(row["DepartmentName"].ToString());
                }

                // Varsayılan olarak ilk seçeneği seç
                if (cmbDepartment.Items.Count > 0)
                    cmbDepartment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Departmanlar yüklenirken hata: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                // ComboBox'ı temizle
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("Seçiniz..."); // İlk boş seçenek

                // Stored Procedure kullanarak verileri çek
                DataTable dt = SqlHelper.GetDataByProcedure("sp_GetCategories");

                foreach (DataRow row in dt.Rows)
                {
                    cmbCategory.Items.Add(row["CategoryName"].ToString());
                }

                // Varsayılan olarak ilk seçeneği seç
                if (cmbCategory.Items.Count > 0)
                    cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kategoriler yüklenirken hata: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Belge Kaydetme Metodu
        private void SaveDocument()
        {
            // Validasyon
            if (string.IsNullOrWhiteSpace(txtDocName.Text))
            {
                MessageBox.Show("Belge adı zorunludur!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocName.Focus();
                return;
            }

            if (cmbDepartment.SelectedIndex <= 0)
            {
                MessageBox.Show("Departman seçmelisiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDepartment.Focus();
                return;
            }

            try
            {
                MessageBox.Show("Belge kaydetme işlemi hazırlanıyor...",
                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Belge kaydedilirken hata: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); // Bu sayfayı kapatır (Böylece ana sayfa otomatik açılır)
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblSelectedFile_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }
    }
}
