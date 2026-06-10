namespace Pokladna.Forms.ManagementForms
{
    internal interface IRibbonActionTarget
    {
        void ExecuteAdd();
        void ExecuteEdit();
        void ExecuteDelete();
        void ExecuteClearExpired(); // Pro dárkové karty a kupony (Vymazat neplatné)
        void ExecuteClearAll();    // Pro Výpisy (Vymazat)

        // Sekce: Nástroje
        void ExecuteSort();
        void ExecuteFilter();

        // Sekce: Export
        void ExecuteExportPdf();
        void ExecuteExportExcel();
        void ExecuteExportCsv();
    }
}
