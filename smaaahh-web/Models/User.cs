using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_web
{
    public abstract class User
    {
        public int UserId;

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.Password), MinLength(5)]
        public string Password { get; set; }
        public string FirstName { get ; set; }
        public string LastName { get ; set ; }
        public string ImgProfil { get; set; }
        public string UserName { get; set; }

        public User()
        {

        }

        public double getRating()
        {
            if (this is Driver)
            {
                return (this as Driver).Rating;
            }
            if (this is Rider)
            {
                return (this as Rider).Rating;
            }
            return 0;
        }
        
    }

}

