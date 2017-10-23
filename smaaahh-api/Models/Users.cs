using smaaahh_dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smaaahh_api.Models
{
    public class Users
    {
        private static Db db = new Db();
        public static bool verifEmail(string email)
        {
            bool Error = false;
            try
            {
                Driver d = db.Drivers.First(t => t.Email == email);
                Error = true;
            }
            catch (Exception e)
            { }

            try
            {
                Rider r = db.Riders.First(t => t.Email == email);
                Error = true;
            }
            catch (Exception)
            {
            }

            try
            {
                Admin a = db.Admins.First(t => t.Email == email);
                Error = true;
            }
            catch (Exception e)
            { }
            return Error;
        }
    }
}