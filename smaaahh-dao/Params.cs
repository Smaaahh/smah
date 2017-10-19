using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_dao
{
    public class Params
    {
        [Key]
        public int ParamsId { get; set; }
        public decimal Price { get; set; }
    }
}
