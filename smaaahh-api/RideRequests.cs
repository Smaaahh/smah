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
    
    public partial class RideRequests
    {
        public int RideRequestID { get; set; }
        public int RiderId { get; set; }
        public decimal PosXStart { get; set; }
        public decimal PosYStart { get; set; }
        public decimal PosXEnd { get; set; }
        public decimal PosYEnd { get; set; }
        public int PlaceNumber { get; set; }
        public System.DateTime DateCreation { get; set; }
    
        public virtual Riders Riders { get; set; }
    }
}
