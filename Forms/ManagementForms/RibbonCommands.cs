namespace Pokladna
{
    public static class RibbonCommands
    {
        //Hlavn�
        public static readonly System.Windows.Input.RoutedUICommand PriceList = new System.Windows.Input.RoutedUICommand("Cen�k", "PriceList", typeof(RibbonCommands));
        public static readonly System.Windows.Input.RoutedUICommand Employees = new System.Windows.Input.RoutedUICommand("Zam�stnanci", "Employees", typeof(RibbonCommands));
        public static readonly System.Windows.Input.RoutedUICommand GiftCards = new System.Windows.Input.RoutedUICommand("D�rkov� karty", "GiftCards", typeof(RibbonCommands));
        public static readonly System.Windows.Input.RoutedUICommand Coupons = new System.Windows.Input.RoutedUICommand("Kupony", "Coupons", typeof(RibbonCommands));
        public static readonly System.Windows.Input.RoutedUICommand Transactions = new System.Windows.Input.RoutedUICommand("Transakce", "Transactions", typeof(RibbonCommands));
        public static readonly System.Windows.Input.RoutedUICommand Sales = new System.Windows.Input.RoutedUICommand("Prodeje polo�ek", "Sales", typeof(RibbonCommands));
        //Editace
        public static readonly System.Windows.Input.RoutedUICommand Add = new System.Windows.Input.RoutedUICommand("P�idat", "Add", typeof(RibbonCommands));
        public static readonly System.Windows.Input.RoutedUICommand Edit = new System.Windows.Input.RoutedUICommand("Upravit", "Edit", typeof(RibbonCommands));
        public static readonly System.Windows.Input.RoutedUICommand Delete = new System.Windows.Input.RoutedUICommand("Odstranit", "Delete", typeof(RibbonCommands));
        //Cen�k
        public static readonly System.Windows.Input.RoutedUICommand BulkPriceChange = new System.Windows.Input.RoutedUICommand("P�ecen�n� polo�ek", "BulkPriceChange", typeof(RibbonCommands));
        //Kupony + karty
        public static readonly System.Windows.Input.RoutedUICommand ClearExpired = new System.Windows.Input.RoutedUICommand("Vymazat neplatn�", "ClearExpired", typeof(RibbonCommands));
        //Export
        public static readonly System.Windows.Input.RoutedUICommand PDFExport = new System.Windows.Input.RoutedUICommand("PDF", "PDF_Export", typeof(RibbonCommands));
        public static readonly System.Windows.Input.RoutedUICommand ExcelExport = new System.Windows.Input.RoutedUICommand("Excel", "Excel_Export", typeof(RibbonCommands));
        public static readonly System.Windows.Input.RoutedUICommand CSVExport = new System.Windows.Input.RoutedUICommand("CSV", "CSV_Export", typeof(RibbonCommands));
        //V�pisy
        public static readonly System.Windows.Input.RoutedUICommand Filter = new System.Windows.Input.RoutedUICommand("Filtr", "Filter", typeof(RibbonCommands));
        //Kosnec
        public static readonly System.Windows.Input.RoutedUICommand Exit = new System.Windows.Input.RoutedUICommand("Konec", "Exit", typeof(RibbonCommands));
    }
}