using smaaahh_web.Models;
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

        [HttpGet]
        public async Task<string> GetPrice()
        {
            var jsonSerialiser = new JavaScriptSerializer();
            Params param = await CallApi<Params>($"api/Params", false);

            return jsonSerialiser.Serialize(param);
        }

        [HttpPost]
        [Route("ajax/ChooseDriver")]
        public async Task<string> ChooseDriver(int driverId, string posXStart, string posYStart, string posXEnd, string posYEnd, string nbKm)
        {
            int riderId;
            int.TryParse(Session["UserId"].ToString(), out riderId);
            var jsonSerialiser = new JavaScriptSerializer();

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

                    bool reussi = false;
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

                    if (int.Parse(Session["RideRequestId"].ToString()) != 0)
                    {
                        rideRequest.RideRequestID = int.Parse(Session["RideRequestId"].ToString());
                        reussi = await UpdateAPIItemAsync<RideRequest>($"api/RideRequests/{rideRequest.RideRequestID}", rideRequest);
                    }
                    else
                    {
                        RideRequest rideRecup = await CreateAPIItemAsync<RideRequest>($"api/RideRequests", rideRequest);
                        if (rideRecup != null)
                        {
                            reussi = true;
                            Session["RideRequestId"] = rideRecup.RideRequestID;
                        }
                    }

                    return jsonSerialiser.Serialize(reussi);
                }
                catch (Exception e)
                {
                    return jsonSerialiser.Serialize(false);
                }
                
            }
            return jsonSerialiser.Serialize(false);
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