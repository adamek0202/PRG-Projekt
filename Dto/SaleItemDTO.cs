using System;
using System.Collections.Generic;

namespace Pokladna.Dto
{
    public class SaleItemDto
    {
        public string Name { get; set; } = string.Empty;
        public int Count { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice => UnitPrice * Count;

        public bool IsMenu { get; set; }

        // Původní vlastnost zůstává beze změny – nic se nerozsype
        public List<string> ComponentNames { get; set; } = new();

        public string CouponId { get; set; } = string.Empty;

        // Původní vlastnost pro obecné poznámky k jídlu
        public List<string> Notes { get; set; } = new();

        // NOVÉ (PŘIDÁNO): Slovník pro poznámky ke konkrétním komponentám
        // Klíč = název komponenty (např. "Hranolky"), Hodnota = List poznámek (např. "bez soli")
        public Dictionary<string, List<string>> ComponentNotes { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    }
}