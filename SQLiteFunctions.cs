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

    internal static class DatabaseFunctions
    {
        public static void RecordSale(System.Windows.Forms.ListView listView, Payments payment, int price)
        {
            var connection = DatabaseConnection.Connection;
            const string querry = "UPDATE Products SET Sold = sold + 1 WHERE Name = @name";

            foreach (ListViewItem item in listView.Items)
            {
                // Zjisti počet podle pravidel
                int count = 0;

                if (item.SubItems.Count > 1) // Pokud má položka 2 subpoložky
                {
                    count = int.Parse(item.SubItems[2].Text); // Počet je v druhé subpoložce
                }
                else if (item.Group != null && item.Group.Items.Count > 0 && item.Group.Items[0].SubItems.Count > 1)
                {
                    // Pokud nemá subpoložky, vezmi počet z první položky skupiny
                    count = int.Parse(item.Group.Items[0].SubItems[2].Text);
                }

                // Proveď SQL příkaz pro každý počet
                for (int i = 0; i < count; i++)
                {
                    string productName = item.Text;

                    using (var command = new SQLiteCommand(querry, connection))
                    {
                        command.Parameters.AddWithValue("@name", productName);
                        Console.WriteLine(command.ExecuteNonQuery());
                    }
                }
            }

            const string query = "INSERT INTO Transactions (Date, Price, Payment) VALUES (@date, @price, @payment)";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("date", DateTime.Now.ToShortDateString());
                command.Parameters.AddWithValue("price", price.ToString());
                command.Parameters.AddWithValue("payment", payment.ToString());
                command.ExecuteNonQuery();
            }
        }

        private static void AddMenuComponents(MainForm form ,int[] componentIds, ListViewGroup group)
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
                            form.AddSubItem(new[] { componentName }, group);
                        }
                    }
                }
            }
        }

        public static void HandleButtonPress(MainForm form ,int buttonId, int times)
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
                        command.Parameters.AddWithValue("@ProductID", buttonId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string name = reader["Name"].ToString();
                                int price = Convert.ToInt32(reader["Price"]) * times;
                                form.AddHeadItem(name, price, times, group);
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

                                form.AddHeadItem(name, price, times, group);

                                var componentsIds = System.Text.Json.JsonSerializer.Deserialize<int[]>(componentsJson);
                                AddMenuComponents(form ,componentsIds, group);
                            }
                        }
                    }
                }
            } 
        }
    }
}