using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace smaaahh_web.Controllers
{
    public class AjaxController : DefaultController
    {
        // GET: Ajax
        [HttpGet]
        public async Task<string> GetDriversFree()
        {
            var jsonSerialiser = new JavaScriptSerializer();
            List<Driver> listeDriver = await CallApi<List<Driver>>($"api/Drivers/Free", false);
            
            return jsonSerialiser.Serialize(listeDriver);
        }

        [HttpPost]
        [Route("ajax/ChooseDriver")]
        public async Task<bool> ChooseDriver(int driverId, string posXStart, string posYStart, string posXEnd, string posYEnd, string nbKm)
        {
            int riderId;
            int.TryParse(Session["UserID"].ToString(), out riderId);

            if (riderId != 0)
            {
                try
                {
                    var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
                    double posXStartM = double.Parse(posXStart, numberFormatInfo);

                    double posYStartM = double.Parse(posYStart, numberFormatInfo);

                    double posXEndM = double.Parse(posXEnd, numberFormatInfo);

                    double posYEndM = double.Parse(posYEnd, numberFormatInfo);

                    decimal nbKmM = decimal.Parse(nbKm, numberFormatInfo);
                    
                    RideRequest rideRequest = new RideRequest()
                    {
                        RiderId = riderId,
                        DriverId = driverId,
                        PosXStart = posXStartM,
                        PosYStart = posYStartM,
                        PosXEnd = posXEndM,
                        PosYEnd = posYEndM,
                        PlaceNumber = 4,
                        DateCreation = DateTime.Now,
                        nbKm = nbKmM
                    };
                    RideRequest rideRecup = await CreateAPIItemAsync<RideRequest>($"api/RideRequest", rideRequest);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                
            }
            return false;
        }


        // POST: Ajax
        [HttpPost]
        [Route("ajax/UpdateDriverPosition")]
        public async Task<bool> UpdateDriverPosition(string latitude, string longitude)
        {
            // on va chercher le Driver
            Driver driver = null;
            // on convertit les coordonnées
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
            double posX = double.Parse(latitude, numberFormatInfo);

            double posY = double.Parse(longitude, numberFormatInfo);
            Task.Run(async () =>
            {
                driver = await GetDriver(Session["UserEmail"].ToString());
            }).Wait();
            // on met à jour les infos latitude et longitude
            driver.PosX = posX;
            driver.PosY = posY;
            return await UpdateDriver(driver); 
        }
    }
}