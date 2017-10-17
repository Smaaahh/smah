using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;


namespace smaaahh_api
{
    public class MemberShipProvider
    {
        public List<Claim> GetUserClaims(string username)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(ClaimTypes.Email, "ihab@utopios.net"));
            return claims;
        }

        public bool VerifyUserPassword(string username, string password)
        {
            if (username == "admin" && password == "password")
                return true;
            return false;
        }

        public MemberShipProvider()
        {

        }
    }
}