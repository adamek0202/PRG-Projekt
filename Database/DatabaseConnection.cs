using MySqlConnector;
using Pokladna.Configuration;
using System;
using System.Data;

//Databázová logika
//Neprovádět bezdůvodné zásahy, hrozí rozbití aplikace

namespace Pokladna.Database
{
    internal static class DatabaseConnection
    {
        public static string ConnectionString =>
            $"Server={ConfigManager.Values.Database.Server};" +
            $"Port={ConfigManager.Values.Database.Port};" +
            $"Database={ConfigManager.Values.Database.DatabaseName};" +
            $"Uid={ConfigManager.Values.Database.User};" +
            $"Pwd={ConfigManager.Values.Database.Password};" +
            $"Connection Timeout={ConfigManager.Values.Database.Timeout};" +
            $"Pooling=true;" +
            "MinimumPoolSize=5;" +
            "AllowUserVariables=True;" +
            "MaximumPoolSize=50;";

        private static MySqlConnection _connection;

        public static MySqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    // Connection string pro síťovou DB (ideálně načítat z config souboru kasy)

                    _connection = new MySqlConnection(ConnectionString);
                }

                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }

                return _connection;
            }
        }

        public static bool IsConnectionValid(out string error)
        {
            try
            {
                var _ = Connection; // vyvolá inicializaci
                error = null;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public static void CloseConnection()
        {
            _connection?.Close();
            _connection = null;
        }
    }
}