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
            // Arama kutusundaki metni al, boþluklarý temizle
            string arananKelime = txtSearch.Text.Trim().Replace("'", "''");

            if (string.IsNullOrEmpty(arananKelime))
            {
                // Kutu boþsa filtreyi kaldýr
                binder.RemoveFilter();
            }
            else
            {
                // BURAYA DÝKKAT: Veritabanýndaki sütun adýnýz 'DosyaAdi' deðilse deðiþtirin!
                binder.Filter = string.Format("DosyaAdi LIKE '%{0}%'", arananKelime);
                if (binder.Count == 0)
                {
                    MessageBox.Show("Aradýðýnýz kriterlere uygun belge bulunamadý.", "Sonuç Yok", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ýsterseniz filtreyi geri kaldýrabilirsiniz ki tablo boþ kalmasýn:
                    // binder.RemoveFilter(); 
                    // txtSearch.SelectAll(); // Kullanýcý kolayca yeni kelime yazabilsin diye metni seç
                }
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
                // Tarihleri sýfýrla (son 30 gün)
                dtpStartDate.Value = DateTime.Now.AddDays(-30);
                dtpEndDate.Value = DateTime.Now;

                // Departman ve kategori seçimlerini sýfýrla
                if (cmbDepartment.Items.Count > 0)
                    cmbDepartment.SelectedIndex = 0;

                if (cmbCategory != null && cmbCategory.Items.Count > 0)
                    cmbCategory.SelectedIndex = 0;

                // Tüm verileri getir (son 30 gün)
                DateTime startDate = DateTime.Now.AddDays(-30);
                DateTime endDate = DateTime.Now;

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@StartDate", startDate.ToString("yyyy-MM-dd")),
                    new SqlParameter("@EndDate", endDate.ToString("yyyy-MM-dd")),
                    new SqlParameter("@DepartmentName", DBNull.Value)
                };

                DataTable dt = SqlHelper.GetDataByProcedure("storedprocedure_FilterDocuments", parameters);

                if (dgvDocuments != null)
                {
                    dgvDocuments.DataSource = dt;
                    lblRecordCount.Text = $"Toplam {dt.Rows.Count} belge bulundu";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Temizleme sýrasýnda hata: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Baþlangýçta tüm belgeleri yükle (sadece son 30 gün)
                DateTime startDate = DateTime.Now.AddDays(-30);
                DateTime endDate = DateTime.Now;

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@StartDate", startDate.ToString("yyyy-MM-dd")),
                    new SqlParameter("@EndDate", endDate.ToString("yyyy-MM-dd")),
                    new SqlParameter("@DepartmentName", DBNull.Value)
                };

                DataTable dt = SqlHelper.GetDataByProcedure("storedprocedure_FilterDocuments", parameters);

                // DataGridView'e baðla
                if (dgvDocuments != null)
                {
                    dgvDocuments.DataSource = dt;
                    ConfigureDataGridView();
                }

                // Tarih kontrollerini ayarla
                if (dtpStartDate != null && dtpEndDate != null)
                {
                    dtpStartDate.Value = startDate;
                    dtpEndDate.Value = endDate;
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
                        dgvDocuments.Columns["DocumentID"].HeaderText = "Belge ID";

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
                    dgvDocuments.DataSource = dt;

                    // Filtrelenen kayýt sayýsýný göster
                    int rowCount = dt.Rows.Count;
                    lblRecordCount.Text = $"Toplam {rowCount} belge bulundu";

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
                int selectedCount = dgvDocuments.SelectedRows.Count;
                lblSelectionCount.Text = $"{selectedCount} belge seçildi";
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

        // --- 3. ADIM: SENÝN YAZDIÐIN KOD (Buraya Entegre Edildi) ---
        //private void ApplyRolePermissions(UserRole role)
        //{
        //    // Önce hepsini varsayýlan hale getir (veya gizle/kapat)
        //    btnDocumentAdd.Enabled = true;
        //    btnMyDocuments.Enabled = true;
        //    btnPendingApproval.Enabled = true;
        //    btnTrash.Enabled = true;
        //    btnHelp.Enabled = true;

        //    // Butonlarýn görünürlüðünü de açalým (önceden gizlendiyse geri gelsin)
        //    btnDocumentAdd.Visible = true;
        //    btnMyDocuments.Visible = true;
        //    btnPendingApproval.Visible = true;
        //    btnTrash.Visible = true;
        //    btnHelp.Visible = true;

        //    // Renkleri sýfýrla
        //    ResetButtonStyles();

        //    // Filtreleme kontrollerini de role göre ayarla
        //    ApplyRoleToFilterControls(role);

        //    switch (role)
        //    {
        //        case UserRole.Admin:
        //        case UserRole.Editor:
        //            // Admin ve Editörde hepsi aktif
        //            break;

        //        case UserRole.Employee:
        //            // Çalýþan: Onay Bekleyenler pasif/gizli
        //            btnPendingApproval.Enabled = false;
        //            break;

        //        case UserRole.Reader:
        //            // Okuyucu: Sadece Yardým aktif
        //            btnDocumentAdd.Enabled = false;
        //            btnMyDocuments.Enabled = false;
        //            btnPendingApproval.Enabled = false;
        //            btnTrash.Enabled = false;

        //            // Yardým açýk kalýr:
        //            btnHelp.Enabled = true;
        //            break;
        //    }
        //}


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

        private void lblSelectionCount_Click(object sender, EventArgs e)
        {

        }
    }
}