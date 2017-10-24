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

        public async static Task<T> CallApi<T>(string url)
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

        public static async Task<T> CreateAPIItemAsync<T>(string url, T item)
        {
            HttpClient user = new HttpClient();
            user.BaseAddress = new Uri(ApiUrl);
            user.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = await user.PostAsJsonAsync(url, item);
            T s = default(T);
            // response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsAsync<T>();
            }
            // Return the URI of the created resource.
            //return response.Headers.Location.ToString();
            return s;
        }

        public static async Task<bool> UpdateAPIItemAsync<T>(string url, T item)
        {
            HttpClient user = new HttpClient();
            user.BaseAddress = new Uri(ApiUrl);
            user.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = await user.PutAsJsonAsync(url, item);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            //item = await response.Content.ReadAsAsync<T>();
            return true;
        }

    }
}
