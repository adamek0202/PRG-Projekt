using System;
using System.Data.SQLite;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Pokladna.GlobalPosPrinter;
using System.Collections.Generic;
using Projekt.Forms;

//Databázová logika
//Neprovádět bezdůvodné zásahy, hrozí rozbití aplikace

namespace Pokladna
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

    internal static class DatabaseFunctions
    {
        public static int orderId;

        public static int LoadReceiptNumber()
        {
            const string querry = "SELECT value from Sysvars WHERE key = 'ReceiptNumber'";
            using(var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
            {
                using(var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return int.Parse(reader["value"].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public static bool CheckTables()
        {
            var requiredTables = new Dictionary<string, List<string>>
            {
                { "Categories", new List<string> { "Id", "Name" } },
                {"Coupons", new List<string> {"Id", "Code", "Name", "Items", "Price", "Validity", "Maxuses", "Uses"} },
                {"GiftCards", new List<string> {"Id", "Code", "Holder","Money", "ValidityEnd"} },
                {"Menus", new List<string> {"MenuID", "Name", "Price", "Components"} },
                {"Products", new List<string> {"ProductID", "Name", "Price", "CategoryID", "Sold"} },
                {"Sysvars", new List<string> {"Key", "Value"} },
                {"Transactions", new List<string> {"Id", "Number", "DateTime", "Price", "Payment", "User"} },
                {"Users", new List<string> {"Id", "Password", "FullName", "Position"} }
            };

            foreach (var table in requiredTables)
            {
                string tableName = table.Key;
                List<string> expectedColumns = table.Value;

                if (!TableExists(tableName))
                {
                    Console.WriteLine($"Chybí tabulka: {tableName}");
                    return false;
                }

                if (!ColumnsMatch(tableName, expectedColumns))
                {
                    Console.WriteLine($"Tabulka {tableName} má neodpovídající sloupce.");
                    return false;
                }
            }
            return true;
        }

        private static bool TableExists(string tableName)
        {
            using (var cmd = new SQLiteCommand(
                "SELECT name FROM sqlite_master WHERE type='table' AND name=@name;", DatabaseConnection.Connection))
            {
                cmd.Parameters.AddWithValue("@name", tableName);
                var result = cmd.ExecuteScalar();
                return result != null;
            }
        }

        private static bool ColumnsMatch(string tableName, List<string> expectedColumns)
        {
            var actualColumns = new List<string>();

            using (var cmd = new SQLiteCommand($"PRAGMA table_info({tableName});", DatabaseConnection.Connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    actualColumns.Add(reader["name"].ToString());
                }
            }

            foreach (var column in expectedColumns)
            {
                if (!actualColumns.Contains(column))
                    return false;
            }

            return true;
        }

        public static bool CheckDatabaseIntegrity()
        {
            using (var cmd = new SQLiteCommand("PRAGMA integrity_check;", DatabaseConnection.Connection))
            {
                var result = cmd.ExecuteScalar()?.ToString();
                return result == "ok";
            }
        }


        public static void RecordSale(System.Windows.Forms.ListView listView, Payments payment, int price)
        {
            const string querry = "UPDATE Products SET Sold = Sold + 1 WHERE Name = @name";

            foreach (ListViewItem item in listView.Items)
            {
                if (item.Text != "Sleva")
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
                            command.ExecuteNonQuery();
                        }
                    } 
                }
            }

            const string query = "INSERT INTO Transactions (Number, DateTime, Price, Payment, User) VALUES (@number, @datetime, @price, @payment, @user)";
            using (var command = new SQLiteCommand(query, DatabaseConnection.Connection))
            {
                command.Parameters.AddWithValue("number", receiptId);
                command.Parameters.AddWithValue("datetime", DateTime.Now);
                command.Parameters.AddWithValue("price", price.ToString());
                command.Parameters.AddWithValue("payment", payment.ToString());
                command.Parameters.AddWithValue("user", MainForm.Cashier);
                command.ExecuteNonQuery();
            }
        }

        private static void AddMenuComponents(MainForm form, int[] componentIds, ListViewGroup group)
        {
            string querry = "SELECT Name FROM Products WHERE ProductID = @ProductID";

            foreach (var componentId in componentIds)
            {
                using (var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
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
                                if (!MainForm.PriceCheck)
                                {
                                    form.AddHeadItem(name, price, times, group); 
                                }
                                else
                                {
                                    MessageBox.Show($"Cena položky {name} je {price} Kč", "Cena", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MainForm.PriceCheck = false;
                                }
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

                                if (!MainForm.PriceCheck)
                                {
                                    form.AddHeadItem(name, price, times, group, true);
                                    var componentsIds = System.Text.Json.JsonSerializer.Deserialize<int[]>(componentsJson);
                                    AddMenuComponents(form, componentsIds, group); 
                                }
                                else
                                {
                                    MessageBox.Show($"Cena položky {name} je {price} Kč", "Cena", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MainForm.PriceCheck = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        

        public static async void SendOrderName(ListView listView)
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
                            if (new int[] { 1, 2, 3, 4, 7 }.Contains(categoryId))
                            {
                                PrintOrder();
                                await SendOrder(orderId);
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

        public static bool CheckEmployeeExistence(string password)
        {
            string querry = $"SELECT EXISTS(SELECT 1 FROM Users WHERE Password = @password LIMIT 1)";
            using(var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
            {
                command.Parameters.AddWithValue("password", password);
                return Convert.ToBoolean(command.ExecuteScalar());
            }
        }

        public static bool CreateEmployee(string name, string password, string position)
        {
            if (!CheckEmployeeExistence(password))
            {
                string querry = "INSERT INTO Users (Password, FullName, Position) VALUES (@password, @fullname, @position)";
                using(var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
                {
                    command.Parameters.AddWithValue("password", password);
                    command.Parameters.AddWithValue("fullname", name);
                    command.Parameters.AddWithValue("position", position);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            return false;
        }

        public static string[] GetEmployees(string position)
        {
            List<string> values = new List<string>();

            string querry = "SELECT FullName from Users WHERE (Position = @position) OR (Position = manager)";
            using(var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
            {
                command.Parameters.AddWithValue("position", position);
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        values.Add(reader["FullName"].ToString());
                    }
                }
            }
            return values.ToArray();
        }

        public static bool RemoveEmployee(string name)
        {
            string querry = "DELETE FROM Users WHERE FullName = @name";
            using(var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
            {
                command.Parameters.AddWithValue("name", name);
                return command.ExecuteNonQuery() > 0;
            }
        }

        public static List<Sale> LoadTransactions(string user = "")
        {
            List<Sale> sales = new List<Sale>();
            string querry = "SELECT Number, DateTime, Price, Payment, User FROM Transactions";
            if(user != string.Empty)
            {
                querry += " WHERE User = @user";
            }
            using (var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
            {
                if(user != string.Empty)
                {
                    command.Parameters.AddWithValue("user", user);
                }
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sales.Add(new Sale(
                            int.Parse(reader["number"].ToString()),
                            DateTime.Parse(reader["DateTime"].ToString()),
                            int.Parse(reader["Price"].ToString()),
                            reader["Payment"].ToString(),
                            reader["User"].ToString()
                        ));
                    }
                }
            }
            return sales;
        }

        private static void PrintOrder()
        {
            if (EPrinter != null)
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
        }

        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task SendOrder(int orderId)
        {
            try
            {
                string url = "http://192.168.0.2:8080/new-order"; // Cílová adresa
                var payload = new { OrderId = orderId };
                string json = System.Text.Json.JsonSerializer.Serialize(payload);

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