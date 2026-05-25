using System;

namespace Pokladna
{
    internal class Sale
    {
        public int Number { get; private set; }
        public DateTime DateAndTime { get; private set; }
        public int Price { get; private set; }
        public string Payment { get; private set; }
        public string User { get; private set; }

        public Sale(int number, DateTime dateAndTime, int price, string payment, string user)
        {
            Number = number;
            DateAndTime = dateAndTime;
            Price = price;
            User = user;

            // Mapování typů plateb
            Payment = payment switch
            {
                "Cash" => "Hotovost",
                "Card" => "Platební karta",
                "FoodCard" => "Stravenková karta",
                _ => payment // Vrátí původní hodnotu, pokud neodpovídá
            };
        }
    }
}