using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace smaaahh_wpf.Classes
{
    class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action actionAExecuter;

        public RelayCommand(Action act)
        {
            actionAExecuter = act;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            actionAExecuter();
        }
    }
}
