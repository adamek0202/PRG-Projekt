using System;
using System.Collections.Generic;

namespace Pokladna
{
    internal class Product
    {
        public string Name { get; }
        public int Price { get; }
        public int Count { get; private set; }

        // Seznam podpoložek (např. "s tatarkou", "bez cibule") svázaných přímo s tímto produktem
        public List<string> SubItems { get; } = new List<string>();

        public int TotalPrice => Price * Count;

        public Product(string name, int price, int count = 1)
        {
            Name = name;
            Price = price;
            Count = count;
        }

        public void UpdateCount(int newCount)
        {
            if (newCount < 0) return;
            Count = newCount;
        }
    }
}