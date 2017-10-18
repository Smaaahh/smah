using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_wpf.Modeles
{
    public class RideRequest
    {
        public int RideRequestID { get; set; }

        public virtual Rider Rider { get; set; }
        public int RiderId { get; set; }

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
