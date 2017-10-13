using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_dao
{
   public class Admin : User
    {
        public int AdminId { get; set; }
        public List<Role> Roles { get; set; }

        public Admin() : base()
        {
            Roles = new List<Role>();
        }
 
    }
}
