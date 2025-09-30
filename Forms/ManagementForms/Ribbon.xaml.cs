using System;
using System.Reflection;
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
            BindCommands();
        }

        public event EventHandler<RoutedUICommand>? CommandInvoked;

        private void OnRibbonClick(object sender, RoutedEventArgs e)
        {
            if(sender is RibbonButton btn && btn.Command is RoutedUICommand cmd)
            {
                CommandInvoked?.Invoke(this, cmd);
            }
        }

        private void BindCommands()
        {
//            var commandType = typeof(RibbonCommands);
//            foreach(var field in commandType.GetFields(BindingFlags.Public | BindingFlags.Static))
//            {
//                if(field.GetValue(null) is RoutedUICommand cmd)
//                {
                    this.CommandBindings.Add(new CommandBinding(
                        RibbonCommands.Coupons,
                        (s, e) => e.Handled = true,
                        (s, e) => { e.CanExecute = true; e.Handled = true; }
                    ));
//                }
//            }
        }
    }
}
