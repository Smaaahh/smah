using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using smaaahh_dao;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace smaaahh_api.Controllers
{
    public class AccountController : ApiController
    {
        private AuthService _authService;
        private Db db = new Db();

        
        public AccountController()
        {

        }

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        // GET: Membership
        [Route("api/Account/Authenticate")]
        public async Task<string> GetAuthenticate(String email, String password, String type)
        {
            MemberShipProvider m = new MemberShipProvider();
            RSAKeyProvider r = new RSAKeyProvider();
            _authService = new AuthService(m, r);
            string Token = await _authService.GenerateJwtTokenAsync(email, password, type);
            return Token;
        }

        [Route("api/Account/Get")]
        [TokenAuthenticate]
        public string Get(string email, String type)
        {
            User user = null;
            try
            {
                switch (type)
                {
                    case "driver":
                        user = db.Drivers.First(t => t.Email == email);
                        break;
                    case "rider":
                        user = db.Riders.First(t => t.Email == email);
                        break;
                }
            }
            catch(Exception e)
            {

            }
            var jsonSerialiser = new JavaScriptSerializer();

            return jsonSerialiser.Serialize(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id, string type)
        {
            if (type == "rider")
                return db.Riders.Count(e => e.RiderId == id) > 0;
            else if (type == "driver")
                return db.Riders.Count(e => e.RiderId == id) > 0;

            return false;
        }

        
    }
}