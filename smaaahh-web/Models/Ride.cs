using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_web
{
    public class Ride
    { 
        public int RideId { get; set; }

        public int RiderId { get; set; }
        public Rider Rider { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public double PosXStart { get; set; }
        public double PosYStart { get; set; }
        public double PosXEnd { get; set; }
        public double PosYEnd { get; set; }

        public int PlaceNumber { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public PaymentType? Payment { get; set; }

        public decimal Price { get; set; }
        public decimal nbKm { get; set; }

        public int? PromotionCodeId { get; set; }
        public PromotionCode PromotionCode { get; set; }
        
        public Ride()
        { }

        public Ride(RideRequest rideRequest, decimal prix)
        {
            RiderId = rideRequest.RiderId;
            DriverId = rideRequest.DriverId;
            PosXStart = rideRequest.PosXStart;
            PosYStart = rideRequest.PosYStart;
            PosXEnd = rideRequest.PosXEnd;
            PosYEnd = rideRequest.PosYEnd;

            PlaceNumber = rideRequest.PlaceNumber;
            Price = rideRequest.nbKm * prix;
            nbKm = rideRequest.nbKm;
            DateCreation = DateTime.Now;
        }

        public enum PaymentType
        {
            Paypal,
            BankCard
        }

    }
}
