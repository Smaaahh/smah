using smaaahh_web.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace smaaahh_web.Controllers
{
    public class DriversController : DefaultController
    {
        // GET: Drivers
        public ActionResult Create()
        {
            return View();
        }
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Nom, string Prenom, string Pseudo, string Email, string Password, string NTelephone, string Modele_voiture, int Nbplace_voiture, string Immatriculation, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                // on vérifie que personne n'utilise déjà cette adresse mail
                // FAIT DANS L'API
                // on créé un driver
                Driver driver = new Driver(Nom, Prenom, Pseudo, Email, Password, NTelephone, "/content/images/profils/default.jpg");
                Object resultat = null;
                // on l'envoie à l'api et récupère l'id
                Task.Run(async () =>
                {
                    resultat = await CreateItem(driver, "driver");
                }).Wait();
                // on récupère l'image de profil et on la stocke au bon endroit
                if (resultat is Driver)
                {
                    driver = resultat as Driver;
                    try
                    {
                        if (File.ContentLength > 0)
                        {
                            string fileName = Pseudo + "_" + driver.UserId;
                            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                            {
                                fileName = fileName.Replace(c, '_');
                            }

                            string path = Path.Combine(Server.MapPath("/content/images/profils"), fileName + Path.GetExtension(File.FileName));
                            File.SaveAs(path);
                            fileName = "/content/images/profils/" + fileName + Path.GetExtension(File.FileName);
                            // mise à jour du driver avec la bonne image
                            driver.ImgProfil = fileName;
                            Task.Run(async () =>
                            {
                                await UpdateDriver(driver);
                            }).Wait();
                        }
                        ViewBag.Message = "Ficher envoyé";
                        //pas ça
                        // on enregistre la voiture
                        Car car = new Car(Modele_voiture, Nbplace_voiture, Immatriculation, driver.UserId);
                        Task.Run(async () =>
                        {
                            resultat = await CreateItem(car, "car");
                        }).Wait();
                        //end pas sa

                        // on redirige vers la page de login
                        Session["Message"] = "Merci de vous être inscrit. Veuillez maintenant vous identifier.";
                        return RedirectToAction("Login", "Users");
                    }
                    catch (Exception e)
                    {
                        Session["Message"] = "L'image n'a pas pu être envoyée. Votre compte a été créé, veuillez vous identifier.";
                        return RedirectToAction("Login", "Users");
                    }
                }
                else
                {
                    // le driver n'a pas pu être créé
                    ViewBag.Message = resultat;
                }
            }
            return View();

        }

        public ActionResult Dashboard()
        {
            return View();
        }
        // update rider


        public ActionResult Update()
        {
            
            // on doit recuperer les infos du driver pour les passer à la vue
            Driver driver = null;
            Task.Run(async () =>
            {
                driver = await GetDriver(Session["UserEmail"].ToString());
            }).Wait();

            Car car = null;
            Task.Run(async () =>
            {
                car = await GetCar(Session["UserId"].ToString());
            }).Wait();
            return View(new ViewDriverCar(driver, car));
        }
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "CarId, Email,Phone,Password,FirstName,LastName,ImgProfil,UserName,Model,PlaceNumber,CarPlate")] ViewDriverCar driverCar,  HttpPostedFileBase File)
        {
            Driver driver = new Driver(driverCar.LastName, driverCar.FirstName, driverCar.UserName, driverCar.Email, driverCar.Password, driverCar.Phone, driverCar.ImgProfil);
            driver.UserId = int.Parse(Session["UserId"].ToString());
            
            Car car = new Car(driverCar.Model, driverCar.PlaceNumber, driverCar.CarPlate, driver.UserId);
            car.CarId = driverCar.CarId;
            try
            {
                if (File.ContentLength > 0)
                {
                    string fileName = driver.UserName + "_" + driver.UserId;
                    foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                    {
                        fileName = fileName.Replace(c, '_');
                    }
                    // supprimer l'ancienne image
                    
                    //*****
                    string path = Path.Combine(Server.MapPath("/content/images/profils"), fileName + Path.GetExtension(File.FileName));
                    File.SaveAs(path);
                    fileName = "/content/images/profils/" + fileName + Path.GetExtension(File.FileName);
                    // mise à jour du driver avec la bonne image
                    driver.ImgProfil = fileName;
                    
                }
                //ViewBag.Message = "Ficher envoyé";
                
            }
            catch (Exception e)
            {
                Session["Message"] = "L'image n'a pas pu être envoyée. Votre compte a été créé, veuillez vous identifier.";
                //return RedirectToAction("Login", "Users");
            }
            // mettre à jour le driver
            Task.Run(async () =>
            {
                await UpdateDriver(driver);
            }).Wait();
            // on met à jour la voiture
            Task.Run(async () =>
            {
                await UpdateCar(car);
            }).Wait();

            // on redirige vers le dashboard

            return RedirectToAction("DashBoard", "Drivers");

        }
    }
}