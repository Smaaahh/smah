using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using smaaahh_dao;

namespace smaaahh_api.Controllers
{
    public class RatingsController : ApiController
    {
        private Db db = new Db();

        public RatingsController()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;

            config.Formatters.JsonFormatter
                        .SerializerSettings
                        .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // GET: api/Ratings
        public IHttpActionResult GetRatings()
        {
            return Json(db.Ratings.ToList());
        }

        [Route("api/TopRatingsByUser/{type}/{UserId}")]
        public IHttpActionResult GetTopRatingsByUser(int UserId, string type)
        {
            IQueryable<Rating> ratings = null;

            if (type == "driver")
            {
                ratings = db.Ratings.Where(t => t.Enabled == true && t.Ride.DriverId == UserId && t.RiderId != null && t.isTop == true);
            }
            if (type == "rider")
            {
                ratings = db.Ratings.Where(t => t.Enabled == true && t.Ride.RiderId == UserId && t.DriverId != null && t.isTop == true);
            }

            return Json(ratings.ToList());
        }

        [Route("api/RatingsByUser/{type}/{UserId}")]
        public IHttpActionResult GetRatingsByUser(int UserId, string type)
        {
            IQueryable<Rating> ratings = null;

            if (type == "driver")
            {
                ratings = db.Ratings.Where(t => t.Enabled == true && t.Ride.DriverId == UserId && t.RiderId != null);
            }
            if (type == "rider")
            {
                ratings = db.Ratings.Where(t => t.Enabled == true && t.Ride.RiderId == UserId && t.DriverId != null);
            }

            return Json(ratings.ToList());
        }

        // GET: api/Ratings/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult GetRating(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Ratings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRating(int id, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.RatingId)
            {
                return BadRequest();
            }

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Ratings
        [ResponseType(typeof(Rating))]
        public IHttpActionResult PostRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Rating> ratings = null;

            if (rating.RiderId != null)
            {
                Driver d = db.Drivers.Find(db.Rides.Find(rating.RideId).DriverId);
                ratings = db.Ratings.Where(t => t.Enabled == true && t.Ride.DriverId == d.UserId && t.RiderId != null);
                if (ratings.Count() > 0)
                {
                    d.Rating = (ratings.Sum(r => r.Note) + rating.Note) / ratings.Count();
                }
                else
                {
                    d.Rating = rating.Note;
                }
                db.Entry(d).Property("Rating").IsModified = true;
            }
            if (rating.DriverId != null)
            {
                Rider ri = db.Riders.Find(db.Rides.Find(rating.RideId).RiderId);
                ratings = db.Ratings.Where(t => t.Enabled == true && t.Ride.RiderId == ri.UserId && t.DriverId != null);
                if (ratings.Count() > 0)
                {
                    ri.Rating = (ratings.Sum(r => r.Note) + rating.Note) / ratings.Count();
                }
                else
                {
                    ri.Rating = rating.Note;
                }
                
                db.Entry(ri).Property("Rating").IsModified = true;
            }

            db.Ratings.Add(rating);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rating.RatingId }, rating);
        }

        // DELETE: api/Ratings/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult DeleteRating(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            db.Ratings.Remove(rating);
            db.SaveChanges();

            return Ok(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(int id)
        {
            return db.Ratings.Count(e => e.RatingId == id) > 0;
        }
    }
}