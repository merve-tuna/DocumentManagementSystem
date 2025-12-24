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
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DocumentManagementSystem;Integrated Security=True;TrustServerCertificate=True";

        // --- DEĞİŞKENLER ---
        private int _currentUserId = 1;
        private string _currentUserRole = "Admin";

        private string selectedFilePath = "";
        private bool hasUnsavedChanges = false;

        public FrmBelgeEkle()
        {
            InitializeComponent();
            SetupUI();
            LoadComboBoxes();
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
       




        // Belge Kaydetme Metodu
        private void SaveDocument() // Bu metodun adını değiştirmeyin, içeriğini güncelleyin
        {
            // 1. Validasyonlar
            if (string.IsNullOrWhiteSpace(txtDocName.Text) ||
                cmbDepartment.SelectedIndex == -1 ||
                cmbCategory.SelectedIndex == -1 ||
                string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz ve bir dosya seçiniz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. SQL İşlemleri
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    int statusId = (_currentUserRole == "Admin") ? 3 : 2;

                    string query = @"INSERT INTO Documents 
                            (DocumentName, DepartmentID, CategoryID, Description, FilePath, UploadedByUserID, StatusID, UploadDate) 
                            VALUES 
                            (@DocumentName, @DepartmentName, @CategoryName, @Description, @FilePath, @UserName, @StatusName, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DocumentName", txtDocName.Text);
                        cmd.Parameters.AddWithValue("@DepartmentName", cmbDepartment.SelectedValue);
                        cmd.Parameters.AddWithValue("@CategoryName", cmbCategory.SelectedValue);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@FilePath", selectedFilePath);
                        cmd.Parameters.AddWithValue("@UserName", _currentUserId);
                        cmd.Parameters.AddWithValue("@StatusName", statusId);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Belge başarıyla veritabanına kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Ekranı temizle ve butonları ayarla
                            btnAction.Enabled = false;
                            btnClear.Enabled = true;
                            hasUnsavedChanges = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
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
            private void LoadComboBoxes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // --- DEPARTMANLARI YÜKLE ---
                    // DİKKAT: SQL sorgusunda 'DepartmentID' yazdığından emin olun.
                    // Eğer veritabanınızda bu sütunun adı 'ID' ise burayı düzeltmelisiniz.
                    SqlDataAdapter daDept = new SqlDataAdapter("SELECT DepartmentID, DepartmentName FROM Departments WHERE IsActive=1", conn);
                    DataTable dtDept = new DataTable();
                    daDept.Fill(dtDept);

                    // ÖNCE AYARLARI YAP
                    cmbDepartment.DisplayMember = "DepartmentName"; // Ekranda görünecek
                    cmbDepartment.ValueMember = "DepartmentID";     // Arkada tutulacak ID

                    // SONRA VERİYİ VER
                    cmbDepartment.DataSource = dtDept;
                    cmbDepartment.SelectedIndex = -1; // Seçimi temizle

                    // --- KATEGORİLERİ YÜKLE ---
                    SqlDataAdapter daCat = new SqlDataAdapter("SELECT CategoryID, CategoryName FROM Categories WHERE IsActive=1", conn);
                    DataTable dtCat = new DataTable();
                    daCat.Fill(dtCat);

                    cmbCategory.DisplayMember = "CategoryName";
                    cmbCategory.ValueMember = "CategoryID";
                    cmbCategory.DataSource = dtCat;
                    cmbCategory.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri yükleme hatası: " + ex.Message);
                }
            }
        }
    }
    }

