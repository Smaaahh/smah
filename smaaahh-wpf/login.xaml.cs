using smaaahh_dao;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

//using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http;


namespace smaaahh_wpf
{
    /// <summary>
    /// Logique d'interaction pour login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }

        // click sur le bouton valider
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // récupère le login et password
            //Admin admin = verifLogin(); 
            string email = tEmail.Text;
            string password = tPassword.Text;
            MessageBox.Show($"Email : {email} Password : {password}");
            string token = null;
            Task.Run(async () =>
            {
                token = await GetToken(email, password);
            }).Wait();
            //Session["token"] = token;
            MessageBox.Show( $"token : {token}");
            if (token == "Wrong access")
            {
                // email / password invalide
                lMessage.Content = "Email / password invalide.";
            }
            else if (token == "RSA key error")
            {
                // email / password invalide
                lMessage.Content = "Probleme de cryptage.";
            }
            else
            {
                // si l'administrateur est bien identifié
                // redirection vers l'écran principal
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

       

        public async Task<string> GetToken(string email, string password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51453/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/Account/Authenticate?email={email}&password={password}&type=admin");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsAsync<string>();
            }
            return s;
        }
    }
}
