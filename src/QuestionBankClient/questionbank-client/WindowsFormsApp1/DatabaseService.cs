using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuestionBankClient
{
    public static class DatabaseService
    {
        // kapcsolat App.config-ból
        private static readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;

        // kapcsolat
        public static SqlConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("Hiba: A 'DefaultConnection' nem található az App.config fájlban!");
            }
            return new SqlConnection(_connectionString);
        }

        // teszt metódus
        public static bool IsConnectionWorking()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}