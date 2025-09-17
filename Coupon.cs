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
        public string Code { get; }
        public string Name { get; }
        public int Price { get; }
        public List<CouponItem> Items { get; }

        public Coupon(string code, string name, int price, List<CouponItem> items)
        {
            Code = code;
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
}
