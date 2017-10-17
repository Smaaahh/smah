using smaaahh_dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;


namespace smaaahh_api
{
    public class MemberShipProvider
    {
        private Db db = new Db();
        public List<Claim> GetUserClaims(string username)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(ClaimTypes.Email, "ihab@utopios.net"));
            return claims;
        }

        public bool VerifyPassword(string email, string password, string type)
        {
            //if (email == "admin" && password == "password")
            //   return true;
            //return false;
            try
            {
                switch (type)
                {
                    case "admin":
                        db.Admins.First(a => (a.Email == email && a.Password == password));
                        break;
                    case "driver":
                        db.Drivers.First(a => (a.Email == email && a.Password == password));
                        break;
                    case "rider":
                        db.Riders.First(a => (a.Email == email && a.Password == password));
                        break;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public MemberShipProvider()
        {

        }
    }
}