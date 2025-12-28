
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; // Eğer App.config'den okuyacaksan burası gerekli

namespace DocumentManagementSystem
{
    public static class SqlHelper
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DocumentManagementSystem;Integrated Security=True;TrustServerCertificate=True";

        // 1. VERİ ÇEKME (Listeleme ve ComboBox doldurma için)
        public static DataTable GetDataByProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd)) { da.Fill(dt); }
                    }
                    catch (Exception ex) { throw new Exception($"GetData Hatası [{procedureName}]: " + ex.Message); }
                }
            }
            return dt;
        }


        // Stored Procedure ile INSERT/UPDATE/DELETE
        public static int ExecuteProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Stored Procedure hatası [{procedureName}]: " + ex.Message);
                    }
                }
            }
        }


    }
}
