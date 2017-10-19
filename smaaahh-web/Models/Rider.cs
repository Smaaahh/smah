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
        
        public int RiderId { get; set; }
        public double Rating { get; set; }
        
        public double PosX { get; set; }
        public double PosY { get; set; }
      
        public RiderState State { get; set; }

        public Rider()
        {

        }


    }
}
