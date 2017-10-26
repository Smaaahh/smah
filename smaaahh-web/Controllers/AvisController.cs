using smaaahh_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace smaaahh_web.Controllers
{
    public class AvisController : DefaultController
    {
        // GET: Avis
        public ActionResult Create(int RideId)
        {
            if (Session["Type"] == null)
            {
                return RedirectToAction("Login", "Users");
            }

            Ride ride = new Ride();
            Task.Run(async () =>
            {
                ride = await CallApi<Ride>($"api/Ride/{RideId}", true);
            }).Wait();

            ViewBag.Ride = ride;
            Rating rating = new Rating()
            {
                Ride = ride,
                RideId = RideId,
                DateCreation = DateTime.Now,
                Enabled = true,
                isTop = false
            };
            if (Session["Type"].ToString() == "rider")
            {
                rating.RiderId = int.Parse(Session["UserId"].ToString());
                rating.DriverId = null;
            }

            if (Session["Type"].ToString() == "driver")
            {
                rating.DriverId = int.Parse(Session["UserId"].ToString());
                rating.RiderId = null;
            }

            return View(rating);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind()] Rating rating)
        {
            // mettre à jour le rider
            Task.Run(async () =>
            {
                Rating ratingget = await CreateItem<Rating>(rating,"rating");
            }).Wait();


            // on redirige vers le dashboard

            return RedirectToAction("DashBoard", "Riders");
        }
    }
}