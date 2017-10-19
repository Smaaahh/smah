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

        public string DriverLastName
        {
            get => driver.LastName;
            set
            {
                driver.LastName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DriverLastName"));
            }
        }

        public string DriverFirstName
        {
            get => driver.FirstName;
            set
            {
                driver.FirstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DriverFirstName"));
            }
        }

        public string DriverUserName
        {
            get => driver.UserName;
            set
            {
                driver.UserName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DriverUserName"));
            }
        }

        public string DriverPhone
        {
            get => driver.Phone;
            set
            {
                driver.Phone = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DriverPhone"));
            }
        }

        public DriverState DriverState
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
