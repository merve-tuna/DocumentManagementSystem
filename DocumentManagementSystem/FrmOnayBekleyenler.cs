using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentManagementSystem
{
    public partial class FrmOnayBekleyenler : Form
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DocumentManagementSystem;Integrated Security=True;TrustServerCertificate=True";
        public FrmOnayBekleyenler()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void FrmOnayBekleyenler_Load(object sender, EventArgs e)
        {
            int currentRoleID = GetCurrentUserRoleID(UserSession.UserId);

        }
        private int GetCurrentUserRoleID(int userId)
        {
            // Bu metodu UserSession sınıfında static olarak tutman en doğrusu olur.
            // Şimdilik örnek olması için buraya yazıyorum.
            int roleId = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RoleID FROM Users WHERE UserID=@UserID", conn);
                cmd.Parameters.AddWithValue("@UserID", userId);
                object result = cmd.ExecuteScalar();
                if (result != null) roleId = Convert.ToInt32(result);
            }
            return roleId;
        }

        private void ListPendingDocuments(int roleID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetPendingDocuments", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CurrentUserRoleID", roleID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvOnayBekleyenler.DataSource = dt;

                    // Gereksiz kolonları gizle (ID gibi)
                    if (dgvOnayBekleyenler.Columns["DocumentID"] != null)
                        dgvOnayBekleyenler.Columns["DocumentID"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        // ONAYLA BUTONU
        private void btnOnayla_Click(object sender, EventArgs e)
        {
            IslemYap(true);
        }

        // REDDET BUTONU
        private void btnReddet_Click(object sender, EventArgs e)
        {
            IslemYap(false);
        }

        private void IslemYap(bool onayDurumu)
        {
            if (dgvOnayBekleyenler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen listeden bir belge seçiniz.");
                return;
            }

            int docId = Convert.ToInt32(dgvOnayBekleyenler.SelectedRows[0].Cells["DocumentID"].Value);
            string redSebebi = ""; // Eğer formda txtRedSebebi varsa oradan alabilirsin.

            if (!onayDurumu) // Eğer reddediliyorsa
            {
                // Basit bir inputbox veya textbox kontrolü
                // redSebebi = txtRedSebebi.Text; 
                DialogResult soru = MessageBox.Show("Belge reddedilecek. Emin misiniz?", "Onay", MessageBoxButtons.YesNo);
                if (soru == DialogResult.No) return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ProcessDocumentApproval", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DocumentID", docId);
                cmd.Parameters.AddWithValue("@IsApproved", onayDurumu); // 1 veya 0
                cmd.Parameters.AddWithValue("@ProcessByUserID", UserSession.UserId);

                if (!string.IsNullOrEmpty(redSebebi))
                    cmd.Parameters.AddWithValue("@RejectionReason", redSebebi);
                else
                    cmd.Parameters.AddWithValue("@RejectionReason", DBNull.Value);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show(onayDurumu ? "Belge yayınlandı." : "Belge reddedildi.");

            // Listeyi Yenile
            int roleID = GetCurrentUserRoleID(UserSession.UserId);
            ListPendingDocuments(roleID);
        }
        private void dgvOnayBekleyenler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
