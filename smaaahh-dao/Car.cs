using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_dao
{
    public class Car
    {
        [Key]
        public int CarId { get ; set ; }
        public int PlaceNumber { get; set; }
        public string Model { get; set; }

        public string CarPlate { get; set; }

        public int DriverId { get; set; }
        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }

        public Car()
        { }

    }
}
