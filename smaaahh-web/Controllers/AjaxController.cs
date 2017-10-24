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
                    decimal getPrice100km = await CallApi<decimal>($"api/Params", false);
                    var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
                    double posXStartM = double.Parse(posXStart, numberFormatInfo);

                    double posYStartM = double.Parse(posYStart, numberFormatInfo);

                    double posXEndM = double.Parse(posXEnd, numberFormatInfo);

                    double posYEndM = double.Parse(posYEnd, numberFormatInfo);

                    decimal nbKmM = decimal.Parse(nbKm, numberFormatInfo);

                    decimal price = nbKmM * (getPrice100km/100);

                    Ride ride = new Ride()
                    {
                        RiderId = riderId,
                        DriverId = driverId,
                        PosXStart = posXStartM,
                        PosYStart = posYStartM,
                        PosXEnd = posXEndM,
                        PosYEnd = posYEndM,
                        PlaceNumber = 4,
                        DateCreation = DateTime.Now,
                        DateStart = null,
                        DateEnd = null,
                        Payment = 0,
                        Price = price,
                        PromotionCodeId = null
                    };
                    Ride rideRecup = await CreateAPIItemAsync<Ride>($"api/Rides", ride);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                
            }
            return false;
        }

    }
}