using Newtonsoft.Json;
using smaaahh_wpf.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace smaaahh_wpf.Classes
{
    public abstract class DriverFunctions : Functions
    {

        public static List<Driver> GetDrivers ()
        {
            List<Driver> listDrivers = new List<Driver>() ;

            Task.Run(async () =>
            {
                listDrivers = await GetDriversAPI();
            }).Wait();

            return listDrivers;
        }

        public async static Task<List<Driver>> GetDriversAPI()
        {
            return await CallApi<List<Driver>>($"api/Drivers");

        }

        public static void updateUser(Object o, string type)
        {
            string result;
            Task.Run(async () =>
            {
                result = await updateUserAPI(o, type);
            }).Wait();
        }

        public async static Task<string> updateUserAPI(Object o, string type)
        {
            switch(type)
            {
                case "driver":
                    return await CallApi<string>($"api/Driver/");
                    break;
                case "rider":
                    return await CallApi<string>($"api/Rider/");
                    break;
            }
            return null;
        }
    }
}
