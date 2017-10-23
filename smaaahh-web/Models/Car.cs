using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_web
{
    public class Car
    {
        public int CarId { get ; set ; }
        public int PlaceNumber { get; set; }
        public string Model { get; set; }

        public string CarPlate { get; set; }

        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }

        public Car()
        { }

        public Car(string Modele_voiture, int Nbplace_voiture,string Immatriculation, int driverId)
        {
            PlaceNumber = Nbplace_voiture;
            Model = Modele_voiture;
            CarPlate = Immatriculation;
            DriverId = driverId;
        }

    }
}
