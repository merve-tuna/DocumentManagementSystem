using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace DocumentManagementSystem
{
    public enum UserRole
    {
        Admin,
        Editor,
        Employee, 
        Reader    
    }

    public partial class Form1 : Form
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DocumentManagementSystem;Integrated Security=True;TrustServerCertificate=True";


        BindingSource binder = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            SetupUI();
            LoadDepartmentsForFilter();
            LoadCategoriesForFilter();
            LoadInitialData();
            RefreshDocumentList();
            UpdateLabels();
            ConfigureDataGridView();

        }

        private void UpdateLabels()
        {
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
            cmbUserRole.Items.Add("Üretici"); // ID: 3 
            cmbUserRole.Items.Add("Okuyucu"); // ID: 4
            cmbUserRole.SelectedIndex = 0;

            cmbUserRole.SelectedIndexChanged += CmbUserRole_SelectedIndexChanged;

            // Olaylar
            cmbUserRole.SelectedIndexChanged += CmbUserRole_SelectedIndexChanged;

            // Sayfa Geçiş Butonları
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

            ApplyRolePermissions(role);
            ApplyRoleToDataGridView(role);
        }

        private void ApplyRolePermissions(UserRole role)
        {

            btnDocumentAdd.Enabled = true;
            btnMyDocuments.Enabled = true;
            btnPendingApproval.Enabled = true;
            btnTrash.Enabled = true;
            btnHelp.Enabled = true;

            btnDocumentAdd.Visible = true;
            btnMyDocuments.Visible = true;
            btnPendingApproval.Visible = true;
            btnTrash.Visible = true;
            btnHelp.Visible = true;


            ResetButtonStyles();

            switch (role)
            {
                case UserRole.Admin:
                case UserRole.Editor:
                    break;

                case UserRole.Employee:
                    btnPendingApproval.Enabled = false;

                    break;

                case UserRole.Reader:
                    btnDocumentAdd.Enabled = false;
                    btnMyDocuments.Enabled = false;
                    btnPendingApproval.Enabled = false;
                    btnTrash.Enabled = false;
                    btnHelp.Enabled = true;
                    break;
            }

            this.Text = $"DMS - Aktif Kullanıcı ID: {UserSession.UserId} ({UserSession.RoleName})";
        }

        // Görseli düzeltmek için yardýmcý metod
        private void ResetButtonStyles()
        {
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

                this.Hide();
                pageToOpen.ShowDialog();

                this.Show();

                if (this.WindowState == FormWindowState.Normal)
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = finalLocation;
                    this.Size = finalSize;
                }
                LoadInitialData();
            }
        }

        private void lblUserIcon_Click(object sender, EventArgs e)
        {

        }

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
                    MessageBox.Show("Filtreleme hatasý. Sütun adýný kontrol et: " + ex.Message);
                }

                if (binder.Count == 0)
                {
                    MessageBox.Show("Aradýðýnýz kriterlere uygun belge bulunamadý.", "Sonuç Yok", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                if (dgvDocuments != null)
                {
                    binder.DataSource = dt;           
                    dgvDocuments.DataSource = binder; 

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

                if (binder != null) binder.RemoveFilter();

                if (txtSearch != null) txtSearch.Text = "";

                

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@StartDate", DBNull.Value),
                    new SqlParameter("@EndDate", DBNull.Value),
                    new SqlParameter("@DepartmentName", DBNull.Value)
                };

                DataTable dt = SqlHelper.GetDataByProcedure("storedprocedure_FilterDocuments", parameters);


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
                    

                    // ConfigureDataGridView metodunun içine ekle:
                    if (dgvDocuments.Columns.Contains("UploadedByUserID"))
                        dgvDocuments.Columns["UploadedByUserID"].Visible = false; // Kullanıcı görmesin ama biz kodda kullanalım

                    if (dgvDocuments.Columns.Contains("DocumentID"))
                        //dgvDocuments.Columns["DocumentID"].HeaderText = "Belge ID
                        dgvDocuments.Columns["DocumentID"].Visible = false;

                    if (dgvDocuments.Columns.Contains("DocumentName"))
                        dgvDocuments.Columns["DocumentName"].HeaderText = "Belge Adı";


                    if (dgvDocuments.Columns.Contains("DepartmentName"))
                        dgvDocuments.Columns["DepartmentName"].HeaderText = "Departman";


                    if (dgvDocuments.Columns.Contains("CategoryName"))
                        dgvDocuments.Columns["CategoryName"].HeaderText = "Kategori";


                    if (dgvDocuments.Columns.Contains("StatusName"))
                        dgvDocuments.Columns["StatusName"].Visible = false;


                    if (dgvDocuments.Columns.Contains("Description"))
                        dgvDocuments.Columns["Description"].HeaderText = "Açıklama";
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
                    dgvDocuments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"DataGridView ayarlanırken hata: {ex.Message}",
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
                        MessageBox.Show("Belirtilen kriterlere uygun belge bulunamadı.",
                            "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filtreleme sırasında hata: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void ApplyRoleToDataGridView(UserRole role)
        {
            if (dgvDocuments == null) return;

            try
            {
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

        private void lblRecordCount_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void dgvDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0 && dgvDocuments.Columns[e.ColumnIndex].Name == "btnDownload")
            {
                // Satırdaki diğer verileri al (Örn: Dosya Adı, ID vb.)
                // "DosyaAdi" yazan yere senin gridindeki dosya ismini tutan kolonun adını yaz.
                string varsayilanDosyaAdi = dgvDocuments.Rows[e.RowIndex].Cells["DocumentName"].Value.ToString();

                // Eğer veritabanından ID ile çekeceksen ID'yi al:
                // int belgeId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);

                // 2. KAYDETME DİYALOĞU AÇMA (Hedef klasör seçimi)
                using (SaveFileDialog sfd = new SaveFileDialog())
                {

                    sfd.FileName = varsayilanDosyaAdi; // Kullanıcıya varsayılan bir ad öner
                    sfd.Filter = "PDF Dosyaları (*.pdf)|*.pdf"; // Filtre ekleyebilirsin
                    sfd.Title = "Belgeyi Nereye Kaydetmek İstersiniz?";

                    // Kullanıcı "Kaydet"e bastıysa işlem başlar
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            string hedefYol = sfd.FileName;

                            // --- 3. DOSYAYI OLUŞTURMA/İNDİRME ---


                            // Burada örnek olarak boş bir dosya oluşturuyorum (Test için):
                            File.WriteAllBytes(hedefYol, new byte[0]);

                            // 4. UYARI MESAJI
                            MessageBox.Show("İndirildi: \n" + hedefYol, "İndirildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Dosya indirilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            // 1. Tıklanan yer geçerli bir buton mu?
            if (e.RowIndex >= 0 && dgvDocuments.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                // 1. Okuyucu Kontrolü (ID: 4) - Hiçbir şeye basamaz.
                if (UserSession.UserId == UserSession.OKUYUCU_ID) // ID 4
                {
                    MessageBox.Show("Silme yetkiniz bulunmamaktadır.", "Yetkisiz İşlem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Gerekli Verileri Alalım
                int docId = Convert.ToInt32(dgvDocuments.Rows[e.RowIndex].Cells["DocumentID"].Value);
                string dosyaAdi = dgvDocuments.Rows[e.RowIndex].Cells["DocumentName"].Value.ToString();

                // BURASI ÇOK ÖNEMLİ: Belgeyi kim yükledi? (Bu kolonun Grid'de olduğundan emin ol)
                // Eğer veritabanından 'UploadedBy' olarak ID geliyorsa onu kullan.
                // Örnek olarak kolon adının 'UploadedByUserID' olduğunu varsayıyorum.
                int uploaderId = 0;
                if (dgvDocuments.Columns.Contains("UploadedByUserID") && dgvDocuments.Rows[e.RowIndex].Cells["UploadedByUserID"].Value != DBNull.Value)
                {
                    uploaderId = Convert.ToInt32(dgvDocuments.Rows[e.RowIndex].Cells["UploadedByUserID"].Value);
                }
                else
                {
                    // Eğer bu bilgi yoksa güvenlik gereği işleme devam etme veya hata ver
                    MessageBox.Show("Belge sahibi bilgisi alınamadı. Lütfen sistem yöneticisine başvurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int currentUserId = UserSession.UserId;
                bool yetkiVar = false;

                // 3. YETKİ KONTROLÜ (SENİN KURALLARIN)

                // KURAL: ID 1 (Admin) tüm belgeleri silebilir
                if (currentUserId == UserSession.ADMIN_ID)
                {
                    yetkiVar = true;
                }
                // KURAL: ID 2 (Editör), kendisinin (2) ve Üreticinin (3) yüklediklerini silebilir
                else if (currentUserId == UserSession.EDITOR_ID)
                {
                    // Yükleyen kişi Editör(2) veya Üretici(3) ise sil
                    if (uploaderId == UserSession.EDITOR_ID || uploaderId == UserSession.URETICI_ID)
                    {
                        yetkiVar = true;
                    }
                }
                // KURAL: ID 3 (Üretici) sadece kendi yüklediği (3) belgeleri silebilir
                else if (currentUserId == UserSession.URETICI_ID)
                {
                    if (uploaderId == UserSession.URETICI_ID) // Yani kendisine aitse
                    {
                        yetkiVar = true;
                    }
                }

                // Yetki yoksa durdur
                if (!yetkiVar)
                {
                    MessageBox.Show("Bu belgeyi silmek için yetkiniz yok.\nSadece yetki alanınızdaki belgeleri silebilirsiniz.", "Erişim Engellendi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                // 4. ONAY VE SİLME
                DialogResult cevap = MessageBox.Show($"'{dosyaAdi}' adlı belge Çöp Kutusu'na taşınacak.\nOnaylıyor musunuz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (cevap == DialogResult.Yes)
                {
                    // Artık ID gönderiyoruz, isim değil.
                    SoftDeleteIslemiYap(docId);
                    RefreshDocumentList();
                }
            }
        }
        private void SoftDeleteIslemiYap(int docId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_MoveToRecycleBin", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // DÜZELTİLMİŞ PARAMETRELER:
                        cmd.Parameters.AddWithValue("@DocumentID", docId);
                        cmd.Parameters.AddWithValue("@DeletedByUserID", UserSession.UserId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Silindi.", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void OpenDoc(int id)
        {
            string dosyaYolu = "";

            // 1. Veritabanına bağlanıp dosya yolunu (FilePath) çekiyoruz
            // connectionString değişkeninin yukarıda tanımlı olduğunu varsayıyorum.
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // SQL Sorgusu: ID'si bu olan belgenin yolunu getir
                    string query = "SELECT FilePath FROM Documents WHERE DocumentID = @DocumentID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DocumentID", id);

                        object result = cmd.ExecuteScalar(); // Tek bir bilgi (yol) geleceği için ExecuteScalar
                        if (result != null)
                        {
                            dosyaYolu = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dosya yolu veritabanından alınırken hata: " + ex.Message);
                    return; // Hata varsa devam etme, çık
                }
            }

            // 2. Dosyayı Windows ile Açma İşlemi
            if (!string.IsNullOrEmpty(dosyaYolu) && System.IO.File.Exists(dosyaYolu))
            {
                try
                {
                    // Bu komut dosyayı varsayılan programıyla (Word, PDF okuyucu vs.) açar
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = dosyaYolu,
                        UseShellExecute = true  // <--- İŞTE SİHİRLİ KOD BU!
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dosya açılırken hata oluştu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Dosya bulunamadı!\nAranan Yol: " + dosyaYolu + "\n\nDosya silinmiş veya taşınmış olabilir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BelgeyiAc(int belgeId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Sorgu: Sadece ilgili ID'nin verisini ve uzantısını çekiyoruz
                string query = "SELECT DocumentName, FileType, FileData FROM Documents WHERE DocumentID = @DocumentID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DocumentID", belgeId);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // 1. Veritabanından verileri al
                            byte[] fileData = (byte[])reader["FileData"];
                            string uzanti = reader["FileType"].ToString(); // Örn: .pdf
                            string ad = reader["DocumentName"].ToString();

                            // 2. Geçici bir dosya yolu oluştur
                            // Path.GetTempPath() kullanıcının Temp klasörünü bulur.
                            // Guid.NewGuid() çakışma olmasın diye rastgele isim verir.
                            string tempDosyaYolu = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + uzanti);

                            // 3. Byte dizisini fiziksel dosyaya dönüştür ve kaydet
                            File.WriteAllBytes(tempDosyaYolu, fileData);

                            // 4. Dosyayı varsayılan programla aç
                            try
                            {
                                ProcessStartInfo psi = new ProcessStartInfo(tempDosyaYolu)
                                {
                                    UseShellExecute = true // .NET Core/6+ kullanıyorsan bu true olmalı
                                };
                                Process.Start(psi);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Dosya açılırken hata oluştu: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void dgvDocuments_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Seçilen satırdaki ID hücresini al (ID kolonunun isminin 'colID' olduğunu varsayıyorum)
                // Eğer kolon ismin yoksa index kullan: Rows[e.RowIndex].Cells[0].Value
                int secilenId = Convert.ToInt32(dgvDocuments.Rows[e.RowIndex].Cells["DocumentID"].Value);

                // Fonksiyonu çağır
                BelgeyiAc(secilenId);
            }
        }
    }
}
