using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokladna
{
    internal class CouponItem
    {
        public int Count { get; }
        public string Name { get; }

        public CouponItem(int count, string name)
        {
            Count = count;
            Name = name;
        }
    }

    internal class Coupon
    {
        public string Name { get; }
        public int Price { get; }
        public List<CouponItem> Items { get; }

        public Coupon(string name, int price, List<CouponItem> items)
        {
            Name = name;
            Price = price;
            Items = items;
        }
    }

    internal class CouponException : Exception
    {
        public CouponException(string message) : base(message)
        {
        }
    }

    internal class EmptyDatasetException : Exception
    {
        public EmptyDatasetException(string message) : base(message)
        {
        }
    }
}
