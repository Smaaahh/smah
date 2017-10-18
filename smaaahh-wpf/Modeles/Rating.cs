using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_wpf.Modeles
{
    public class Rating
    {
        public int RatingId { get; set; }
        public string Message { get; set; }
        public int Note { get; set; }
        public DateTime DateCreation { get; set; }
        public bool Enabled { get; set; }

        public int RideId { get; set; }
        public virtual Ride Ride { get; set; }

        public int? RiderId { get; set; }
        public int? DriverId { get; set; }

        public virtual Rider Rider { get; set; }

        public virtual Driver Driver { get; set; }

        public Rating()
        {

        }
    }
}
