using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_dao
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Title { get; set; }
        public virtual List<Admin> Admins { get; set; }

        public Role()
        {
            Admins = new List<Admin>();
        }
    }
}
