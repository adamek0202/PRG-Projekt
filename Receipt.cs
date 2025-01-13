using System;
using System.Collections.Generic;

namespace Projekt
{
    internal class Receipt
    {
        public DateTime Date { get; private set; } = DateTime.Now;
        public List<Item> Items { get; set; } = new List<Item>();
        public double Total { get; set; }
    }
}
