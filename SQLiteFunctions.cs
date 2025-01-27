using System;
using System.Data.SQLite;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Projekt.GlobalPosPrinter;
using static Projekt.Receipt;
using System.Data.SqlClient;

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
        public static int orderId;

        public static void RecordSale(System.Windows.Forms.ListView listView, Payments payment, int price)
        {
            const string querry = "UPDATE Products SET Sold = sold + 1 WHERE Name = @name";

            foreach (ListViewItem item in listView.Items)
            {
                int count = 0;

                if (item.SubItems.Count > 1)
                {
                    count = int.Parse(item.SubItems[2].Text);
                }
                else if (item.Group != null && item.Group.Items.Count > 0 && item.Group.Items[0].SubItems.Count > 1)
                {
                    count = int.Parse(item.Group.Items[0].SubItems[2].Text);
                }

                for (int i = 0; i < count; i++)
                {
                    string productName = item.Text;

                    using (var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
                    {
                        command.Parameters.AddWithValue("@name", productName);
                        Console.WriteLine(command.ExecuteNonQuery());
                    }
                }
            }

            const string query = "INSERT INTO Transactions (Date, Price, Payment) VALUES (@date, @price, @payment)";
            using (var command = new SQLiteCommand(query, DatabaseConnection.Connection))
            {
                command.Parameters.AddWithValue("date", DateTime.Now.ToShortDateString());
                command.Parameters.AddWithValue("price", price.ToString());
                command.Parameters.AddWithValue("payment", payment.ToString());
                command.ExecuteNonQuery();
            }
        }

        private static void AddMenuComponents(MainForm form, int[] componentIds, ListViewGroup group)
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

        public static void HandleButtonPress(MainForm form, int buttonId, int times)
        {
            if (times >= 1)
            {
                var group = new ListViewGroup();
                if (buttonId < 1000)
                {
                    string querry = "SELECT Name, Price FROM Products WHERE ProductID = @ProductID";
                    using (var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
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
                    using (var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
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
                                AddMenuComponents(form, componentsIds, group);
                            }
                        }
                    }
                }
            }
        }

        public static async void SendOrderName(ListView listView, int[] categoryIds)
        {
            foreach (ListViewItem item in listView.Items)
            {
                string itemName = item.Text;
                string querry = "SELECT CategoryId FROM Products WHERE Name = @name";
                using (var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
                {
                    command.Parameters.AddWithValue("@name", itemName);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int categoryId = reader.GetInt32(0);
                            if (categoryIds.Contains(categoryId))
                            {
                                //await SendOrder(orderId);
                                PrintOrder();
                                if (orderId < 99)
                                {
                                    orderId++;
                                }
                                else
                                {
                                    orderId = 1;
                                }
                                return;
                            }
                        }
                    }
                }
            }
        }

        private static void PrintOrder()
        {
            EPrinter.AlignCenter();
            EPrinter.DoubleWidth3();
            EPrinter.Append("VASE OBJEDNAVKA");
            EPrinter.AlignLeft();
            EPrinter.Separator();
            EPrinter.AlignCenter();
            EPrinter.DoubleWidth3();
            EPrinter.Append(orderId.ToString());
            EPrinter.AlignLeft();
            EPrinter.Separator();
            //Info text na spodku
            EPrinter.AlignCenter();
            EPrinter.Append("Sledujte stav vasí objednávky na ");
            EPrinter.Append("obrazovce nad výdejem.");
            EPrinter.AlignLeft();
            EPrinter.NewLines(2);
            EPrinter.PartialPaperCut();
            EPrinter.NormalWidth();
            EPrinter.PrintDocument();
            EPrinter.Clear();
        }

        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task SendOrder(int orderId)
        {
            try
            {
                string url = "http://localhost:8080/new-order"; // Cílová adresa
                var payload = new { orderId = orderId }; // JSON data
                //jebat 
                // Serializace JSON payloadu
                string json = JsonSerializer.Serialize(payload);

                Console.WriteLine("Odesílám JSON:");
                Console.WriteLine(json);

                // Vytvoření obsahu POST požadavku
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Odeslání POST požadavku
                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                // Kontrola výsledku
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Objednávka {orderId} byla úspěšně odeslána.");
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Chyba při odesílání objednávky {orderId}. Kód odpovědi: {response.StatusCode}");
                    Console.WriteLine("Odpověď serveru:");
                    Console.WriteLine(responseBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při odesílání objednávky {orderId}: {ex.Message}");
            }
        }
    }
}