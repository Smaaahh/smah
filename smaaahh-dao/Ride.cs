using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_dao
{
    public class Ride
    { 
        [Key]
        public int RideId { get; set; }

        [ForeignKey("RiderId")]
        public virtual Rider Rider { get; set; }
        public int RiderId { get; set; }

        [ForeignKey("DriverId")]
        public virtual Driver Driver { get; set; }
        public int DriverId { get; set; }

        public decimal PosXStart { get; set; }
        public decimal PosYStart { get; set; }
        public decimal PosXEnd { get; set; }
        public decimal PosYEnd { get; set; }

        public int PlaceNumber { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public PaymentType Payment { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("PromotionCodeId")]
        public virtual PromotionCode PromotionCode { get; set; }
        public int PromotionCodeId { get; set; }

        public double RiderRating { get; set; }
        public double DriverRating { get; set; }

        public Ride()
        { }

        public enum PaymentType
        {
            Paypal,
            BankCard
        }

    }
}
