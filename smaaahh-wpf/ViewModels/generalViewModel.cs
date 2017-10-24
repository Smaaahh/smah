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
            // initialiser les données du ViewModel
            mesDrivers = new ObservableCollection<Driver>(AdminFunctions.GetDrivers());
            mesRiders = new ObservableCollection<Rider>(AdminFunctions.GetRiders());
            MaCmdeModifParams = new GalaSoft.MvvmLight.Command.RelayCommand(ModifierParams);


            parametres = AdminFunctions.GetParams();
            parametres = (parametres == null) ? new Params() :parametres;
        }


        #endregion
        
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
            AdminFunctions.updateUser<Driver>(item, "driver", item.
                UserId);
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

        public ICommand UpdateRiderCommand
        {
            get
            {
                //return _updateDriverCommand;
                return _updateRiderCommand ??
                       (_updateRiderCommand = new RelayCommand<Rider>(ExecuteDoSomethingWithRiderItem));
            }

        }

        private RelayCommand<Rider> _updateRiderCommand;

        private void ExecuteDoSomethingWithRiderItem(Rider item)
        {
            // Do something
            AdminFunctions.updateUser<Rider>(item, "rider", item.UserId);
        }


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
        //--------------------------------------------------
        #region parametres
        private Params parametres;

        public Params Parameters
        {
            get => parametres;
            set
            {
                parametres = value;
                RaisePropertyChanged("Parametres");
            }
        }

        public decimal Price
        {
            get => parametres.Price;
            set
            {
                parametres.Price = value;
                RaisePropertyChanged("Price");
            }
        }

        public ICommand MaCmdeModifParams { get; }

        public void ModifierParams()
        {
            //var prix = Parameters.Price;
            // acces à l'API
            parametres = AdminFunctions.UpdateParams(Parameters);
        }
        #endregion
    }
}
