﻿using smaaahh_web.Models;
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

        //*************************************
        #region fonctions génériques

        public async Task<T> CallApi<T>(string url, bool needAuth)
        {
            HttpClient user = new HttpClient();
            user.BaseAddress = new Uri(UrlApi);
            user.DefaultRequestHeaders.Accept.Clear();
            T s = default(T);
            user.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            if (needAuth)
            {
                user.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());
            }

            HttpResponseMessage response = await user.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsAsync<T>();
            }
            return s;
        }

        public async Task<T> CreateItem<T>(T item, string type)
        {
            string url = "";
            switch (type)
            {
                case "car":
                    url = $"api/Cars";
                    break;
                case "driver":
                    url = $"api/Drivers";
                    break;
                case "rider":
                    url = $"api/Riders";
                    break;
                case "ride":
                    url = $"api/Rides";
                    break;
                case "rating":
                    url = $"api/Ratings";
                    break;
            }
            return await CreateAPIItemAsync<T>(url, item);
        }

        public async Task<T> CreateAPIItemAsync<T>(string url, T item)
        {
            HttpClient user = new HttpClient();
            user.BaseAddress = new Uri(UrlApi);
            user.DefaultRequestHeaders.Accept.Clear();
            user.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await user.PostAsJsonAsync<T>(url, item);
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

        public async Task<bool> UpdateAPIItemAsync<T>(string url, T item)
        {
            HttpClient user = new HttpClient();
            user.BaseAddress = new Uri(UrlApi);
            user.DefaultRequestHeaders.Accept.Clear();
            user.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await user.PutAsJsonAsync(url, item);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            //item = await response.Content.ReadAsAsync<T>();
            return true;
        }

        public async Task<string> GetToken(string Email, string Password, string Type)
        {
            return await CallApi<string>($"api/Account/Authenticate?email={Email}&password={Password}&type={Type}", false);
        }

        public async Task<string> GetInfo(int? id)
        {
            return await CallApi<string>($"api/Account/Get?id=" + id, true);
        }
        #endregion



        //*************************************
        #region Rider
        public async Task<Rider> GetRider(string email)
        {
           
            return await CallApi<Rider>($"api/Account/?email={email}&type=rider", true);
            
        }
        public async Task<bool> UpdateRider(Rider rider)
        {
            return await UpdateAPIItemAsync<Rider>($"api/Riders/?id={rider.UserId}", rider);
        }
        #endregion

        //*************************************
        #region Driver
        public async Task<Driver> GetDriver(string email)
        {
            return await CallApi<Driver>($"api/Account/?email={email}&type=driver", true);
        }
        public async Task<bool> UpdateDriver(Driver driver)
        {
            return await UpdateAPIItemAsync<Driver>($"api/Drivers/?id={driver.UserId}", driver);
        }
        #endregion


        //*************************************
        #region Car
        public async Task<Car> GetCar(string UserId)
        {
            return await CallApi<Car>($"api/UserCars/{UserId}", true);
        }

        public async Task<bool> UpdateCar(Car car)
        {
            return await UpdateAPIItemAsync<Car>($"api/Cars/?id={car.CarId}", car);
        }
        #endregion




        //*************************************
        #region RideRequest et Ride
        public async Task<RideRequest> GetRideRequestByRider(int RiderId)
        {
            return await CallApi<RideRequest>($"api/RideRequest/RideRequestByRider/?id={RiderId}", true);
        }

        public async Task<RideRequest> GetRideRequest(int RideRequestId)
        {
            return await CallApi<RideRequest>($"api/RideRequests/?id={RideRequestId}", true);
        }

        
        #endregion

        //*************************************
        #region Params
        public async Task<Params> GetParams()
        {
            return await CallApi<Params>($"api/Params", true);
        }
        #endregion
    }
}