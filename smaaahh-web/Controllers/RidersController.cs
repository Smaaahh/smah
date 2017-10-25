using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace smaaahh_web.Controllers
{
    public class RidersController : DefaultController
    {
        // GET: Riders
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Nom, string Prenom, string Pseudo, string Email, string Password, string NTelephone, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                // on vérifie que personne n'utilise déjà cette adresse mail
                // FAIT DANS L'API
                // on créé un driver
                Rider rider = new Rider(Nom, Prenom, Pseudo, Email, Password, NTelephone, "/content/images/profils/default.jpg");
                object resultat = null;
                // on l'envoie à l'api et récupère l'id
                Task.Run(async () =>
                {
                    resultat = await CreateItem(rider, "rider");
                }).Wait();
                // on récupère l'image de profil et on la stocke au bon endroit
                if (resultat is Rider)
                {
                    rider = resultat as Rider;
                    try
                    {
                        if (File.ContentLength > 0)
                        {
                            string fileName = Pseudo + "_" + rider.UserId;
                            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                            {
                                fileName = fileName.Replace(c, '_');
                            }

                            string path = Path.Combine(Server.MapPath("/content/images/profils"), fileName + Path.GetExtension(File.FileName));
                            File.SaveAs(path);
                            fileName = "/content/images/profils/" + fileName + Path.GetExtension(File.FileName);
                            // mise à jour du driver avec la bonne image
                            rider.ImgProfil = fileName;
                            Task.Run(async () =>
                            {
                                await UpdateRider(rider);
                            }).Wait();
                        }
                        //ViewBag.Message = "ficher envoye";
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

        public ActionResult Update()
        {
            // on doit recuperer les infos du rider pour les passer à la vue
            Rider rider = null;
            Task.Run(async () =>
            {
                rider = await GetRider(Session["UserEmail"].ToString());
            }).Wait();
            return View(rider);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "Email,Phone,Password,FirstName,LastName,ImgProfil,UserName")] Rider rider, HttpPostedFileBase File)
        {
            rider.UserId = int.Parse(Session["UserId"].ToString());
            try
            {
                if (File.ContentLength > 0)
                {
                    string fileName = rider.UserName + "_" + rider.UserId;
                    foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                    {
                        fileName = fileName.Replace(c, '_');
                    }
                    // supprimer l'ancienne image 
                    // **** A FAIRE *****************
                    //System.IO.File.Delete(rider.ImgProfil);
                    string path = Path.Combine(Server.MapPath("/content/images/profils"), fileName + Path.GetExtension(File.FileName));
                    File.SaveAs(path);
                    fileName = "/content/images/profils/" + fileName + Path.GetExtension(File.FileName);
                    // mise à jour du driver avec la bonne image
                    rider.ImgProfil = fileName;
                }
            }
            catch (Exception e)
            {
                Session["Message"] = "L'image n'a pas pu être envoyée. Votre compte a été créé, veuillez vous identifier.";
             }
            // mettre à jour le rider
            Task.Run(async () =>
            {
                await UpdateRider(rider);
            }).Wait();
            

            // on redirige vers le dashboard

            return RedirectToAction("DashBoard", "Riders");
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Payment()
        {
            // on va chercher la riderequest
            RideRequest rideRequest = null;
            Task.Run(async () =>
            {
                rideRequest = await GetRideRequestByRider(int.Parse(Session["UserId"].ToString()));
            }).Wait();

            return View(rideRequest);
        }

    }

}