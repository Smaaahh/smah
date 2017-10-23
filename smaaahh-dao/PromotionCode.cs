using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_dao
{
    public class PromotionCode
    {
        [Key]
        public int PromotionCodeId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Reduction { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Image { get; set; }

        public PromotionCode ()
        {
        }
        public static string GenerateCoupon(int length)
        {
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

    }
}
