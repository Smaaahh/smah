using smaaahh_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Windows;

namespace smaaahh_web.Controllers
{
    public class UsersController : DefaultController
    {
        public ActionResult Login()
        {
            ViewBag.Message = Session["Message"];
            Session["Message"] = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            string token = null;
            Task.Run(async () =>
            {
                token = await GetToken(email, password, "driver");
            }).Wait();

            if (token == "Wrong access")
            {
                Task.Run(async () =>
                {
                    token = await GetToken(email, password, "rider");
                }).Wait();
                if (token == "Wrong access")
                {
                    // email / password invalide
                    ViewBag.ErrorMessage = "Email / password invalide.";
                    return View();
                }
                else if (token == "RSA key error")
                {
                    // email / password invalide
                    ViewBag.ErrorMessage = "Probleme de cryptage.";
                    return View();
                }
                else
                {
                    // si rider est bien identifié
                    // redirection vers l'écran principal
                    Session.Clear();
                    Session["token"] = token;

                    loginUser(email, "rider");

                    return RedirectToAction("Dashboard", "Riders");

                }
            }
            else if (token == "RSA key error")
            {
                // email / password invalide
                ViewBag.ErrorMessage = "Probleme de cryptage.";
                return View();
            }
            else
            {
                // si driver est bien identifié
                // redirection vers l'écran principal
                Session.Clear();
                Session["token"] = token;

                loginUser(email, "driver");
                return RedirectToAction("Dashboard", "Drivers");

            }
        }

        public ActionResult Logout()
        {
            if (Session["Type"].ToString() == "driver")
            {
                // qu'est-ce qu'il se passe s'il se déconnecte avant d'avoir terminé sa course ????
                //****************************
                // modifier les états
                // mettre à jour le driver
                MettreAJourEtatDriver(false, false);
            }
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public void loginUser(string email, string type)
        {
            // aller recuperer le rider et stocker en session
            User user = null;

            Task.Run(async () =>
            {
                switch (type)
                {
                    case "rider":
                        user = await GetRider(email);
                        break;
                    case "driver":
                        user = await GetDriver(email);
                        break;
                }

            }).Wait();

            Session["Type"] = type;
            Session["UserId"] = user.UserId;
            Session["UserFirstName"] = user.FirstName;
            Session["UserLastName"] = user.LastName;
            Session["UserName"] = user.UserName;
            Session["UserEmail"] = user.Email;
            Session["ImgProfil"] = user.ImgProfil;

            // mettre le RideRequest en session
            RideRequest rideRequest = null;
            Task.Run(async () =>
            {
                rideRequest = await GetRideRequestByRider(user.UserId);
            }).Wait();
            Session["RideRequestId"] = (rideRequest == null) ? 0 : rideRequest.RideRequestID;
            // mettre à jour la position du driver
            // => sera fait lors de l'affichage du dashboard
            // et son etat
            if (type == "driver")
            {
                MettreAJourEtatDriver(true, true);
            }
        }

        public void MettreAJourEtatDriver(bool active, bool free)
        {
            Driver driver = null;
            Task.Run(async () =>
            {
                driver = await GetDriver(Session["UserEmail"].ToString());
            }).Wait();
            driver.Active = active;
            driver.Free = free;
            Task.Run(async () =>
            {
                await UpdateDriver(driver);
            }).Wait();
        }


        public ActionResult Profil(int id,string type)
        {
            
            Task.Run(async () =>
            {
                ViewBag.ListAvisTop = await CallApi<List<Rating>>($"api/TopRatingsByUser/{type}/{id}", false);
                ViewBag.ListAvis = await CallApi<List<Rating>>($"api/RatingsByUser/{type}/{id}", false);
            }).Wait();
            User user = null;
            ViewBag.Type = type;
            if ( type == "driver")
            {
                Task.Run(async () =>
                {
                    user = await CallApi<Driver>($"api/Drivers/{id}", false);
                }).Wait();
            }
            if (type == "rider")
            {
                Task.Run(async () =>
                {
                    user = await CallApi<Rider>($"api/Riders/{id}", false);
                }).Wait();
            }
            ViewBag.User = user;
            return View("Rating");
        }
        
    }
}
