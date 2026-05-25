using MySqlConnector;
using Pokladna.Database;
using Pokladna.Dto;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Pokladna.Services
{
    internal static class MenuService
    {
        public static MenuDto GetMenu(int menuId)
        {
            const string query = "SELECT Name, Price, Components FROM Menus WHERE MenuID = @id";

            using (var connection = new MySqlConnection(DatabaseConnection.ConnectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", menuId);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string name = reader["Name"].ToString() ?? "Neznámé menu";
                        int price = Convert.ToInt32(reader["Price"]);
                        string jsonComponents = reader["Components"].ToString() ?? "[]";

                        int[] componentIds = JsonSerializer.Deserialize<int[]>(jsonComponents) ?? Array.Empty<int>();

                        return new MenuDto(name, price, componentIds);
                    }

                    throw new KeyNotFoundException($"Menu s ID {menuId} nebylo v databázi nalezeno.");
                }
            }
        }

        /// <summary>
        /// Vrátí seznam komponent (produktů) pro dané Menu podle jeho ID.
        /// Využívá paměťovou cache pro názvy produktů, takže nezatěžuje síť!
        /// </summary>
        public static List<CouponItem> GetMenuComponents(int menuId)
        {
            var componentList = new List<CouponItem>();

            try
            {
                // 1. Vytáhneme data o menu (jediný rychlý dotaz do MySQL)
                var menu = GetMenu(menuId);

                // 2. Projdeme IDčka komponent
                foreach (int productId in menu.ComponentIds)
                {
                    // Tady saháme do naší rychlé paměťové cache v DatabaseFunctions!
                    // Žádný nový SQL dotaz do sítě se nekoná, pokud už produkt v RAMce máme.
                    string? productName = DatabaseFunctions.GetProductNameById(productId);

                    if (!string.IsNullOrEmpty(productName))
                    {
                        // Použijeme tvůj model CouponItem (nebo SaleItemDto), 
                        // abychom nemuseli vracet hnusný a nebezpečný "object"
                        componentList.Add(new CouponItem(1, productName));
                    }
                    else
                    {
                        Console.WriteLine($"[DB Warning] Produkt s ID {productId} v menu {menu.Name} nebyl v DB nalezen!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DB Critical] Selhalo načtení komponent pro menu ID {menuId}: {ex.Message}");
            }

            return componentList;
        }
    }
}
