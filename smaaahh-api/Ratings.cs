//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace smaaahh_api
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ratings
    {
        public int RatingId { get; set; }
        public string Message { get; set; }
        public int Note { get; set; }
        public System.DateTime DateCreation { get; set; }
        public bool Enabled { get; set; }
        public Nullable<int> RiderId { get; set; }
        public Nullable<int> DriverId { get; set; }
    
        public virtual Drivers Drivers { get; set; }
        public virtual Riders Riders { get; set; }
    }
}
