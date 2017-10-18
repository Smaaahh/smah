using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_wpf.Modeles
{
    public class Car
    {
        public int CarId { get; set; }
        public int PlaceNumber { get; set; }
        public string Model { get; set; }

        public string CarPlate { get; set; }

        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }

        public Car()
        { }

    }
}
