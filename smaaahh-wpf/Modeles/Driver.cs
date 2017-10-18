using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_wpf.Modeles
{
    public class Driver : User
    {
        

        public int DriverId { get; set; }
        public double Rating { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public bool Active { get; set; }
        public bool Free { get; set; }

        public DriverState State { get; set; }

        public Driver()
        {
        }


    }

    public enum DriverState
    {
        Enabled,
        Disabled
    }
}
