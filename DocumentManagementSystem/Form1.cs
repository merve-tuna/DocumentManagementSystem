using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentManagementSystem
{
    // 1. ADIM: Rolleri tanýmladýðýmýz Enum yapýsý (Class'ýn dýþýna ekledik)
    public enum UserRole
    {
        Admin,
        Editor,
        Employee, // Çalýþan
        Reader    // Okuyucu
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupUI(); // Olaylar ve Veriler
        }

 

        private void SetupUI()
        {
            // Kullanýcý Rolleri (Ekranda Görünen Ýsimler)
            cmbUserRole.Items.Clear();
            cmbUserRole.Items.Add("Admin");
            cmbUserRole.Items.Add("Editör");
            cmbUserRole.Items.Add("Çalýþan");
            cmbUserRole.Items.Add("Okuyucu");
            cmbUserRole.SelectedIndex = 0; // Varsayýlan Admin

            // Filtre Verileri
            cmbDepartment.Items.Add("Ýnsan Kaynaklarý");
            cmbDepartment.Items.Add("Bilgi Ýþlem");

            cmbCategory.Items.Add("Fatura");
            cmbCategory.Items.Add("Rapor");

            // Olaylar
            cmbUserRole.SelectedIndexChanged += CmbUserRole_SelectedIndexChanged;

            // Sayfa Geçiþ Butonlarý
            btnDocumentAdd.Click += (s, e) => OpenPage("BelgeEkle");
            btnMyDocuments.Click += (s, e) => OpenPage("Belgelerim");
            btnPendingApproval.Click += (s, e) => OpenPage("OnayBekleyenler");
            btnTrash.Click += (s, e) => OpenPage("CopKutusu");
            btnHelp.Click += (s, e) => OpenPage("Yardim");
        }

        // --- 2. ADIM: ComboBox Seçimini Enum'a Çevirme ---
        private void CmbUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ComboBox boþsa iþlem yapma
            if (cmbUserRole.SelectedItem == null) return;

            string selectedText = cmbUserRole.SelectedItem.ToString();
            UserRole role;

            // Türkçe Seçimi -> Kodun anlayacaðý Role Çeviriyoruz
            switch (selectedText)
            {
                case "Admin": role = UserRole.Admin; break;
                case "Editör": role = UserRole.Editor; break;
                case "Çalýþan": role = UserRole.Employee; break;
                case "Okuyucu": role = UserRole.Reader; break;
                default: role = UserRole.Reader; break;
            }

            // Senin yazdýðýn fonksiyonu çaðýrýyoruz
            ApplyRolePermissions(role);
        }

        // --- 3. ADIM: SENÝN YAZDIÐIN KOD (Buraya Entegre Edildi) ---
        private void ApplyRolePermissions(UserRole role)
        {
            // Önce hepsini varsayýlan hale getir (veya gizle/kapat)
            btnDocumentAdd.Enabled = true;
            btnMyDocuments.Enabled = true;
            btnPendingApproval.Enabled = true;
            btnTrash.Enabled = true;
            btnHelp.Enabled = true;

            // Butonlarýn görünürlüðünü de açalým (önceden gizlendiyse geri gelsin)
            btnDocumentAdd.Visible = true;
            btnMyDocuments.Visible = true;
            btnPendingApproval.Visible = true;
            btnTrash.Visible = true;
            btnHelp.Visible = true;

            // Renkleri sýfýrla
            ResetButtonStyles();

            switch (role)
            {
                case UserRole.Admin:
                case UserRole.Editor:
                    // Admin ve Editörde hepsi aktif
                    break;

                case UserRole.Employee:
                    // Çalýþan: Onay Bekleyenler pasif/gizli
                    // Senin isteðin üzerine Enabled kapatýyoruz:
                    btnPendingApproval.Enabled = false;

                    // Görsel olarak da kullanýcý anlasýn diye rengini gri yapabiliriz
                    btnPendingApproval.BackColor = Color.LightGray;
                    break;

                case UserRole.Reader:
                    // Okuyucu: Sadece Yardým aktif
                    btnDocumentAdd.Enabled = false;
                    btnMyDocuments.Enabled = false;
                    btnPendingApproval.Enabled = false;
                    btnTrash.Enabled = false;

                    // Yardým açýk kalýr:
                    btnHelp.Enabled = true;
                    break;
            }
        }

        // Görseli düzeltmek için yardýmcý metod
        private void ResetButtonStyles()
        {
            // Tüm butonlarýn rengini varsayýlan yap (Örn: Beyaz veya Control rengi)
            Color defaultColor = Color.White; 

            btnDocumentAdd.BackColor = defaultColor;
            btnMyDocuments.BackColor = defaultColor;
            btnPendingApproval.BackColor = defaultColor;
            btnTrash.BackColor = defaultColor;
            btnHelp.BackColor = defaultColor;
        }

        private void OpenPage(string pageName)
        {
            Form pageToOpen = null;

            switch (pageName)
            {
                case "BelgeEkle": pageToOpen = new FrmBelgeEkle(); break;
                case "Belgelerim": pageToOpen = new FrmBelgelerim(); break;
                case "OnayBekleyenler": pageToOpen = new FrmOnayBekleyenler(); break;
                case "CopKutusu": pageToOpen = new FrmCopKutusu(); break;
                case "Yardim": pageToOpen = new FrmYardim(); break;
            }

            if (pageToOpen != null)
            {
                // 1. HAFIZA DEÐÝÞKENLERÝ TANIMLIYORUZ
                // Ana formdan çocuk forma geçerken mevcut durumu aktar
                pageToOpen.StartPosition = FormStartPosition.Manual;
                pageToOpen.WindowState = this.WindowState;

                if (this.WindowState == FormWindowState.Normal)
                {
                    pageToOpen.Location = this.Location;
                    pageToOpen.Size = this.Size;
                }

                // Geri dönerken kullanacaðýmýz geçici deðiþkenler (Varsayýlan olarak mevcut hali alalým)
                FormWindowState finalState = this.WindowState;
                Point finalLocation = this.Location;
                Size finalSize = this.Size;

                // 2. KAPANIRKEN SON DURUMU HAFIZAYA AL
                pageToOpen.FormClosing += (sender, e) =>
                {
                    Form f = (Form)sender;
                    finalState = f.WindowState;

                    // Eðer pencere normal moddaysa (tam ekran deðilse) boyutlarý kaydet
                    if (f.WindowState == FormWindowState.Normal)
                    {
                        finalLocation = f.Location;
                        finalSize = f.Size;
                    }
                    else
                    {
                        // Eðer tam ekransa, eski konumunu (RestoreBounds) hatýrlamak iyi olur
                        finalLocation = f.RestoreBounds.Location;
                        finalSize = f.RestoreBounds.Size;
                    }
                };

                // 3. GÝZLE VE AÇ
                this.Hide();
                pageToOpen.ShowDialog();

                // 4. KRÝTÝK NOKTA: ÖNCE GÖSTER, SONRA BOYUTLA
                // Formu önce görünür yapýyoruz ki Windows deðiþiklikleri kabul etsin.
                this.Show();

                // Þimdi hafýzadaki deðerleri uyguluyoruz
                this.WindowState = finalState;

                // Eðer normal moda döndüysek boyut ve konumu ayarla
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = finalLocation;
                    this.Size = finalSize;
                }
            }
        }

        private void LoadFilterData()
        {
            try
            {
                // Departmanlarý Çek
                string deptQuery = "SELECT DepartmentID, DepartmentName FROM Departments WHERE IsActive = 1";
                DataTable dtDept = SqlHelper.GetData(deptQuery);

                cmbDepartment.DataSource = dtDept;
                cmbDepartment.DisplayMember = "DepartmentName";
                cmbDepartment.ValueMember = "DepartmentID";
                cmbDepartment.SelectedIndex = -1;

                // Kategorileri Çek
                string catQuery = "SELECT CategoryID, CategoryName FROM Categories WHERE IsActive = 1";
                DataTable dtCat = SqlHelper.GetData(catQuery);

                cmbCategory.DataSource = dtCat;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryID";
                cmbCategory.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtre verileri yüklenirken hata: " + ex.Message);
            }
        }

        private void LoadDocuments()
        {
            try
            {
                // Senin veritabaný yapýna uygun sorgu
                string query = @"
            SELECT 
                d.DocumentID,
                d.DocumentName AS [Belge Adý],
                dep.DepartmentName AS [Departman],
                cat.CategoryName AS [Kategori],
                u.UserName AS [Yükleyen],
                d.UploadDate AS [Tarih],
                d.CurrentVersion AS [Versiyon],
                s.StatusName AS [Durum]
            FROM Documents d
            INNER JOIN Departments dep ON d.DepartmentID = dep.DepartmentID
            INNER JOIN Categories cat ON d.CategoryID = cat.CategoryID
            INNER JOIN Users u ON d.UploadedByUserID = u.UserID
            INNER JOIN DocumentStatus s ON d.StatusID = s.StatusID
            WHERE d.IsDeleted = 0";

                DataTable dt = SqlHelper.GetData(query);
                dgvDocuments.DataSource = dt;

                // ID sütununu gizle
                if (dgvDocuments.Columns["DocumentID"] != null)
                {
                    dgvDocuments.Columns["DocumentID"].Visible = false;
                }

                // Tarih formatý
                if (dgvDocuments.Columns["Tarih"] != null)
                {
                    dgvDocuments.Columns["Tarih"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Belgeler yüklenirken hata: " + ex.Message);
            }
        }
    }
}