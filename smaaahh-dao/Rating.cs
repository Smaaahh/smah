using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_dao
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        public string Message { get; set; }
        public int Note { get; set; }
        public DateTime DateCreation { get; set; }
        public bool Enabled { get; set; }

        public int? RiderId { get; set; }
        public int? DriverId { get; set; }
        
        [ForeignKey("RiderId")]
        public virtual Rider Rider { get; set; }

        [ForeignKey("DriverId")]
        public virtual Driver Driver { get; set; }

        public Rating()
        {

        }
    }
}
