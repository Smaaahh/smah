using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}