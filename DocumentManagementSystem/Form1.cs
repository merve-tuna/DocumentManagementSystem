using System;
using System.Data;
using System.Data.SqlClient;
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
        BindingSource binder = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            SetupUI(); // Olaylar ve Veriler
            LoadDepartmentsForFilter();
            LoadCategoriesForFilter();
            LoadInitialData();
            RefreshDocumentList();
            UpdateLabels();
            ConfigureDataGridView();

        }

        private void UpdateLabels()
        {
            // 1. Kayýt Sayýsý (Toplam Listelenen)
            if (binder != null)
            {
                lblRecordCount.Text = $"Toplam {binder.Count} belge listelendi";
            }

        }

        private void RefreshDocumentList()
        {
            try
            {
                DataTable dt = SqlHelper.GetDataByProcedure("storedprocedure_FilterDocuments");

                if (dt != null)
                {
                    binder.DataSource = dt;
                    dgvDocuments.DataSource = binder;

                    // ANA SAYFA ÝÇÝN AYAR METODUNU ÇAÐIR:
                    ConfigureDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void SetupUI()
        {
            cmbUserRole.Items.Clear();
            cmbUserRole.Items.Add("Admin");   // ID: 1
            cmbUserRole.Items.Add("Editör");  // ID: 2
            cmbUserRole.Items.Add("Üretici"); // ID: 3 (Eski hali: Çalýþan)
            cmbUserRole.Items.Add("Okuyucu"); // ID: 4
            cmbUserRole.SelectedIndex = 0;

            cmbUserRole.SelectedIndexChanged += CmbUserRole_SelectedIndexChanged;

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
            if (cmbUserRole.SelectedItem == null) return;

            string selectedText = cmbUserRole.SelectedItem.ToString();
            UserRole role;

            // 1. ADIM: UserSession Bilgilerini Güncelle (Dinamik ID'ler)
            switch (selectedText)
            {
                case "Admin":
                    role = UserRole.Admin;
                    UserSession.UserId = 1;
                    break;
                case "Editör":
                    role = UserRole.Editor;
                    UserSession.UserId = 2;
                    break;
                case "Üretici":
                    role = UserRole.Employee;
                    UserSession.UserId = 3;
                    break;
                case "Okuyucu":
                    role = UserRole.Reader;
                    UserSession.UserId = 4;
                    break;
                default:
                    role = UserRole.Reader;
                    UserSession.UserId = 4;
                    break;
            }

            UserSession.RoleName = selectedText;

            // 2. ADIM: Yetkileri ve Görsel Kýsýtlamalarý Uygula
            ApplyRolePermissions(role);
            ApplyRoleToFilterControls(role); // Filtreleri kilitlemek/açmak için
            ApplyRoleToDataGridView(role);   // Grid'i kilitlemek/açmak için
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

            this.Text = $"DMS - Aktif Kullanýcý ID: {UserSession.UserId} ({UserSession.RoleName})";
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
                //this.WindowState = finalState;

                // Eðer normal moda döndüysek boyut ve konumu ayarla
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = finalLocation;
                    this.Size = finalSize;
                }
                // Sayfa kapandýktan sonra verileri yenile
                LoadInitialData();
            }
        }

        private void lblUserIcon_Click(object sender, EventArgs e)
        {

        }

        // 1. Önce arama iþlemini yapan ORTAK bir metot yazalým
        private void DoSearch()
        {
            string arananKelime = txtSearch.Text.Trim().Replace("'", "''");

            if (string.IsNullOrEmpty(arananKelime))
            {
                binder.RemoveFilter();
            }
            else
            {

                try
                {
                    binder.Filter = string.Format("DocumentName LIKE '%{0}%'", arananKelime);
                }
                catch (Exception ex)
                {
                    // Eðer hala hata alýyorsan sütun adý yanlýþtýr.
                    MessageBox.Show("Filtreleme hatasý. Sütun adýný kontrol et: " + ex.Message);
                }

                if (binder.Count == 0)
                {
                    MessageBox.Show("Aradýðýnýz kriterlere uygun belge bulunamadý.", "Sonuç Yok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Ýstersen burada aramayý temizleme kodunu aktif edebilirsin
                }
                UpdateLabels();
            }
        }

        // 2. "Ara" Butonuna týklandýðýnda çalýþacak kod
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        // 3. TextBox üzerindeyken tuþa basýldýðýnda çalýþacak kod
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // Basýlan tuþ "Enter" ise
            if (e.KeyCode == Keys.Enter)
            {
                DoSearch();

                // "Dýt" sesini engellemek ve iþlemi tamamlandý saymak için:
                e.SuppressKeyPress = true;
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. ADIM: Arama Kutusunu ve Bellek Filtresini Temizle
                txtSearch.Text = "";       // Görsel olarak kutuyu boþalt
                binder.RemoveFilter();     // Arka plandaki "LIKE" filtresini kaldýr

                // 2. ADIM: Görsel Seçimleri Sýfýrla
                if (cmbDepartment.Items.Count > 0) cmbDepartment.SelectedIndex = 0; // "Tümü" yap
                if (cmbCategory != null && cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;

                // Tarihleri görsel olarak mantýklý bir aralýða çek (Örn: Bu yýlýn baþý veya bugün)
                // Not: Bu sadece görseldir, sorguyu etkilememesi için aþaðýda NULL göndereceðiz.
                dtpStartDate.Value = DateTime.Now.AddYears(-1);
                dtpEndDate.Value = DateTime.Now;

                // 3. ADIM: SQL'den "TÜM" Verileri Çek (Sýnýrlama Olmadan)
                // SP'de NULL gönderdiðimizde "OR @Param IS NULL" çalýþtýðý için tüm veriler gelir.
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@StartDate", DBNull.Value),      // Tarih sýnýrý yok
            new SqlParameter("@EndDate", DBNull.Value),        // Bitiþ sýnýrý yok
            new SqlParameter("@DepartmentName", DBNull.Value)  // Departman sýnýrý yok
                };

                DataTable dt = SqlHelper.GetDataByProcedure("storedprocedure_FilterDocuments", parameters);

                // 4. ADIM: Veriyi Binder ile Baðla (Kritik Nokta)
                if (dgvDocuments != null)
                {
                    binder.DataSource = dt;           // Veriyi önce binder'a yükle
                    dgvDocuments.DataSource = binder; // Sonra grid'e binder'ý ver

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Temizleme iþlemi sýrasýnda hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void LoadDepartmentsForFilter()
        {
            try
            {
                cmbDepartment.Items.Clear();
                cmbDepartment.Items.Add("Tümü"); // Tüm departmanlarý göster

                // Stored Procedure ile departmanlarý çek (FrmBelgeEkle'deki gibi)
                DataTable dt = SqlHelper.GetDataByProcedure("sp_GetDepartments");

                foreach (DataRow row in dt.Rows)
                {
                    cmbDepartment.Items.Add(row["DepartmentName"].ToString());
                }

                if (cmbDepartment.Items.Count > 0)
                    cmbDepartment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Departmanlar yüklenirken hata: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadCategoriesForFilter()
        {
            try
            {
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("Tümü"); // Tüm kategorileri göster

                // Stored Procedure ile kategorileri çek
                DataTable dt = SqlHelper.GetDataByProcedure("sp_GetCategories");

                foreach (DataRow row in dt.Rows)
                {
                    cmbCategory.Items.Add(row["CategoryName"].ToString());
                }

                if (cmbCategory.Items.Count > 0)
                    cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kategoriler yüklenirken hata: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInitialData()
        {
            try
            {

                // 1. Önceki filtre kalýntýlarýný temizle
                if (binder != null) binder.RemoveFilter();

                // 2. Arama kutusunu görsel olarak temizle (Ýsteðe baðlý)
                if (txtSearch != null) txtSearch.Text = "";


                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@StartDate", DBNull.Value),
                    new SqlParameter("@EndDate", DBNull.Value),
                    new SqlParameter("@DepartmentName", DBNull.Value)
                };

                DataTable dt = SqlHelper.GetDataByProcedure("storedprocedure_FilterDocuments", parameters);

                //MessageBox.Show($"LoadInitialData Çalýþtý!\nGelen Satýr Sayýsý: {dt.Rows.Count}");

                // DataGridView'e baðla
                if (dgvDocuments != null)
                {
                    binder.DataSource = dt;       // 1. Veriyi binder'a yükle
                    dgvDocuments.DataSource = binder; // 2. Grid'i binder'a baðla
                    ConfigureDataGridView();
                    UpdateLabels();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken hata: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ConfigureDataGridView()
        {
            if (dgvDocuments != null && dgvDocuments.Columns.Count > 0)
            {
                try
                {
                    // Kolon baþlýklarýný Türkçeleþtir
                    if (dgvDocuments.Columns.Contains("DocumentID"))
                        //dgvDocuments.Columns["DocumentID"].HeaderText = "Belge ID
                        dgvDocuments.Columns["DocumentID"].Visible = false;

                    if (dgvDocuments.Columns.Contains("DocumentName"))
                        dgvDocuments.Columns["DocumentName"].HeaderText = "Belge Adý";


                    if (dgvDocuments.Columns.Contains("DepartmentName"))
                        dgvDocuments.Columns["DepartmentName"].HeaderText = "Departman";


                    if (dgvDocuments.Columns.Contains("CategoryName"))
                        dgvDocuments.Columns["CategoryName"].HeaderText = "Kategori";


                    if (dgvDocuments.Columns.Contains("StatusName"))
                        dgvDocuments.Columns["StatusName"].HeaderText = "Durum";


                    if (dgvDocuments.Columns.Contains("Description"))
                        dgvDocuments.Columns["Description"].HeaderText = "Açýklama";
                        dgvDocuments.Columns["Description"].Visible = false;


                    if (dgvDocuments.Columns.Contains("FileType"))
                        dgvDocuments.Columns["FileType"].HeaderText = "Dosya Türü";


                    if (dgvDocuments.Columns.Contains("FileSize"))
                        dgvDocuments.Columns["FileSize"].HeaderText = "Dosya Boyutu";


                    if (dgvDocuments.Columns.Contains("CurrentVersion"))
                        dgvDocuments.Columns["CurrentVersion"].HeaderText = "Versiyon";


                    if (dgvDocuments.Columns.Contains("UploadDate"))
                    {
                        dgvDocuments.Columns["UploadDate"].HeaderText = "Yükleme Tarihi";
                        dgvDocuments.Columns["UploadDate"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                    }

                    if (dgvDocuments.Columns.Contains("UploadedBy"))
                        dgvDocuments.Columns["UploadedBy"].HeaderText = "Yükleyen";

                    // Kolon geniþliklerini ayarla
                    dgvDocuments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    // Açýklama kolonunu daha geniþ yap
                    if (dgvDocuments.Columns.Contains("Description"))
                        dgvDocuments.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"DataGridView ayarlanýrken hata: {ex.Message}",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            FilterData();
        }

        private void FilterData()
        {
            try
            {
                string startDate = dtpStartDate.Value.ToString("yyyy-MM-dd");
                string endDate = dtpEndDate.Value.ToString("yyyy-MM-dd");
                string department = cmbDepartment.SelectedItem?.ToString();

                // "Tümü" seçildiyse NULL gönder
                object departmentParam = (department == "Tümü" || string.IsNullOrEmpty(department)) ?
                    DBNull.Value : (object)department;

                // Filtreleme için stored procedure (SÝZÝN storedprocedure_FilterDocuments)
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@StartDate", startDate),
                    new SqlParameter("@EndDate", endDate),
                    new SqlParameter("@DepartmentName", departmentParam)
                };

                // DataGridView'i doldur
                DataTable dt = SqlHelper.GetDataByProcedure("storedprocedure_FilterDocuments", parameters);

                if (dgvDocuments != null)
                {
                    binder.DataSource = dt;       // 1. Veriyi binder'a yükle
                    dgvDocuments.DataSource = binder; // 2. Grid'i binder'a baðla

                    // Filtrelenen kayýt sayýsýný göster
                    int rowCount = dt.Rows.Count;
                    lblRecordCount.Text = $"Toplam {rowCount} belge bulundu";
                    lblRecordCount.Visible = true;

                    if (rowCount == 0)
                    {
                        MessageBox.Show("Belirtilen kriterlere uygun belge bulunamadý.",
                            "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filtreleme sýrasýnda hata: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void DgvDocuments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDocuments.Rows.Count > 0)
            {
                try
                {
                    // Çift týklanan belgenin detaylarýný göster
                    DataGridViewRow row = dgvDocuments.Rows[e.RowIndex];
                    string documentName = row.Cells["DocumentName"].Value?.ToString();
                    string department = row.Cells["DepartmentName"].Value?.ToString();

                    if (!string.IsNullOrEmpty(documentName))
                    {
                        MessageBox.Show($"Belge: {documentName}\nDepartman: {department}",
                            "Belge Detayý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Detay gösterilirken hata: {ex.Message}",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }



        private void DgvDocuments_SelectionChanged(object sender, EventArgs e)
        {
            // Seçili satýr sayýsýný göster
            if (dgvDocuments != null)
            {
                //int selectedCount = dgvDocuments.SelectedRows.Count;

                UpdateLabels();
            }
        }

        private void ApplyRoleToDataGridView(UserRole role)
        {
            if (dgvDocuments == null) return;

            try
            {
                // Rol bazlý DataGridView ayarlarý
                switch (role)
                {
                    case UserRole.Admin:
                    case UserRole.Editor:
                        // Admin ve Editör: Tüm iþlemler aktif
                        dgvDocuments.ReadOnly = false;
                        dgvDocuments.AllowUserToAddRows = false;
                        dgvDocuments.AllowUserToDeleteRows = false;
                        break;

                    case UserRole.Employee:
                        // Çalýþan: Sadece görüntüleme
                        dgvDocuments.ReadOnly = true;
                        dgvDocuments.AllowUserToAddRows = false;
                        dgvDocuments.AllowUserToDeleteRows = false;
                        break;

                    case UserRole.Reader:
                        // Okuyucu: Sadece görüntüleme
                        dgvDocuments.ReadOnly = true;
                        dgvDocuments.AllowUserToAddRows = false;
                        dgvDocuments.AllowUserToDeleteRows = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rol ayarlanýrken hata: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void ApplyRoleToFilterControls(UserRole role)
        {
            // Rol bazlý filtreleme kontrol ayarlarý
            bool canFilter = true;

            switch (role)
            {
                case UserRole.Reader:
                    canFilter = false;
                    break;
                default:
                    canFilter = true;
                    break;
            }

            // Filtreleme kontrollerini ayarla
            if (dtpStartDate != null) dtpStartDate.Enabled = canFilter;
            if (dtpEndDate != null) dtpEndDate.Enabled = canFilter;
            if (cmbDepartment != null) cmbDepartment.Enabled = canFilter;
            if (cmbCategory != null) cmbCategory.Enabled = canFilter;
            if (btnFilter != null) btnFilter.Enabled = canFilter;
            if (btnClear != null) btnClear.Enabled = canFilter;
        }



        private void lblRecordCount_Click(object sender, EventArgs e)
        {

        }
    }
}