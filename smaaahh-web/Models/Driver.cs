using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_web
{
   public class Driver : User
    {
        public enum DriverState
        {
            Enabled,
            Disabled
        }
        
        public double Rating { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public bool Active { get; set; }
        public bool Free { get; set; }

        public DriverState State { get; set; }

        public Driver()
        {
        }

        public Driver(string Nom, string Prenom, string Pseudo, string Email, string Password, string NTelephone, string Image):base( Nom,  Prenom,  Pseudo,  Email,  Password,  NTelephone,  Image)
        {
            Rating = 0;
            PosX = 0;
            PosY = 0;
            Active = false;
            Free = false;
            State = DriverState.Disabled;
        }



    }
}
