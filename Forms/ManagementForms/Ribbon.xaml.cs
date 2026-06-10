using Pokladna.Events;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;

namespace Pokladna
{
    /// <summary>
    /// Interakční logika pro Ribbon.xaml
    /// </summary>
    public partial class Ribbon : UserControl
    {
        public Ribbon()
        {
            InitializeComponent();
        }

        private void OnExecuteCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command is RoutedCommand routedCommand)
            {
                AppActionType? action = MapCommandToAction(routedCommand.Name);
                if (action.HasValue)
                {
                    AppEventBroker.TriggerAction(action.Value, routedCommand.Name);
                }
            }
        }

        public void UpdateRibbonState(string openFormName)
        {
            // Nejdřív schováme úplně všechny specifické taby a ukážeme domovský
            TabHome.Visibility = Visibility.Collapsed;
            TabGiftCards.Visibility = Visibility.Collapsed;
            // Sem doplň schování ostatních tabů (TabEmployees.Visibility = Visibility.Collapsed atd.)

            TabHome.Visibility = Visibility.Visible;
            MainRibbon.SelectedItem = TabHome; // Výchozí skok na Domů

            // Pokud se otevřelo konkrétní okno, upravíme viditelnost
            if (!string.IsNullOrEmpty(openFormName))
            {
                // Schováme hlavní domovskou záložku
                TabHome.Visibility = Visibility.Collapsed;

                // Ukážeme tu, která patří otevřenému oknu a aktivujeme ji
                switch (openFormName)
                {
                    case "PriceListForm":
                        TabPriceList.Visibility = Visibility.Visible;
                        MainRibbon.SelectedItem = TabPriceList;
                        break;

                    case "GiftCardsForm":
                        TabGiftCards.Visibility = Visibility.Visible;
                        MainRibbon.SelectedItem = TabGiftCards;
                        break;

                        // Sem doplníš mapování pro další formuláře...
                }
            }
        }

        private AppActionType? MapCommandToAction(string commandName)
        {
            return commandName switch
            {
                "PriceList" => AppActionType.OpenPriceList,
                "Employees" => AppActionType.OpenEployeesList,
                "Transactions" => AppActionType.OpenTransactions,
                "Add" => AppActionType.AddRecord,
                "Edit" => AppActionType.EditRecord,
                "Delete" => AppActionType.RemoveRecord,
                "Exit" => AppActionType.CloseWindow,
                _ => null
            };
        }
    }
}
