using System;
using System.Data.SQLite;
using System.Windows.Forms;

//Databázová logika
//Neprovádět bezdůvodné zásahy, hrozí rozbití aplikace

namespace Projekt
{
    public static class DatabaseConnection
    {
        private static SQLiteConnection _connection;

        public static SQLiteConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SQLiteConnection("Data source=pokladna.db;Version=3;");
                    _connection.Open();
                }
                return _connection;
            }
        }

        public static void CloseConnection()
        {
            _connection?.Close();
            _connection = null;
        }
    }

    public partial class MainForm : Form
    {
        private void AddMenuComponents(int[] componentIds, ListViewGroup group)
        {
            var connection = DatabaseConnection.Connection;
            string querry = "SELECT Name FROM Products WHERE ProductID = @ProductID";

            foreach (var componentId in componentIds)
            {
                using (var command = new SQLiteCommand(querry, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", componentId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string componentName = reader["Name"].ToString();
                            AddSubItem(new[] { componentName }, group);
                        }
                    }
                }
            }
        }

        private void HandleButtonPress(int buttonId, int times)
        {
            if (times >= 1)
            {
                var connection = DatabaseConnection.Connection;
                var group = new ListViewGroup();
                if (buttonId < 1000)
                {
                    string querry = "SELECT Name, Price FROM Products WHERE ProductID = @ProductID";
                    using (var command = new SQLiteCommand(querry, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", buttonId); // Corrected parameter name
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string name = reader["Name"].ToString();
                                int price = Convert.ToInt32(reader["Price"]) * times;
                                AddHeadItem(name, price, times, group);
                                //NativeFunctions.ForceShowScrollBar(listView1.Handle);
                            }
                            else
                            {
                                MessageBox.Show($"Produkt s ID {buttonId} nebyl v databázi nalezen\nKontaktujte prosím správce systému", "Systémová chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else if (buttonId >= 1000)
                {
                    string querry = "SELECT Name, Price, Components FROM Menus WHERE MenuID = @MenuID";
                    using (var command = new SQLiteCommand(querry, connection))
                    {
                        command.Parameters.AddWithValue("@MenuID", buttonId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int price;
                                string name = reader["Name"].ToString();
                                try
                                {
                                    price = Convert.ToInt32(reader["Price"]) * times;
                                }
                                catch
                                {
                                    MessageBox.Show("Nastala chyba při načítání položky z databáze\nKontaktujte prosím správce systému", "Systémová chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                string componentsJson = reader["Components"].ToString();

                                AddHeadItem(name, price, times, group);

                                var componentsIds = System.Text.Json.JsonSerializer.Deserialize<int[]>(componentsJson);
                                AddMenuComponents(componentsIds, group);
                            }
                        }
                    }
                }
            } 
        }
    }
}