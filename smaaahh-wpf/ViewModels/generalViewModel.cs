using smaaahh_wpf.Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace smaaahh_wpf.ViewModels
{
    class generalViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region driver
        //--------------------------------------------------
        private Driver driver;

        public ICommand MaCommande { get; }

        public Driver Driver
        {
            get => driver;
            set
            {
                driver = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Driver"));
            }
        }

        public string LastName
        {
            get => driver.LastName;
            set
            {
                driver.LastName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LastName"));
            }
        }

        public string FirstName
        {
            get => driver.FirstName;
            set
            {
                driver.FirstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FirstName"));
            }
        }

        public string UserName
        {
            get => driver.UserName;
            set
            {
                driver.UserName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
            }
        }
    
        public DriverState State
        {
            get => driver.State;
            set
            {
                driver.State = value;
                PropertyChanged(this, new PropertyChangedEventArgs("State"));
            }
        }
    }

    
    #endregion
}
