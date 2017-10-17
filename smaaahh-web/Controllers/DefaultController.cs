using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace smaaahh_web.Controllers
{
    public class DefaultController : Controller
    {
        protected string UrlApi = "http://localhost:51453/";

        public async Task<T> CallApi<T>(string url, bool needAuth)
        {
            HttpClient user = new HttpClient();
            user.BaseAddress = new Uri(UrlApi);
            user.DefaultRequestHeaders.Accept.Clear();
            T s = default(T);
            user.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            if (needAuth)
                user.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());

            HttpResponseMessage response = await user.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsAsync<T>();
            }
            return s;
        }
    }
}