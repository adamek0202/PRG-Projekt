using MySqlConnector;
using System;
using System.Data;

//Databázová logika
//Neprovádět bezdůvodné zásahy, hrozí rozbití aplikace

namespace Pokladna.Database
{
    public static class DatabaseConnection
    {
        public static string ConnectionString =>
            "Server=192.168.0.2;" +
            "Port=3306;" +
            "Database=fastfood_db;" +
            "Uid=adam;" +
            "Pwd=111111;" +
            "Pooling=true;" +      // Zapne connection pooling
            "MinimumPoolSize=5;" + // Udrží 5 spojení neustále otevřených pro rychlou reakci kasy
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