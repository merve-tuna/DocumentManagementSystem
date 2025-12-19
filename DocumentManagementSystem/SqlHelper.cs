using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; // Eğer App.config'den okuyacaksan burası gerekli

namespace DocumentManagementSystem
{
    public static class SqlHelper
    {
        // ÖNERİ: Bağlantı adresini "." yaparak her bilgisayarda çalışmasını sağla.
        // Eğer App.config kullanmıyorsan bu şekilde kalabilir:
        private static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=odev2;Integrated Security=True;TrustServerCertificate=True";

        // ALTERNATİF: App.config dosyasından okumak istersen (Daha profesyonel):
        // private static string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;

        // Veri Çekme Fonksiyonu (SELECT)
        public static DataTable GetData(string query, SqlParameter[] parameters = null)
        {
            // DataTable'ı dışarıda tanımlayıp using içinde doldurmak daha güvenlidir.
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Hata mesajını özelleştirebilir veya loglayabilirsin
                        throw new Exception("Veri çekme hatası: " + ex.Message);
                    }
                }
            }
            return dt;
        }

        // Veri Kaydetme/Güncelleme/Silme Fonksiyonu (INSERT, UPDATE, DELETE)
        public static int ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery(); // Etkilenen satır sayısını döner
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("İşlem hatası: " + ex.Message);
                    }
                }
            }
        }
    }
}