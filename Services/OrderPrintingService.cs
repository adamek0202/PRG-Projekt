using MySqlConnector;
using Pokladna.Configuration;
using Pokladna.Database;
using Serilog;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokladna.Services
{
    internal static class Orderervice
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task ProcessAndSendNewOrderAsync()
        {
            int currentOrderNumber = GetNextOrderNumberFromDb();

            if (currentOrderNumber == -1)
            {
                throw new InvalidOperationException("Nepodařilo se získat číslo objednávky z databáze.");
            }

            // Spustíme odesílání na server na pozadí, abychom neblokovali aplikaci
            await SendOrderAsync(currentOrderNumber);
        }

        private static int GetNextOrderNumberFromDb()
        {
            int assignedNumber = -1;

            using(var trans = DatabaseConnection.Connection.BeginTransaction())
            {
                try
                {
                    string selectSql = "";
                    using(var cmd = new MySqlCommand(selectSql, DatabaseConnection.Connection, trans))
                    {
                        object result = cmd.ExecuteScalar();
                        if(result == null)
                        {
                            throw new InvalidOperationException();
                        }
                        assignedNumber = Convert.ToInt32(result);
                    }

                    int nextNumber = (assignedNumber >= 100) ? 1 : assignedNumber + 1;

                    string updateSql = "";
                    using(var cmd = new MySqlCommand(updateSql, DatabaseConnection.Connection, trans))
                    {
                        cmd.Parameters.AddWithValue("@next", nextNumber);
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    return assignedNumber;
                }
                catch (MySqlException ex)
                {
                    Log.Error($"DB Chyba při generování čísla objednávky. Spouštím rollback. Zpráva: {ex.Message}");
                    try
                    {
                        trans.Rollback();
                    } catch(Exception rollbackEx)
                    {
                        Log.Error($"Selhal i pokus o Rollback transakce: {rollbackEx.Message}");
                    }
                    return -1;
                } catch(Exception ex)
                {
                    Log.Error($"Neočekávaná chyba v transakci: {ex.Message}");
                    try
                    {
                        trans.Rollback();
                    } catch(Exception rollbackEx)
                    {
                        Log.Error($"Chyba při rollbacku: {rollbackEx.Message}");
                    }
                    return -1;
                }
            }
        }

        private static async Task SendOrderAsync(int orderId)
        {
            try
            {
                string targetIp = ConfigManager.Values.Database.Server;
                string url = $"http://{targetIp}:8080/new-order";

                var payload = new { OrderId = orderId };
                string json = JsonSerializer.Serialize(payload);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient.Timeout = TimeSpan.FromSeconds(5);

                HttpResponseMessage response = await HttpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Log.Information($"Objednávka {orderId} byla úspěšně odeslána.");
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Log.Error($"Chyba serveru. Kód: {response.StatusCode}, Tělo: {responseBody}");
                }
            } catch(Exception ex)
            {
                Log.Error($"Chyba při síťové komunikaci pro objednávku {orderId}: {ex.Message}");
            }
        }
    }
}
