using System;

namespace Pokladna
{
    internal class PosContext
    {
        // Konstanta pro HW tiskárnu, klidně může žít zde
        public string PrinterName { get; } = "BP-T3";

        // Běžné stavové proměnné pokladny
        public int NextReceiptId { get; set; }
        public string CurrentCashierName { get; set; } = "Administrátor";

        // Režimy pokladny (už ne staticky v MainFormu!)
        public bool IsPriceCheckMode { get; set; } = false;
        public bool IsTakeAway { get; set; } = false;

        // Aktuálně rozpracovaná objednávka (taška, produkty, kupóny)
        public Order CurrentOrder { get; private set; }

        public PosContext()
        {
            CurrentOrder = new Order();
            ResetCurrentOrder();
        }

        /// <summary>
        /// Vyčistí starou objednávku a připraví novou s čistým štítem pro dalšího zákazníka.
        /// </summary>
        public void ResetCurrentOrder()
        {
            CurrentOrder.Clear();
            // Zde můžeme objednávce rovnou vnutit aktuální stav pokladny
            CurrentOrder.ReceiptId = NextReceiptId;
            CurrentOrder.CashierName = CurrentCashierName;
            CurrentOrder.IsTakeAway = IsTakeAway;
        }

        /// <summary>
        /// Zavolá se po úspěšném dokončení platby. Posune číslování účtenek.
        /// </summary>
        public void IncrementReceiptId()
        {
            NextReceiptId++;
            // Zde bys ideálně uložil nové číslo účtenky do DB, aby se při výpadku proudu neztratilo:
            // DatabaseFunctions.SaveCurrentReceiptId(NextReceiptId);
        }
    }
}