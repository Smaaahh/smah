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
        
        public Rider Rider { get; set; }

        public decimal PosXStart { get; set; }
        public decimal PosYStart { get; set; }
        public decimal PosXEnd { get; set; }
        public decimal PosYEnd { get; set; }

        public int PlaceNumber { get; set; }
        public DateTime DateCreation { get; set; }

        public RideRequest()
        { }
    }
}
