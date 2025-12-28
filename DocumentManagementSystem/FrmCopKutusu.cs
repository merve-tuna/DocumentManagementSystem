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
using static System.Collections.Specialized.BitVector32;

namespace DocumentManagementSystem
{
    public partial class FrmCopKutusu : Form
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DocumentManagementSystem;Integrated Security=True;TrustServerCertificate=True";
        public FrmCopKutusu()
        {
            InitializeComponent();
            LoadTrashData();
        }

       

       
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadTrashData()
        {
            // Veritabanı bağlantı sınıfını kullandığını varsayıyorum (SqlHelper veya benzeri)
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetDeletedDocuments", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvTrash.DataSource = dt;

                    // İstersen kolon başlıklarını düzeltebilirsin
                    // dgvTrash.Columns["DocumentName"].HeaderText = "Belge Adı";
                    // dgvTrash.Columns["DeletedBy"].HeaderText = "Silen Kişi";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        // Form yüklendiğinde çalışacak event
        private void FrmCopKutusu_Load(object sender, EventArgs e)
        {
            LoadTrashData();
        }



    }

}
