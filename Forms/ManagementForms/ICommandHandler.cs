using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Pokladna
{
    internal interface ICommandHandler
    {
        Dictionary<RoutedUICommand, (Action execute, Func<bool> CanExecute)> GetCommands();
    }
}
