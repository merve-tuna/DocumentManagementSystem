using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentManagementSystem
{
    public partial class FrmBelgeEkle : Form
    {
        // --- DEĞİŞKENLER ---
        private int _currentUserId = 1;
        private string _currentUserRole = "Admin";

        private string selectedFilePath = "";
        private bool hasUnsavedChanges = false;

        public FrmBelgeEkle()
        {
            InitializeComponent();
            SetupUI();
            LoadDepartments();
            LoadCategories();
        }

        // --- BAŞLANGIÇ AYARLARI ---
        private void SetupUI()
        {
            // Panel özelliklerini aç
            pnlDropZone.AllowDrop = true; // Sürükle bırak için bu ZORUNLUDUR

            // Event Bağlantıları
   
            this.FormClosing += FrmBelgeEkle_FormClosing;

            // Sürükle Bırak Eventleri
            pnlDropZone.DragEnter += pnlDropZone_DragEnter; // İçeri girince
            pnlDropZone.DragLeave += pnlDropZone_DragLeave; // Sürüklerken vazgeçip dışarı çıkarsa
            pnlDropZone.DragDrop += pnlDropZone_DragDrop;   // Bırakınca
            pnlDropZone.Click += pnlDropZone_Click;         // Tıklayınca

            // Diğer butonlar
            btnAction.Click += btnAction_Click;
            btnClear.Click += btnClear_Click;

            // Değişiklik takibi
            if (txtDocName != null) txtDocName.TextChanged += (s, e) => hasUnsavedChanges = true;
            if (cmbDepartment != null) cmbDepartment.SelectedIndexChanged += (s, e) => hasUnsavedChanges = true;
        }


        // 2. FORM KAPANIRKEN ÇALIŞACAK METOD (FormClosing Hatası İçin)
        // Dikkat: Burada 'FormClosingEventArgs' kullanılmalıdır.
        private void FrmBelgeEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasUnsavedChanges)
            {
                DialogResult dr = MessageBox.Show(
                    "Kaydetmediğiniz değişiklikler var. Çıkmak istediğinize emin misiniz?",
                    "Uyarı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dr == DialogResult.No)
                {
                    e.Cancel = true; // Kapatmayı iptal et
                }
            }
        }

        // 3. TEMİZLE BUTONU METODU (btnClear Hatası İçin)
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDocName.Clear();
            txtDescription.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbCategory.SelectedIndex = -1;
            selectedFilePath = "";
            lblSelectedFile.Text = "Dosya Seçilmedi";
            lblSelectedFile.ForeColor = Color.Black;

            btnAction.Enabled = true;
            btnClear.Enabled = false;
            hasUnsavedChanges = false;
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

        // --- SÜRÜKLE BIRAK & DOSYA SEÇME ---
        // 1. SÜRÜKLEME BAŞLADIĞINDA (Kullanıcı dosyayı panelin üzerine getirdi)
        private void pnlDropZone_DragEnter(object sender, DragEventArgs e)
        {
            // Eğer sürüklenen şey bir dosya ise ikonunu değiştir
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                pnlDropZone.BackColor = Color.LightBlue; // Görsel efekt: Renk değişsin
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // 2. SÜRÜKLEME İPTAL OLURSA (Kullanıcı panelin üzerine geldi ama bırakmadan çıktı)
        private void pnlDropZone_DragLeave(object sender, EventArgs e)
        {
            pnlDropZone.BackColor = Color.WhiteSmoke; // Rengi eski haline döndür (veya panelin orijinal rengi neyse)
        }

        // 3. DOSYA BIRAKILDIĞINDA
        private void pnlDropZone_DragDrop(object sender, DragEventArgs e)
        {
            pnlDropZone.BackColor = Color.WhiteSmoke; // Rengi düzelt

            // Dosyaları al
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                // Sadece ilk dosyayı alıyoruz (Çoklu yükleme yapmayacaksak)
                SelectFile(files[0]);
            }
        }

        // 4. PANELE TIKLANIP DOSYA SEÇİLDİĞİNDE
        private void pnlDropZone_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Yüklenecek Belgeyi Seçin";
                // Kullanıcının sadece belirli dosyaları görmesini sağla
                ofd.Filter = "Belgeler|*.pdf;*.docx;*.doc;*.xlsx;*.xls;*.txt|Resimler|*.jpg;*.jpeg;*.png|Tüm Dosyalar|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    SelectFile(ofd.FileName);
                }
            }
        }

        // ORTAK DOSYA İŞLEME METODU (Değişmedi, aynen kalabilir)
        private void SelectFile(string path)
        {
            selectedFilePath = path;

            // Dosya adını ekrana yaz
            lblSelectedFile.Text = Path.GetFileName(path);
            lblSelectedFile.ForeColor = Color.Green;

            // Otomatik olarak belge adı boşsa, dosya adını oraya da yazabiliriz (İsteğe bağlı)
            if (string.IsNullOrWhiteSpace(txtDocName.Text))
            {
                txtDocName.Text = Path.GetFileNameWithoutExtension(path);
            }

            hasUnsavedChanges = true;
        }
    }
}
