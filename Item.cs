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
