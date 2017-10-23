using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using smaaahh_wpf.Classes;
using smaaahh_wpf.Modeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace smaaahh_wpf.ViewModels
{
    public class generalViewModel : ViewModelBase
    {
        #region constructeur
        public generalViewModel()
        {
            mesDrivers = new ObservableCollection<Driver>(AdminFunctions.GetDrivers());
            mesRiders = new ObservableCollection<Rider>(AdminFunctions.GetRiders());
            //_updateDriverCommand = new RelayCommand<Driver>(ExecuteDoSomethingWithDriverItem);

            //this.ExpanderCommand = new RelayCommand(this.ExecuteExpanderCommand);
        }


        #endregion
        public ICommand MaCommande { get; }
        //--------------------------------------------------
        #region driver

        public ObservableCollection<Driver> MesDrivers
        {
            get => mesDrivers;
            set
            {
                mesDrivers = value;
                RaisePropertyChanged("MesDrivers");
            }
        }
        private ObservableCollection<Driver> mesDrivers;

        public ICommand UpdateDriverCommand
        {
            get
            {
                //return _updateDriverCommand;
                return _updateDriverCommand ??
                       (_updateDriverCommand = new RelayCommand<Driver>(ExecuteDoSomethingWithDriverItem));
            }

        }

        private RelayCommand<Driver> _updateDriverCommand;

        private void ExecuteDoSomethingWithDriverItem(Driver item)
        {
            // Do something
            AdminFunctions.updateUser<Driver>(item, "driver", item.DriverId);
        }

        // This property will be the command binding target
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand ExpanderCommand { get; set; }

        // this is the handler method
        public void ExecuteExpanderCommand(object parameter)
        {
            Driver item = (Driver)parameter;
            //do your stuff here
            AdminFunctions.updateUser<Driver>(item, "driver", item.DriverId);
        }


        private Driver driver;

        public Driver Driver
        {
            get => driver;
            set
            {
                driver = value;
                RaisePropertyChanged("Driver");
            }
        }


        #endregion
        //--------------------------------------------------
        #region rider

        public ObservableCollection<Rider> MesRiders
        {
            get => mesRiders;
            set
            {
                mesRiders = value;
                RaisePropertyChanged("MesRiders");
            }
        }
        private ObservableCollection<Rider> mesRiders;

        private Rider rider;

        public Rider Rider
        {
            get => rider;
            set
            {
                rider = value;
                RaisePropertyChanged("Rider");
            }
        }

        
        #endregion
    }
}
