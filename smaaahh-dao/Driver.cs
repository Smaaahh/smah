using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_dao
{
   public class Driver : User
    {
        [Key]
        public int DriverId { get; set; }
        public double Rating { get; set; }
        public decimal PosX { get; set; }
        public decimal PosY { get; set; }

        public int CartId { get; set; }

        [ForeignKey("CartId")]
        public virtual Car Car { get; set; }

        public Driver():base()
        {
        }


    }
}
