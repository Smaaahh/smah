using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_web
{
    public class RideRequest
    {
        public int RideRequestID { get; set; }
        
        public int RiderId { get; set; }
        public Rider Rider { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public double PosXStart { get; set; }
        public double PosYStart { get; set; }
        public double PosXEnd { get; set; }
        public double PosYEnd { get; set; }

        public decimal nbKm { get; set; }

        public int PlaceNumber { get; set; }
        public DateTime DateCreation { get; set; }

        public RideRequest()
        { }
    }
}
