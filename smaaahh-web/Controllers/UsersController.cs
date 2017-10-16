using smaaahh_dao;
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
    public class UsersController : Controller
    {
        public string Index(int? id)
        {
            string token = null;
            Task.Run(async () =>
            {
                token = await GetToken("Email", "Password");
            }).Wait();
            Session["token"] = token;
            return $"token : {token}";
        }


        public string Index2(int? id)
        {
            string retour = null;
            Task.Run(async () =>
            {
                retour = await GetInfo();
            }).Wait();

            return $"retour : {retour}";
        }


        public async Task<Driver> GetDriver(string Email)
        {
            HttpClient user = new HttpClient();
            user.BaseAddress = new Uri("http://localhost:5050/");
            user.DefaultRequestHeaders.Accept.Clear();
            Driver e = null;
            user.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await user.GetAsync($"api/Drivers/{Email}");
            if (response.IsSuccessStatusCode)
            {
                e = await response.Content.ReadAsAsync<Driver>();
            }
            return e;
        }

        public async Task<Rider> GetRider(string Email)
        {
            HttpClient user = new HttpClient();
            user.BaseAddress = new Uri("http://localhost:5050/");
            user.DefaultRequestHeaders.Accept.Clear();
            Rider e = null;
            user.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await user.GetAsync($"api/Drivers/{Email}");
            if (response.IsSuccessStatusCode)
            {
                e = await response.Content.ReadAsAsync<Rider>();
            }
            return e;
        }
        public async Task<string> GetToken(string Email, string Password)
        {
            HttpClient user = new HttpClient();
            user.BaseAddress = new Uri("http://localhost:5050/");
            user.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            user.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await user.GetAsync($"api/Account/Authenticate?mail={Email}&password={Password}");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsAsync<string>();
            }
            return s;
        }

        public async Task<string> GetInfo()
        {
            HttpClient user = new HttpClient();
            user.BaseAddress = new Uri("http://localhost:5050/");
            user.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            user.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            user.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());
            HttpResponseMessage response = await user.GetAsync($"api/Account/Get?id=1");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsAsync<string>();
            }
            return s;
        }
    }
}
