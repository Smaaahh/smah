﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_wpf.Modeles
{
    public abstract class User
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImgProfil { get; set; }
        public string UserName { get; set; }

        public User()
        {

        }
    }
}