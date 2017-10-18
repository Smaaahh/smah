using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_wpf.Classes
{
    public abstract class AdminFunctions : Functions
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

        public async static Task<string> GetToken(string email, string password, string type)
        {
            return await CallApi<string>($"api/Account/Authenticate?email={email}&password={password}&type={type}");

        }

        
    }
}
