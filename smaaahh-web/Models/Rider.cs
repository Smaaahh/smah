using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_web
{
    public class Rider : User
    {
        public enum RiderState
        {
            Enabled,
            Disabled
        }
        
        public double Rating { get; set; }
        
        public double PosX { get; set; }
        public double PosY { get; set; }
      
        public RiderState State { get; set; }

        public Rider()
        {

        }
        
        public Rider(string Nom, string Prenom, string Pseudo, string Email, string Password, string NTelephone, string Image) : base(Nom, Prenom, Pseudo, Email, Password, NTelephone, Image)
        {
            Rating = 0;
            PosX = 0;
            PosY = 0;
        }

    }
}
