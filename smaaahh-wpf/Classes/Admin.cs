using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_wpf.Classes
{
    public abstract class Admin : Functions
    {
        public static string verifLogin (string email, string password, string type)
        {
            string token = null;
            Task.Run(async () =>
            {
                token = await GetToken(email, password, type);
            }).Wait();
            //Session["token"] = token;
            //MessageBox.Show($"token : {token}");
            return token;
            
        }

        public async static Task<string> GetToken2(string email, string password, string type)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51453/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/Account/Authenticate?email={email}&password={password}&type={type}");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsAsync<string>();
            }
            return s;

            
        }

        public async static Task<string> GetToken(string email, string password, string type)
        {
            return await CallApi<string>($"api/Account/Authenticate?email={email}&password={password}&type={type}", false);

        }
    }
}
