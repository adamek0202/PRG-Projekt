using System;

namespace Pokladna.Dto
{
    public class MenuDto
    {
        public string Name { get; }
        public int Price { get; }
        public int[] ComponentIds { get; }

        public MenuDto(string name, int price, int[] componentIds)
        {
            Name = name;
            Price = price;
            ComponentIds = componentIds ?? Array.Empty<int>();
        }
    }
}
