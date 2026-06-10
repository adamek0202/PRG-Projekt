using MySqlConnector;
using Pokladna.Dto;
using Pokladna.Exceptions;
using Pokladna.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokladna.Database
{
    internal static class DatabaseFunctions
    {
        public static int orderId;

        private static readonly Dictionary<int, string> ProductNameCache = new();

        /// <summary>
        /// Tuhle metodu zavoláš v administraci kasy, pokud pokladní upraví název produktu v DB, 
        /// aby se vymazala stará cache a kasa neukazovala staré názvy.
        /// </summary>
        public static void ClearProductCache()
        {
            ProductNameCache.Clear();
        }

        /// <summary>
        /// Načte aktuální číslo účtenky z MySQL tabulky Sysvars.
        /// Pokud klíč neexistuje nebo selže DB, vrací 0.
        /// </summary>
        public static int LoadReceiptNumber()
        {
            // `value` dáváme do backticks, protože v MySQL to může být vyhodnoceno jako klíčové slovo
            string query = "SELECT `value` FROM Sysvars WHERE `key` = 'ReceiptNumber' LIMIT 1";

            try
            {
                using (var command = new MySqlCommand(query, DatabaseConnection.Connection))
                {
                    var result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        // MySQL může int vrátit jako Int32, Int64 nebo string podle definice sloupce, 
                        // Convert.ToInt32 je nejpružnější způsob, jak to bezpečně zkonvertovat.
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Chyba při načítání čísla účtenky (ReceiptNumber) z MySQL.");
            }

            return 0; // Výchozí hodnota při chybě nebo absenci záznamu
        }

        /// <summary>
        /// Ověří existenci databáze a vytvoří kompletní strukturu tabulek podle aktuálního schématu.
        /// Využívá centrální připojení z DatabaseConnection.
        /// </summary>
        public static void InitializeDatabaseSchema()
        {
            try
            {
                // 1. Krok: Vytvoření samotné DB (pokud neexistuje). 
                // Použijeme tvůj connection string, ale odmažeme z něj název DB, abychom se připojili k serveru obecně.
                var builder = new MySqlConnectionStringBuilder(DatabaseConnection.ConnectionString)
                {
                    Database = "" // Dočasně vymažeme fastfood_db, protože možná ještě neexistuje
                };

                using (var conn = new MySqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    // Založíme DB se správným kódováním, pokud ještě nebyla vytvořena
                    using (var cmd = new MySqlCommand(
                        "CREATE DATABASE IF NOT EXISTS `fastfood_db` CHARACTER SET utf8mb4 COLLATE utf8mb4_czech_ci;", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                // 2. Krok: Vytvoření tabulek. Tady už jedeme čistě přes tvůj originální ConnectionString.
                // Využíváme blok using, což je s aktivním Connection Poolingem z DatabaseConnection nejrychlejší a nejbezpečnější cesta.
                using (var conn = new MySqlConnection(DatabaseConnection.ConnectionString))
                {
                    conn.Open();

                    // Sysvars
                    ExecuteQuery(conn, @"
                        CREATE TABLE IF NOT EXISTS `Sysvars` (
                            `Key` VARCHAR(50) NOT NULL,
                            `Value` TEXT NOT NULL,
                            PRIMARY KEY (`Key`)
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_czech_ci;");

                    // Categories
                    ExecuteQuery(conn, @"
                        CREATE TABLE IF NOT EXISTS `Categories` (
                            `Id` INT NOT NULL AUTO_INCREMENT,
                            `Name` VARCHAR(100) NOT NULL,
                            PRIMARY KEY (`Id`)
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_czech_ci;");

                    ExecuteQuery(conn, @"
    CREATE TABLE IF NOT EXISTS `Users` (
        `Id` VARCHAR(50) NOT NULL,            -- ID zaměstnance (např. číslo karty, čipu nebo zadané ID kasy)
        `Password` VARCHAR(255) NOT NULL,     -- PIN kód (uložený bezpečně jako hash, nebo string pro přímé porovnání)
        `FullName` VARCHAR(100) NOT NULL,     -- Jméno a příjmení (zobrazuje se na účtence: 'Obsluha: Adam')
        `Position` VARCHAR(50) NOT NULL,      -- Role/Pozice (např. 'Pokladni', 'Manazer', 'Admin')
        PRIMARY KEY (`Id`)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_czech_ci;");

                    // Products
                    ExecuteQuery(conn, @"
                        CREATE TABLE IF NOT EXISTS `Products` (
                            `ProductID` INT NOT NULL AUTO_INCREMENT,
                            `Name` VARCHAR(150) NOT NULL,
                            `Price` INT NOT NULL,
                            `CategoryID` INT NOT NULL,
                            `Sold` INT NOT NULL DEFAULT 0,
                            PRIMARY KEY (`ProductID`),
                            CONSTRAINT `FK_Products_Categories` FOREIGN KEY (`CategoryID`) REFERENCES `Categories` (`Id`) ON DELETE RESTRICT
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_czech_ci;");

                    // Menus
                    ExecuteQuery(conn, @"
                        CREATE TABLE IF NOT EXISTS `Menus` (
                            `MenuID` INT NOT NULL AUTO_INCREMENT,
                            `Name` VARCHAR(150) NOT NULL,
                            `Price` INT NOT NULL,
                            `Components` TEXT NOT NULL,
                            PRIMARY KEY (`MenuID`)
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_czech_ci;");

                    // Coupons
                    ExecuteQuery(conn, @"
                        CREATE TABLE IF NOT EXISTS `Coupons` (
                            `Id` INT NOT NULL AUTO_INCREMENT,
                            `Code` VARCHAR(50) NOT NULL,
                            `Name` VARCHAR(100) NOT NULL,
                            `Items` TEXT NOT NULL,
                            `Price` INT NOT NULL,
                            `Validity` DATETIME NOT NULL,
                            `Maxuses` INT NOT NULL,
                            `Uses` INT NOT NULL DEFAULT 0,
                            PRIMARY KEY (`Id`),
                            UNIQUE KEY `UQ_Coupon_Code` (`Code`)
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_czech_ci;");

                    // GiftCards
                    ExecuteQuery(conn, @"
                        CREATE TABLE IF NOT EXISTS `GiftCards` (
                            `Id` INT NOT NULL AUTO_INCREMENT,
                            `Code` VARCHAR(50) NOT NULL,
                            `Holder` VARCHAR(100) NOT NULL,
                            `Money` INT NOT NULL,
                            `ValidityEnd` DATETIME NOT NULL,
                            PRIMARY KEY (`Id`),
                            UNIQUE KEY `UQ_GiftCard_Code` (`Code`)
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_czech_ci;");

                    // Transactions (Účtenky)
                    ExecuteQuery(conn, @"
                        CREATE TABLE IF NOT EXISTS `Transactions` (
                            `Number` INT NOT NULL AUTO_INCREMENT,
                            `DateTime` DATETIME NOT NULL,
                            `Price` INT NOT NULL,
                            `Payment` VARCHAR(20) NOT NULL,
                            `User` VARCHAR(50) NOT NULL,
                            `Discount` INT NOT NULL DEFAULT 0,
                            `IsHere` TINYINT(1) NOT NULL DEFAULT 1,
                            PRIMARY KEY (`Number`),
                            INDEX `idx_trans_datetime` (`DateTime`),
                            INDEX `idx_trans_user` (`User`)
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_czech_ci;");

                    // TransactionItems (Řádky z SaleItemDto)
                    ExecuteQuery(conn, @"
                        CREATE TABLE IF NOT EXISTS `TransactionItems` (
                            `Id` INT NOT NULL AUTO_INCREMENT,
                            `TransactionNumber` INT NOT NULL,
                            `Name` VARCHAR(255) NOT NULL,
                            `Count` INT NOT NULL,
                            `UnitPrice` INT NOT NULL,
                            `IsMenu` TINYINT(1) NOT NULL DEFAULT 0,
                            `CouponId` VARCHAR(50) DEFAULT '',
                            PRIMARY KEY (`Id`),
                            CONSTRAINT `FK_Items_Transactions` 
                                FOREIGN KEY (`TransactionNumber`) REFERENCES `Transactions` (`Number`) ON DELETE CASCADE
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_czech_ci;");

                    // TransactionItemDetails (Komponenty menu / poznámky kuchařům)
                    ExecuteQuery(conn, @"
                        CREATE TABLE IF NOT EXISTS `TransactionItemDetails` (
                            `Id` INT NOT NULL AUTO_INCREMENT,
                            `ItemId` INT NOT NULL,
                            `DetailType` ENUM('Component', 'Note') NOT NULL,
                            `Value` VARCHAR(255) NOT NULL,
                            PRIMARY KEY (`Id`),
                            CONSTRAINT `FK_Details_Items` 
                                FOREIGN KEY (`ItemId`) REFERENCES `TransactionItems` (`Id`) ON DELETE CASCADE
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_czech_ci;");
                }

                Serilog.Log.Information("Inicializace databázového schématu na síťovém serveru proběhla úspěšně.");
            }
            catch (Exception ex)
            {
                Serilog.Log.Fatal(ex, "Kritická chyba při inicializaci databázového schématu!");
                throw;
            }
        }

        private static void ExecuteQuery(MySqlConnection conn, string sql)
        {
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static UserSession AuthenticateUser(string password)
        {
            // Předpokládám, že pro MySQL používáš připojení z nějaké tvé třídy, např. DatabaseConnection.Connection
            string query = "SELECT FullName, Position FROM Users WHERE Password = @password LIMIT 1";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, DatabaseConnection.Connection))
                {
                    cmd.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int fullNameIndex = reader.GetOrdinal("FullName");
                            int positionIndex = reader.GetOrdinal("Position");

                            // Vytáhneme data do lokálních proměnných
                            string name = reader.IsDBNull(fullNameIndex) ? "Neznámý" : reader.GetString(fullNameIndex);
                            string role = reader.IsDBNull(positionIndex) ? "crew" : reader.GetString(positionIndex);

                            // !!! TADY JE TA ZMĚNA !!! 
                            // Narveme to rovnou do konstruktoru přes kulaté závorky
                            return new UserSession(name, role);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Kritická chyba při autentizaci uživatele přes MySQL DB.");
            }

            return null; // Uživatel nenalezen nebo chyba DB
        }

        public static DataTable ReadFullTable(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Název tabulky nesmí být prázdný.", nameof(tableName));

            if (!DatabaseValidator.RequiredTables.ContainsKey(tableName))
                throw new UnauthorizedAccessException($"Přístup k tabulce '{tableName}' není povolen.");

            var table = new DataTable();
            string query = $"SELECT * FROM {tableName}";

            using (var connection = new MySqlConnection(DatabaseConnection.ConnectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    table.Load(reader);
                }
            }

            if (table.Rows.Count == 0)
                throw new EmptyDatasetException($"Tabulka '{tableName}' je na serveru prázdná.");

            return table;
        }

        public static void RecordSale(Order order)
        {
            using (var connection = new MySqlConnection(DatabaseConnection.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. AKTUALIZACE STATISTIK PRODANÝCH PRODUKTŮ
                        const string productQuery = "UPDATE Products SET Sold = Sold + @count WHERE Name = @name";
                        using (var productCmd = new MySqlCommand(productQuery, connection, transaction))
                        {
                            productCmd.Parameters.Add("@count", MySqlDbType.Int32);
                            productCmd.Parameters.Add("@name", MySqlDbType.VarChar);

                            foreach (var item in order.Items)
                            {
                                // Přeskočíme řádky, které nejsou reálnými produkty (např. textové slevy)
                                if (item.UnitPrice < 0 || item.Name.StartsWith("Sleva")) continue;

                                // Zápis použití kupónu, pokud je u položky přítomen
                                if (!string.IsNullOrEmpty(item.CouponId))
                                {
                                    CouponService.RecordCouponUsageInternal(item.CouponId, connection, transaction);
                                }

                                productCmd.Parameters["@count"].Value = item.Count;
                                productCmd.Parameters["@name"].Value = item.Name;
                                productCmd.ExecuteNonQuery();
                            }
                        }

                        // 2. ZÁPIS HLAVIČKY OBJEDNÁVKY DO TRANSAKCÍ
                        const string transactionQuery = "INSERT INTO Transactions (ReceiptID, DateTime, Price, Payment, User) VALUES (@number, @datetime, @price, @payment, @user)";
                        using (var transCmd = new MySqlCommand(transactionQuery, connection, transaction))
                        {
                            transCmd.Parameters.AddWithValue("@number", order.ReceiptId);
                            transCmd.Parameters.AddWithValue("@datetime", DateTime.Now);
                            transCmd.Parameters.AddWithValue("@price", order.GetTotalPrice());
                            transCmd.Parameters.AddWithValue("@payment", order.PaymentMethod.ToString());
                            transCmd.Parameters.AddWithValue("@user", order.CashierName);

                            transCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new InvalidOperationException("Chyba při zápisu objednávky do MySQL. Transakce stornována.", ex);
                    }
                }
            }
        }

        private static void ExportCSV(string path, DataTable table)
        {
            if(path == string.Empty || table == null || table.Rows.Count > 0)
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Aktualizuje základní údaje kupónu (název a cenu) v MySQL databázi podle jeho kódu.
        /// Volá se z manažerské sekce při editaci.
        /// </summary>
        public static void ModifyCoupon(Coupon coupon)
        {
            if (coupon == null) return;

            string query = "UPDATE Coupons SET Name = @name, Price = @price WHERE Code = @code";

            try
            {
                using (var command = new MySqlCommand(query, DatabaseConnection.Connection))
                {
                    command.Parameters.AddWithValue("@name", coupon.Name);
                    command.Parameters.AddWithValue("@price", coupon.Price);
                    command.Parameters.AddWithValue("@code", coupon.Code);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Serilog.Log.Information("Kupón {CouponCode} byl úspěšně upraven manažerem.", coupon.Code);
                    }
                    else
                    {
                        Serilog.Log.Warning("Pokus o úpravu kupónu {CouponCode} selhal – kód nebyl v DB nalezen.", coupon.Code);
                    }
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Chyba při modifikaci kupónu {CouponCode} v MySQL.", coupon.Code);
            }
        }

        /// <summary>
        /// Načte z MySQL seznam produktů, u kterých byl prodán alespoň 1 kus.
        /// </summary>
        public static List<SoldProduct> GetSoldProductsReport()
        {
            var list = new List<SoldProduct>();
            // V MySQL bereme z tabulky Products jen ty, co mají Sold > 0
            string query = "SELECT ProductId, Name, Price, Sold FROM Products WHERE Sold > 0";

            try
            {
                using (var command = new MySqlCommand(query, DatabaseConnection.Connection))
                using (var reader = command.ExecuteReader())
                {
                    int idIdx = reader.GetOrdinal("ProductId");
                    int nameIdx = reader.GetOrdinal("Name");
                    int priceIdx = reader.GetOrdinal("Price");
                    int soldIdx = reader.GetOrdinal("Sold");

                    while (reader.Read())
                    {
                        list.Add(new SoldProduct(
                            reader.GetInt32(idIdx),
                            reader.IsDBNull(nameIdx) ? "Neznámý produkt" : reader.GetString(nameIdx),
                            reader.GetInt32(priceIdx),
                            reader.GetInt32(soldIdx)
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Chyba při načítání reportu prodejů produktů z MySQL.");
            }

            return list;
        }

        /// <summary>
        /// Inkrementuje počítadlo použití kupónu o +1.
        /// Volá se automaticky při úspěšném dokončení a zaplacení objednávky na kase.
        /// </summary>
        public static void RecordCouponUsage(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return;

            string query = "UPDATE Coupons SET Uses = Uses + 1 WHERE Code = @code";

            try
            {
                using (var command = new MySqlCommand(query, DatabaseConnection.Connection))
                {
                    command.Parameters.AddWithValue("@code", code);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Serilog.Log.Information("Počítadlo použití kupónu {CouponCode} bylo navýšeno.", code);
                    }
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Chyba při zápisu použití kupónu {CouponCode} do MySQL.", code);
            }
        }

        //public static async Task SendOrderName(ListView listView)
        //{
        //    foreach (ListRow item in listView.Items)
        //    {
        //        string itemName = item.Text;
        //        string querry = "SELECT CategoryId FROM Products WHERE Name = @name";
        //        using (var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
        //        {
        //            command.Parameters.AddWithValue("@name", itemName);

        //            using (var reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    int categoryId = reader.GetInt32(0);
        //                    if (new int[] { 1, 2, 3, 4, 7 }.Contains(categoryId))
        //                    {
        //                        PrintOrder();
        //                        await SendOrder(orderId);
        //                        if (orderId < 99)
        //                        {
        //                            orderId++;
        //                        }
        //                        else
        //                        {
        //                            orderId = 1;
        //                        }
        //                        return;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Nová autentizace: Ověří uživatele kombinací osobního čísla (nebo UID karty) a PINu.
        /// </summary>
        public static UserSession AuthenticateUser(string personalNumber, string pin)
        {
            if (string.IsNullOrWhiteSpace(personalNumber) || string.IsNullOrWhiteSpace(pin)) return null;

            string query = "SELECT FullName, Position FROM Users WHERE PersonalNumber = @personalNumber AND Pin = @pin LIMIT 1";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, DatabaseConnection.Connection))
                {
                    cmd.Parameters.AddWithValue("@personalNumber", personalNumber);
                    cmd.Parameters.AddWithValue("@pin", pin); // V budoucnu doporučuji hashovat (např. SHA256)

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int fullNameIdx = reader.GetOrdinal("FullName");
                            int positionIdx = reader.GetOrdinal("Position");

                            return new UserSession(
                                reader.IsDBNull(fullNameIdx) ? "Neznámý" : reader.GetString(fullNameIdx),
                                reader.IsDBNull(positionIdx) ? "crew" : reader.GetString(positionIdx)
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Kritická chyba při autentizaci přes Osobní číslo + PIN.");
            }

            return null;
        }

        /// <summary>
        /// Kontrola existence podle osobního čísla (aby v systému nebyla dvě stejná čísla/karty).
        /// </summary>
        public static bool CheckEmployeeExistence(string personalNumber)
        {
            if (string.IsNullOrEmpty(personalNumber)) return false;

            string query = "SELECT 1 FROM Users WHERE PersonalNumber = @personalNumber LIMIT 1";

            try
            {
                using (var command = new MySqlCommand(query, DatabaseConnection.Connection))
                {
                    command.Parameters.AddWithValue("@personalNumber", personalNumber);
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Chyba při kontrole existence zaměstnance podle osobního čísla.");
                return false;
            }
        }

        /// <summary>
        /// Vytvoření zaměstnance s osobním číslem a PINem.
        /// </summary>
        public static bool CreateEmployee(string name, string personalNumber, string pin, string position)
        {
            // Kontrola, jestli už toto osobní číslo/kartu nemá někdo jiný
            if (CheckEmployeeExistence(personalNumber))
            {
                Serilog.Log.Warning("Pokus o vytvoření uživatele {FullName} selhal – Osobní číslo {Num} už existuje.", name, personalNumber);
                return false;
            }

            string query = "INSERT INTO Users (FullName, PersonalNumber, Pin, Position) VALUES (@fullname, @personalNumber, @pin, @position)";

            try
            {
                using (var command = new MySqlCommand(query, DatabaseConnection.Connection))
                {
                    command.Parameters.AddWithValue("@fullname", name);
                    command.Parameters.AddWithValue("@personalNumber", personalNumber);
                    command.Parameters.AddWithValue("@pin", pin);
                    command.Parameters.AddWithValue("@position", position);

                    command.ExecuteNonQuery();
                    Serilog.Log.Information("Zaměstnanec {FullName} byl vytvořen s osobním číslem {Num}.", name, personalNumber);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Chyba při zápisu zaměstnance {FullName} s PINem do MySQL.", name);
                return false;
            }
        }

        /// <summary>
        /// Načte historii transakcí z MySQL. Volitelně filtruje podle jména pokladního.
        /// </summary>
        public static List<Sale> LoadTransactions(string user = "")
        {
            var sales = new List<Sale>();

            // Sloupce a tabulku `User` obalujeme do backticks, protože User je v MySQL rezervované slovo!
            string query = "SELECT Number, DateTime, Price, Payment, `User` FROM Transactions";

            if (!string.IsNullOrEmpty(user))
            {
                query += " WHERE `User` = @user";
            }

            try
            {
                using (var command = new MySqlCommand(query, DatabaseConnection.Connection))
                {
                    if (!string.IsNullOrEmpty(user))
                    {
                        command.Parameters.AddWithValue("@user", user);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        // Zjistíme indexy sloupců dynamicky podle názvu
                        int numberIdx = reader.GetOrdinal("Number");
                        int dateTimeIdx = reader.GetOrdinal("DateTime");
                        int priceIdx = reader.GetOrdinal("Price");
                        int paymentIdx = reader.GetOrdinal("Payment");
                        int userIdx = reader.GetOrdinal("User");

                        while (reader.Read())
                        {
                            sales.Add(new Sale(
                                reader.GetInt32(numberIdx),
                                reader.GetDateTime(dateTimeIdx), // Nativní načtení DateTime bez parsování stringu
                                reader.GetInt32(priceIdx),
                                reader.IsDBNull(paymentIdx) ? "Cash" : reader.GetString(paymentIdx),
                                reader.IsDBNull(userIdx) ? "Neznámý" : reader.GetString(userIdx)
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Chyba při načítání transakcí z MySQL (filtr uživatele: {User}).", user);
            }

            return sales;
        }

        /// <summary>
        /// Vyhledá dárkovou kartu v MySQL podle jejího UID kódu.
        /// </summary>
        public static GiftCard GetGiftCardByUid(string uid)
        {
            string query = "SELECT Code, Holder, Balance FROM GiftCards WHERE Code = @code LIMIT 1";

            try
            {
                using (var cmd = new MySqlCommand(query, DatabaseConnection.Connection))
                {
                    cmd.Parameters.AddWithValue("@code", uid);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string code = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                            string holder = reader.IsDBNull(1) ? "Neznámý" : reader.GetString(1);
                            int balance = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);

                            // !!! TADY JE TA ZMĚNA !!!
                            // Voláme kulaté závorky konstruktoru a předáváme proměnné
                            return new GiftCard(code, holder, balance);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Chyba při čtení dárkové karty {CardUid} z MySQL.", uid);
            }

            return null; // Karta nenalezena
        }

        public static string? GetProductNameById(int productId)
        {
            if (ProductNameCache.TryGetValue(productId, out string? cachedName))
            {
                return cachedName;
            }

            const string query = "SELECT Name FROM Products WHERE ProductID = @id";

            using (var connection = new MySqlConnection(DatabaseConnection.ConnectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", productId);
                connection.Open();

                string? productName = command.ExecuteScalar()?.ToString();

                if (productName != null)
                {
                    ProductNameCache[productId] = productName;
                }

                return productName;
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