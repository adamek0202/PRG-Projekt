using MySqlConnector;
using Pokladna.Database;
using Pokladna.Exceptions;
using System;
using System.Data;

namespace Pokladna.Services
{
    internal static class ProductService
    {
        // Volitelně: Pokud se ceny produktů nemění za běhu často, 
        // mohl bys tu mít podobnou Dictionary cache jako u názvů.

        public static Product GetProduct(int id)
        {
            const string query = "SELECT Name, Price FROM Products WHERE ProductID = @id";

            using (var connection = new MySqlConnection(DatabaseConnection.ConnectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Nelze se spojit s databázovým serverem pro načtení produktu.", ex);
                }

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string name = reader["Name"].ToString() ?? "Neznámý produkt";

                        // MySQL vrací INT jako Int32, Convert.ToInt32 je pro jistotu safe a čisté
                        int price = Convert.ToInt32(reader["Price"]);

                        return new Product(name, price);
                    }

                    // Pokud produkt neexistuje
                    throw new ProductNotFoundException($"Produkt s ID {id} nebyl v databázi nalezen.");
                }
            }
        }
    }
}