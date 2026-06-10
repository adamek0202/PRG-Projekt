using System;

namespace Pokladna.Events
{
    internal static class AppEventBroker
    {
        /// <summary>
        /// Událost, kterou poslouchá MainForm a reaguje na ni.
        /// </summary>
        public static event EventHandler<AppActionEventArgs> ActionTriggered;

        /// <summary>
        /// Metoda, kterou zavolá WPF Ribbon v momentě, kdy uživatel klikne na jakékoliv tlačítko.
        /// </summary>
        /// <param name="actionType">Jaká akce se má provést (Enum).</param>
        /// <param name="payload">Volitelná data (např. ID vybraného řádku, název příkazu).</param>
        public static void TriggerAction(AppActionType actionType, object payload = null)
        {
            // Vyvoláme událost. Otazník hlídá, zda je na druhé straně (ve WinForms) někdo přihlášen,
            // aby kód nespadl na NullReferenceException, pokud by MainForm ještě nebyl načtený.
            ActionTriggered?.Invoke(null, new AppActionEventArgs(actionType, payload));
        }
    }
}
