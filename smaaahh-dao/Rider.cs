using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_dao
{
    public class Rider : User
    {
        public enum RiderState
        {
            Enabled,
            Disabled
        }

        [Key]
        public int RiderId { get; set; }
        public double Rating { get; set; }
        
        public decimal PosX { get; set; }
        public decimal PosY { get; set; }
      
        public RiderState State { get; set; }

        public Rider()
        {

        }


    }
}
