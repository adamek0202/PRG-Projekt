using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokladna
{
    internal class Product
    {
        public string Name { get; }
        public int Price { get; }

        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }

    internal class ProductNotFoundException : Exception
    {
        public ProductNotFoundException()
        {
        }
    }
}
