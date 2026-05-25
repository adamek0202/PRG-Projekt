using System;
using System.Collections.Generic;

namespace Pokladna
{
    internal class CouponItem
    {
        // Použitím get-only vlastností zajistíme, že data jsou po vytvoření needitovatelná
        public int Count { get; }
        public string Name { get; }

        public CouponItem(int count, string name)
        {
            // Drobná obrana: fastfoodová položka nemůže mít nulové nebo záporné množství
            if (count <= 0) throw new ArgumentException("Počet kusů v kupónu musí být větší než 0.", nameof(count));

            Count = count;
            Name = name ?? throw new ArgumentNullException(nameof(name), "Název položky kupónu nemůže být null.");
        }
    }

    internal class Coupon
    {
        public string Code { get; }
        public string Name { get; }
        public int Price { get; }

        // Použijeme IReadOnlyList, aby nikdo nemohl zvenčí do seznamu položek jen tak dělat .Add() nebo .Clear()
        public IReadOnlyList<CouponItem> Items { get; }

        // Prázdný konstruktor letí do koše, pokud ho vyloženě nevyžaduje starý XmlSerializer.
        // Tím donutíme zbytek aplikace zadat vždy kompletní data.
        public Coupon(string code, string name, int price, List<CouponItem> items)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Kód kupónu musí být vyplněn.", nameof(code));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Název kupónu musí být vyplněn.", nameof(name));
            if (price < 0) throw new ArgumentException("Cena kupónu nemůže být záporná.", nameof(price));

            Code = code;
            Name = name;
            Price = price;
            // Uložíme jako ReadOnly, abychom zamkli i samotný vnitřek listu
            Items = items?.AsReadOnly() ?? throw new ArgumentNullException(nameof(items), "Seznam položek kupónu nemůže být null.");
        }
    }
}