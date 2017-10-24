using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_wpf.Modeles
{
    public class Ride
    {
        public int RideId { get; set; }

        public virtual Rider Rider { get; set; }
        public int RiderId { get; set; }

        public virtual Driver Driver { get; set; }
        public int DriverId { get; set; }

        public double PosXStart { get; set; }
        public double PosYStart { get; set; }
        public double PosXEnd { get; set; }
        public double PosYEnd { get; set; }

        public int PlaceNumber { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public PaymentType Payment { get; set; }

        public decimal Price { get; set; }

        public virtual PromotionCode PromotionCode { get; set; }
        public int PromotionCodeId { get; set; }

        public Ride()
        { }

        public enum PaymentType
        {
            Paypal,
            BankCard
        }

    }
}
