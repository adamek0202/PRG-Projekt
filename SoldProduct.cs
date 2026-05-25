using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokladna
{
    internal class SoldProduct
    {
        public int Id { get; }
        public string Name { get; }
        public int Price { get; }
        public int Count { get; }

        public SoldProduct(int id, string name, int price, int count)
        {
            Id = id;
            Name = name;
            Price = price;
            Count = count;
        }
    }
}
