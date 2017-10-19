using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_web
{
    public class Rating
    {
        public int RatingId { get; set; }
        public string Message { get; set; }
        public int Note { get; set; }
        public DateTime DateCreation { get; set; }
        public bool Enabled { get; set; }
        
        public Ride Ride { get; set; }
        
        public Rider Rider { get; set; }
        
        public Driver Driver { get; set; }

        public Rating()
        {

        }
    }
}
