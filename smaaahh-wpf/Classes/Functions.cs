using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;


namespace smaaahh_wpf
{
    public  class Functions
    {
        protected static string ApiUrl = "http://localhost:51453/";

        public async static Task<T> CallApi<T>(string url, bool needAuth)
        {
            HttpClient user = new HttpClient();
            user.BaseAddress = new Uri(ApiUrl);
            user.DefaultRequestHeaders.Accept.Clear();
            T s = default(T);
            user.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage response = await user.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsAsync<T>();
            }
            return s;
        }

    }
}
