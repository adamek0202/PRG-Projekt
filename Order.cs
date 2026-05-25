using Pokladna.Database;
using Pokladna.Dto;
using Pokladna.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokladna
{
    internal class Order
    {
        private readonly List<SaleItemDto> _items = new();

        // --- Metadata objednávky (vyžadovaná přes PosContext) ---
        public int ReceiptId { get; set; }
        public DateTime CreatedAt { get; private set; }
        public string CashierName { get; set; } = string.Empty;
        public Payments PaymentMethod { get; set; } = Payments.Cash;
        public int ManualDiscountPercentage { get; private set; } = 0;

        // Nové vlastnosti pro typ prodeje a hotovost od zákazníka
        public bool IsTakeAway { get; set; } = false;
        public int AmountTendered { get; set; } = 0;

        // Veřejný přístup k položkám pouze pro čtení
        public IReadOnlyList<SaleItemDto> Items => _items;

        /// <summary>
        /// Inicializuje novou objednávku s unikátním číslem a jménem obsluhy
        /// </summary>
        public void Initialize(int receiptId, string cashierName)
        {
            _items.Clear();
            ReceiptId = receiptId;
            CreatedAt = DateTime.Now;
            CashierName = cashierName;
            IsTakeAway = false;
            AmountTendered = 0;
            PaymentMethod = Payments.Cash;
        }

        /// <summary>
        /// Odstraní položku z objednávky podle jejího indexu.
        /// </summary>
        public void RemoveItem(int index)
        {
            if (index >= 0 && index < _items.Count)
            {
                _items.RemoveAt(index);
            }
        }

        public void AddProduct(int productId, int times)
        {
            var product = ProductService.GetProduct(productId);

            var existing = _items.FirstOrDefault(i => i.Name == product.Name && !i.IsMenu && string.IsNullOrEmpty(i.CouponId));
            if (existing != null)
            {
                existing.Count += times;
            }
            else
            {
                _items.Add(new SaleItemDto
                {
                    Name = product.Name,
                    UnitPrice = product.Price,
                    Count = times,
                    IsMenu = false
                });
            }
        }

        public void AddMenu(int menuId, int times)
        {
            var menu = MenuService.GetMenu(menuId);

            var components = new List<string>();
            foreach (var id in menu.ComponentIds)
            {
                string? compName = DatabaseFunctions.GetProductNameById(id);
                if (!string.IsNullOrEmpty(compName)) components.Add(compName);
            }

            _items.Add(new SaleItemDto
            {
                Name = menu.Name,
                UnitPrice = menu.Price,
                Count = times,
                IsMenu = true,
                ComponentNames = components
            });
        }

        /// <summary>
        /// Nastaví manuální procentuální slevu na celou objednávku.
        /// </summary>
        public void ApplyManualDiscount(int percentage)
        {
            if (percentage < 0 || percentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(percentage), "Sleva musí být mezi 0 a 100 %.");
            }
            ManualDiscountPercentage = percentage;
        }

        public void AddCoupon(Coupon coupon)
        {
            foreach (var component in coupon.Items)
            {
                _items.Add(new SaleItemDto
                {
                    Name = component.Name,
                    UnitPrice = 0, // Cena jednotlivých položek v kupónu je schovaná v celkové ceně
                    Count = component.Count,
                    CouponId = coupon.Code
                });
            }

            // Přidáme samotnou hlavičku kupónu, která nese (případně zápornou) cenu slevy
            _items.Add(new SaleItemDto
            {
                Name = $"Kupón: {coupon.Name}",
                UnitPrice = coupon.Price,
                Count = 1,
                CouponId = coupon.Code
            });
        }

        public int GetTotalPrice()
        {
            // Nejdřív spočítáme čistou cenu všech položek a kupónů v košíku
            int basePrice = _items.Sum(item => item.TotalPrice);

            if (ManualDiscountPercentage > 0)
            {
                // Vypočítáme cenu po slevě a zaokrouhlíme
                double discountFactor = (100.0 - ManualDiscountPercentage) / 100.0;
                return (int)Math.Round(basePrice * discountFactor);
            }

            return basePrice;
        }

        /// <summary>
        /// Kompletně vyčistí data před dalším nákupem
        /// </summary>
        public void Clear()
        {
            _items.Clear();
            ReceiptId = 0;
            CashierName = string.Empty;
            IsTakeAway = false;
            AmountTendered = 0;
            ManualDiscountPercentage = 0; // RESET SLEVY
        }

        public void AddNoteToItem(int itemIndex, string note)
        {
            if (itemIndex >= 0 && itemIndex < _items.Count)
            {
                _items[itemIndex].Notes.Add(note);
            }
        }

        public void RemoveNoteFromItem(int itemIndex, string note)
        {
            if (itemIndex >= 0 && itemIndex < _items.Count)
            {
                _items[itemIndex].Notes.Remove(note);
            }
        }
    }
}