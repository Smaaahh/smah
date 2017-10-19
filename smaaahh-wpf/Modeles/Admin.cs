using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_wpf.Modeles
{
    public class Admin : User
    {
        public int AdminId { get; set; }
        public virtual List<Role> Roles { get; set; }

        public Admin()
        {
            Roles = new List<Role>();
        }

    }
}
