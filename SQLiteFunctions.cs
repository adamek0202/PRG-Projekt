using System;
using System.Data.SQLite;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Projekt.GlobalPosPrinter;
using System.Collections.Generic;

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
            const string querry = "UPDATE Products SET Sold = Sold + 1 WHERE Name = @name";

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
                                await SendOrder(orderId);
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
                string url = "http://192.168.9.220:8080/new-order"; // Cílová adresa
                var payload = new { orderId = orderId };
                string json = JsonSerializer.Serialize(payload);

                Console.WriteLine("Odesílám JSON:");
                Console.WriteLine(json);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(url, content);
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

        public static async Task ProcessListViewAndSend(ListView listView, string ipAddress)
        {
            var itemsList = new List<object>();
            foreach(ListViewItem item in listView.Items)
            {
                string itemName = item.Text;
                if (IsNormalProduct(itemName))
                {
                    itemsList.Add(new { type = "normal", name = itemName });
                }
                else
                {
                    var components = GetMenuComponents(itemName);
                    itemsList.Add(new { type = "composite", name = itemName, components });
                }
            }

            var jsonPayload = JsonSerializer.Serialize(new { location = "here", items = itemsList }, new JsonSerializerOptions { WriteIndented = true });
            await SendHttpPost(ipAddress, jsonPayload);
        }

        private static bool IsNormalProduct(string name)
        {
            using (var command = new SQLiteCommand("SELECT COUNT(*) FROM products WHERE Name = @name", DatabaseConnection.Connection))
            {
                command.Parameters.AddWithValue("@name", name);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        private static List<object> GetMenuComponents(string menuName)
        {
            var components = new List<object>();

            try
            {
                using (var command = new SQLiteCommand("SELECT components FROM menus WHERE Name = @menuName", DatabaseConnection.Connection))
                {
                    command.Parameters.AddWithValue("@menuName", menuName);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string jsonComponents = reader.GetString(0);
                            Console.WriteLine($"Načtený JSON komponent pro '{menuName}': {jsonComponents}");

                            if (!string.IsNullOrEmpty(jsonComponents))
                            {
                                try
                                {
                                    var componentIds = JsonSerializer.Deserialize<List<int>>(jsonComponents);

                                    foreach (var componentId in componentIds)
                                    {
                                        string productName = GetProductNameById(componentId);
                                        if (!string.IsNullOrEmpty(productName))
                                        {
                                            Console.WriteLine($"Přidávám komponentu: {productName} (ID: {componentId})");
                                            components.Add(new { count = 1, name = productName });
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Upozornění: Produkt s ID {componentId} nebyl nalezen!");
                                        }
                                    }
                                }
                                catch (JsonException jsonEx)
                                {
                                    Console.WriteLine($"Chyba při parsování JSON komponent pro '{menuName}': {jsonEx.Message}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Varování: Žádné komponenty pro '{menuName}'");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Varování: Menu '{menuName}' nebylo nalezeno v databázi!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při získávání komponent pro '{menuName}': {ex.Message}");
            }

            return components;
        }


        private static string GetProductNameById(int productId)
        {
            using (var command = new SQLiteCommand("SELECT Name FROM products WHERE ProductId = @id", DatabaseConnection.Connection))
            {
                command.Parameters.AddWithValue("@id", productId);
                var result = command.ExecuteScalar();
                return result != null ? result.ToString() : null;
            }
        }



        private static async Task SendHttpPost(string ipAddress, string jsonPayload)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"http://{ipAddress}:9000/order", content);

                    Console.WriteLine($"Odpověď serveru: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při odesílání HTTP požadavku: {ex.Message}");
            }
        }
    }
}