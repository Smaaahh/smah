using smaaahh_wpf.Classes;
using smaaahh_wpf.Modeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace smaaahh_wpf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Driver> listDriver = new List<Driver>();
        public MainWindow()
        {
            InitializeComponent();

            // initialiser la liste des drivers
            listDriver = DriverFunctions.GetDrivers();
            listDrivers.ItemsSource = listDriver;
            listDrivers.DataContext = listDriver;

            //GetData() creates a collection of Customer data from a database
            //ObservableCollection<Customer> custdata = GetData();

            ////Bind the DataGrid to the customer data
            //DG1.DataContext = custdata;
        }

        private void saveDriver(object sender, DataGridRowEditEndingEventArgs e)
        {
            DriverFunctions.updateUser(e.Row.Item, "driver");
        }

        private void saveRider(object sender, DataGridRowEditEndingEventArgs e)
        {
            DriverFunctions.updateUser(e.Row.Item, "rider");
        }


    }
}
