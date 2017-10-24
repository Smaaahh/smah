using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smaaahh_web.ViewModel
{
    public class ViewDriverCar
    {
        public int UserId;

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImgProfil { get; set; }
        public string UserName { get; set; }

        public int CarId { get; set; }
        public int PlaceNumber { get; set; }
        public string Model { get; set; }

        public string CarPlate { get; set; }

        public ViewDriverCar()
        {
            
        }

        public ViewDriverCar(Driver driver, Car car)
        {
            UserId = driver.UserId;
            Email = driver.Email;
            Phone = driver.Phone;
            Password = driver.Password;
            FirstName = driver.FirstName;
            LastName = driver.LastName;
            ImgProfil = driver.ImgProfil;
            UserName = driver.UserName;

            CarId = car.CarId;
            PlaceNumber = car.PlaceNumber;
            Model = car.Model;
            CarPlate = car.CarPlate;

        }

    }
}