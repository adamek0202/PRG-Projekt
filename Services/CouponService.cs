using MySqlConnector;
using Pokladna.Database;
using Pokladna.Exceptions;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Pokladna.Services
{
    internal static class CouponService
    {
        public static Coupon GetCoupon(string code)
        {
            const string query = "SELECT Name, Price, Items, Validity, Maxuses, Uses FROM Coupons WHERE Code = @code";

            using (var connection = new MySqlConnection(DatabaseConnection.ConnectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@code", code);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new CouponValidationException("Kupón nebyl nalezen.");

                    // 1. Datum platnosti - MySQL vrací nativní DateTime objekt, 
                    // takže nemusíme parsovat string přes CultureInfo!
                    DateTime validity = Convert.ToDateTime(reader["Validity"]);
                    if (validity < DateTime.Today)
                        throw new CouponValidationException("Platnost tohoto kupónu již vypršela.");

                    // 2. Kontrola počtu použití
                    int maxUses = Convert.ToInt32(reader["Maxuses"]);
                    int uses = Convert.ToInt32(reader["Uses"]);
                    if (uses >= maxUses)
                        throw new CouponValidationException("Tento kupón již byl vyčerpán.");

                    // 3. Parsování JSON pole IDček produktů
                    int[] productIds;
                    try
                    {
                        productIds = JsonSerializer.Deserialize<int[]>(reader["Items"].ToString()) ?? Array.Empty<int>();
                    }
                    catch (JsonException)
                    {
                        throw new FormatException("Kritická chyba: Neplatný formát JSON dat u kupónu.");
                    }

                    // 4. Transformace na CouponItem
                    var couponItems = new List<CouponItem>();
                    foreach (var id in productIds)
                    {
                        string? productName = DatabaseFunctions.GetProductNameById(id);
                        couponItems.Add(new CouponItem(1, productName ?? "Neznámá položka"));
                    }

                    string couponName = reader["Name"].ToString();
                    int couponPrice = Convert.ToInt32(reader["Price"]);

                    return new Coupon(code, couponName, couponPrice, couponItems);
                }
            }
        }

        // Voláno v rámci transakce z RecordSale
        public static void RecordCouponUsageInternal(string code, MySqlConnection connection, MySqlTransaction transaction)
        {
            const string query = "UPDATE Coupons SET Uses = Uses + 1 WHERE Code = @code";
            using (var command = new MySqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@code", code);
                command.ExecuteNonQuery();
            }
        }
    }
}