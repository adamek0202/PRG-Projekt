using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    internal class Item
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public int ItemPrice { get; set; }
        public int TotalPrice => Amount * ItemPrice;
    }
}
