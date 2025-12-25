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
        // 1. BAĞLANTI ADRESİN (Kendi bilgisayar adına göre kontrol et)
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DocumentManagementSystem;Integrated Security=True;TrustServerCertificate=True";

        // --- DEĞİŞKENLER ---
        private int _currentUserId = 1;
        private string _currentUserRole = "Admin";

        private string selectedFilePath = "";
        private bool hasUnsavedChanges = false;

        // --- YAPICI METOD (Program ilk açıldığında burası çalışır) ---
        public FrmBelgeEkle()
        {
            InitializeComponent();
            SetupUI();

            // DİKKAT: Artık eski metodları değil, bunu çağırıyoruz
            LoadComboBoxes();
        }

        // --- BAŞLANGIÇ AYARLARI ---
        private void SetupUI()
        {
            pnlDropZone.AllowDrop = true;

            // Event Bağlantıları
            this.FormClosing += FrmBelgeEkle_FormClosing;

            pnlDropZone.DragEnter += pnlDropZone_DragEnter;
            pnlDropZone.DragLeave += pnlDropZone_DragLeave;
            pnlDropZone.DragDrop += pnlDropZone_DragDrop;
            pnlDropZone.Click += pnlDropZone_Click;

            btnAction.Click += btnAction_Click;
            btnClear.Click += btnClear_Click;

            // Değişiklik takibi
            if (txtDocName != null) txtDocName.TextChanged += (s, e) => hasUnsavedChanges = true;
            if (cmbDepartment != null) cmbDepartment.SelectedIndexChanged += (s, e) => hasUnsavedChanges = true;
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
            // Validasyonlar
            if (string.IsNullOrWhiteSpace(txtDocName.Text) ||
                cmbDepartment.SelectedIndex == -1 ||
                cmbCategory.SelectedIndex == -1 ||
                string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz ve bir dosya seçiniz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    int statusId = (_currentUserRole == "Admin") ? 3 : 2;

                    string query = @"INSERT INTO Documents 
                                    (DocumentName, DepartmentID, CategoryID, Description, UploadedByUserID, StatusID, UploadDate) 
                                    VALUES 
                                    (@Name, @Dept, @Cat, @Desc, @User, @Status, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Parametreleri Güvenli Ekleme
                        cmd.Parameters.AddWithValue("@Name", txtDocName.Text);

                        // ID'leri int'e çevirerek alıyoruz (Hata buradaydı, artık çözüldü)
                        cmd.Parameters.AddWithValue("@Dept", Convert.ToInt32(cmbDepartment.SelectedValue));
                        cmd.Parameters.AddWithValue("@Cat", Convert.ToInt32(cmbCategory.SelectedValue));

                        cmd.Parameters.AddWithValue("@Desc", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@Path", selectedFilePath);
                        cmd.Parameters.AddWithValue("@User", _currentUserId);
                        cmd.Parameters.AddWithValue("@Status", statusId);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Belge başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnAction.Enabled = false;
                            btnClear.Enabled = true;
                            hasUnsavedChanges = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası:\n" + ex.Message);
                }
            }
        }

        // --- BUTON TIKLAMALARI ---
        private void btnAction_Click(object sender, EventArgs e)
        {
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
            lblSelectedFile.ForeColor = Color.Green;
            if (string.IsNullOrWhiteSpace(txtDocName.Text)) txtDocName.Text = Path.GetFileNameWithoutExtension(path);
            hasUnsavedChanges = true;
        }

        // Boş eventler (Hata vermemesi için kalsın)
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void lblSelectedFile_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}