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

            //listDrivers = JsonConvert.DeserializeObject<List<Driver>>(result.ToString());
            return listDrivers;
        }

        public async static Task<List<Driver>> GetDriversAPI()
        {
            return await CallApi<List<Driver>>($"api/Drivers");

        }

        public static void SaveDriver(Object o)
        { }
    }
}
