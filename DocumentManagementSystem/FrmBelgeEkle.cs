
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DocumentManagementSystem
{
    public partial class FrmBelgeEkle : Form
    {
        // 1. BAĞLANTI ADRESİN (Kendi bilgisayar adına göre kontrol et)
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DocumentManagementSystem;Integrated Security=True;TrustServerCertificate=True";

        // --- DEĞİŞKENLER ---
        //private int _currentUserId = 1;
        //private string _currentUserRole = "Admin";

        private string selectedFilePath = "";
        private bool hasUnsavedChanges = false;

        public FrmBelgeEkle()
        {
            InitializeComponent();
            LoadComboBoxes();
            SetupUI();
            this.FormClosing += new FormClosingEventHandler(FrmBelgeEkle_FormClosing);

        }

        // --- BAŞLANGIÇ AYARLARI ---
        private void SetupUI()
        {
            pnlDropZone.AllowDrop = true;

            btnClear.Enabled = false; // Temizle butonu başta kapalı
            btnAction.Enabled = true; // Kaydet/Gönder butonu açık

            // Değişiklik takibi
            if (txtDocName != null) txtDocName.TextChanged += (s, e) => hasUnsavedChanges = true;
            if (cmbDepartment != null) cmbDepartment.SelectedIndexChanged += (s, e) => hasUnsavedChanges = true;
            if (cmbDepartment != null) cmbDepartment.SelectedIndexChanged += (s, e) => hasUnsavedChanges = true;

            lblSelectedFile.Visible = false;
        }

        // --- COMBOBOX DOLDURMA (EN ÖNEMLİ KISIM) ---
        private void LoadComboBoxes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. DEPARTMANLARI YÜKLE
                    // NOT: Veritabanında sütun adın 'ID' ise burayı 'ID' yap, 'DepartmentID' ise böyle kalsın.
                    SqlDataAdapter daDept = new SqlDataAdapter("SELECT DepartmentID, DepartmentName FROM Departments WHERE IsActive=1", conn);
                    DataTable dtDept = new DataTable();
                    daDept.Fill(dtDept);

                    cmbDepartment.DisplayMember = "DepartmentName"; // Ekranda görünen
                    cmbDepartment.ValueMember = "DepartmentID";     // Arkadaki ID (SQL'e bu gidecek)
                    cmbDepartment.DataSource = dtDept;
                    cmbDepartment.SelectedIndex = -1;

                    // 2. KATEGORİLERİ YÜKLE
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

        // --- KAYDETME İŞLEMİ ---

        private void SaveDocument()
        {
            // 1. Validasyonlar (Aynı kalıyor)
            if (string.IsNullOrWhiteSpace(txtDocName.Text) || cmbDepartment.SelectedIndex == -1 ||
                cmbCategory.SelectedIndex == -1 || string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Lütfen tüm zorunlu alanları doldurunuz.", "Uyarı");
                return;
            }

            try
            {

                // 2. Parametreleri SQL'deki isimlerle BİREBİR EŞLEŞTİRİYORUZ
                SqlParameter[] p = {
            new SqlParameter("@DocumentName", txtDocName.Text),
            new SqlParameter("@Description", txtDescription.Text),
            new SqlParameter("@FileType", Path.GetExtension(selectedFilePath)), // Uzantı
            new SqlParameter("@FileSize", new FileInfo(selectedFilePath).Length), // Boyut
            new SqlParameter("@CategoryID", Convert.ToInt32(cmbCategory.SelectedValue)),
            new SqlParameter("@DepartmentID", Convert.ToInt32(cmbDepartment.SelectedValue)),
            new SqlParameter("@UploadedByUserID", UserSession.UserId)
        };

                // 3. SqlHelper üzerinden prosedürü çağır
                int sonuc = SqlHelper.ExecuteProcedure("sp_InsertDocument", p);

                if (sonuc != 0)
                {
                    // 1. Önce kullanıcıya mesaj ver
                    MessageBox.Show("Kaydedildi", "Kaydedildi");

                    // 2. Değişiklik takibini kapat (Artık kaydedildi, uyarı vermesin)
                    hasUnsavedChanges = false;

                    // 3. BUTONLARI AYARLA
                    btnAction.Enabled = false;
                    btnDraft.Enabled = false; 
                    btnClear.Enabled = true; 

                }
            }
            catch (Exception ex)
            {
                // image_07bcae.png'deki hatayı burası yakalar
                MessageBox.Show("Kayıt hatası: " + ex.Message);
            }
        }



        // --- BUTON TIKLAMALARI ---
        private void btnAction_Click(object sender, EventArgs e)
        {

            int statusID = 1; // Varsayılan (Hata önlemek için)

            // Admin (1) eklerse -> Direkt YAYINDA (4)
            if (UserSession.UserId == UserSession.ADMIN_ID)
            {
                statusID = 4;
            }
            // Editör (2) eklerse -> ADMİN ONAYI BEKLİYOR (3)
            else if (UserSession.UserId == UserSession.EDITOR_ID)
            {
                statusID = 3;
            }
            // Üretici (3) eklerse -> EDİTÖR ONAYI BEKLİYOR (2)
            else if (UserSession.UserId == UserSession.URETICI_ID)
            {
                statusID = 2;
            }

            // SaveDocument metoduna bu statusID'yi parametre olarak gönderdiğini varsayıyorum: idye göre alma yok
            SaveDocument();
        }


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
            btnDraft.Enabled = true;
            btnClear.Enabled = false;
            hasUnsavedChanges = false;
        }

        // --- FORM KAPATMA ---
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBelgeEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasUnsavedChanges)
            {
                DialogResult dr = MessageBox.Show("Kaydetmediğiniz değişiklikler var. Çıkmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) e.Cancel = true;
            }
        }

        // --- SÜRÜKLE BIRAK (DRAG DROP) ---
        private void pnlDropZone_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                pnlDropZone.BackColor = Color.LightBlue;
            }
            else e.Effect = DragDropEffects.None;
        }

        private void pnlDropZone_DragLeave(object sender, EventArgs e)
        {
            pnlDropZone.BackColor = Color.WhiteSmoke;
        }

        private void pnlDropZone_DragDrop(object sender, DragEventArgs e)
        {
            pnlDropZone.BackColor = Color.WhiteSmoke;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0) SelectFile(files[0]);
        }

        private void pnlDropZone_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Belge Seçin";
                if (ofd.ShowDialog() == DialogResult.OK) SelectFile(ofd.FileName);
            }
        }

        private void SelectFile(string path)
        {
            selectedFilePath = path;
            lblSelectedFile.Text = Path.GetFileName(path);
            lblSelectedFile.ForeColor = Color.Blue;
            if (string.IsNullOrWhiteSpace(txtDocName.Text)) txtDocName.Text = Path.GetFileNameWithoutExtension(path);
            hasUnsavedChanges = true;
            lblSelectedFile.Visible = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void lblSelectedFile_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }

        private void btnDraft_Click(object sender, EventArgs e)
        {
            // Validasyon: Belge adı zorunludur
            if (string.IsNullOrWhiteSpace(txtDocName.Text))
            {
                MessageBox.Show("Taslak olarak kaydetmek için zorunlu alanlar girilmelidir.", "Uyarı");
                return;
            }

            try
            {
                SqlParameter[] p = {
            new SqlParameter("@DocumentName", txtDocName.Text),
            new SqlParameter("@Description", txtDescription.Text),
            new SqlParameter("@CategoryID", cmbCategory.SelectedValue ?? DBNull.Value),
            new SqlParameter("@DepartmentID", cmbDepartment.SelectedValue ?? DBNull.Value),
            new SqlParameter("@UploadedByUserID", UserSession.UserId)
        };

                // Prosedürü çağır
                int result = SqlHelper.ExecuteProcedure("sp_SaveAsDraft", p);

                if (result != 0)
                {
                    MessageBox.Show("Kaydedildi.", "Kaydedildi");
                    hasUnsavedChanges = false;

                    // 3. BUTON DURUMLARINI AYARLA
                    btnAction.Enabled = false; // Normal kaydet pasif
                    btnDraft.Enabled = false;  // Taslak kaydet pasif (tekrar basılmasın)
                    btnClear.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Taslak olarak kaydetmek için zorunlu alanlar girilmelidir.", "Uyarı");
            }
        }


    }
}
