using MySqlConnector;
using System;
using System.Collections.Generic;

namespace Pokladna.Database
{
    internal static class DatabaseValidator
    {
        public static readonly Dictionary<string, HashSet<string>> RequiredTables = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Categories", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Id", "Name" } },

            { "Coupons", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Id", "Code", "Name", "Items", "Price", "Validity", "Maxuses", "Uses" } },

            { "GiftCards", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Id", "Code", "Holder", "Money", "ValidityEnd" } },

            { "Menus", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "MenuID", "Name", "Price", "Components" } },

            { "Products", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "ProductID", "Name", "Price", "CategoryID", "Sold" } },

            { "Sysvars", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Key", "Value" } },
    
            // AKTUALIZOVÁNO: Přidány sloupce Discount a IsHere pro správný tisk a evidenci
            { "Transactions", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "id", "ReceiptID", "DateTime", "Price", "Payment", "User", "Discount", "IsHere" } },
    
            // NOVÉ: Kontrola tabulky pro jednotlivé řádky z SaleItemDto
            { "TransactionItems", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Id", "TransactionNumber", "Name", "Count", "UnitPrice", "IsMenu", "CouponId" } },
    
            // NOVÉ: Kontrola tabulky pro detaily (komponenty menu, kuchařské poznámky z Notes)
            { "TransactionItemDetails", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Id", "ItemId", "DetailType", "Value" } },

            { "Users", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Id", "Password", "FullName", "Position" } }
        };



        public static bool CheckTables()
        {
            // Otevřeme jedno spojení pro celou sadu testů structures
            using (var connection = new MySqlConnection(DatabaseConnection.ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DB Critical] Nelze se vůbec spojit s MySQL serverem: {ex.Message}");
                    return false;
                }

                foreach (var table in RequiredTables)
                {
                    string tableName = table.Key;
                    HashSet<string> expectedColumns = table.Value;

                    if (!TableExists(tableName, connection))
                    {
                        Console.WriteLine($"[DB Error] Na MySQL serveru chybí tabulka: {tableName}");
                        return false;
                    }

                    if (!ColumnsMatch(tableName, expectedColumns, connection))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool TableExists(string tableName, MySqlConnection connection)
        {
            // Dotaz do informační schémy MySQL na existenci tabulky v naší DB
            const string query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @name;";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", tableName);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private static bool ColumnsMatch(string tableName, HashSet<string> expectedColumns, MySqlConnection connection)
        {
            var actualColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // MySQL způsob, jak bezpečně vytáhnout sloupce konkrétní tabulky z aktuální DB
            const string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @name;";

            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", tableName);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        actualColumns.Add(reader["COLUMN_NAME"].ToString());
                    }
                }
            }

            foreach (var column in expectedColumns)
            {
                if (!actualColumns.Contains(column))
                {
                    Console.WriteLine($"[DB Error] V MySQL tabulce '{tableName}' chybí sloupec: '{column}'");
                    return false;
                }
            }

            return true;
        }

        public static bool CheckDatabaseIntegrity()
        {
            // V MySQL kontrolujeme stav spíše přes ANALYZE TABLE u každé vyžadované tabulky
            try
            {
                using (var connection = new MySqlConnection(DatabaseConnection.ConnectionString))
                {
                    connection.Open();
                    foreach (var table in RequiredTables.Keys)
                    {
                        // Spustí analýzu tabulky (obdoba integrity checku pro jednotlivé tabulky)
                        using (var cmd = new MySqlCommand($"ANALYZE TABLE {table};", connection))
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string status = reader["Msg_text"].ToString();
                                if (status != "OK" && status != "Table is already up to date")
                                {
                                    Console.WriteLine($"[DB Warning] Tabulka {table} vykazuje stav: {status}");
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DB Critical] Selhala kontrola stavu MySQL tabulek: {ex.Message}");
                return false;
            }
        }
    }
}