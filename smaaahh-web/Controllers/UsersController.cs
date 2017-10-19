using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace smaaahh_web.Controllers
{
    public class UsersController : DefaultController
    {
        public string Index(int? id)
        {
            string token = null;
            Task.Run(async () =>
            {
                token = await GetToken("Email", "Password", "driver");
            }).Wait();
            Session["token"] = token;
            return $"token : {token}";
        }


        public string Index2(int? id)
        {
            string retour = null;
            Task.Run(async () =>
            {
                retour = await GetInfo(id);
            }).Wait();

            return $"retour : {retour}";
        }


        public async Task<Driver> GetDriver(string Email)
        {
            return await CallApi<Driver>($"api/Drivers/{Email}", false);
        }

        public async Task<Rider> GetRider(string Email)
        {
            return await CallApi<Rider>($"api/Riders/{Email}",false);
        }
        public async Task<string> GetToken(string Email, string Password, string Type)
        {
            return await CallApi<string>($"api/Account/Authenticate?mail={Email}&password={Password}&type={Type}", false);
        }

        public async Task<string> GetInfo(int? id)
        {
            return await CallApi<string>($"api/Account/Get?id=" + id, true);
        }
    }
}
