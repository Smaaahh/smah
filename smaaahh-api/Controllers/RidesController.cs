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
    public class RidesController : ApiController
    {
        private Db db = new Db();

        // GET: api/Rides
        public IHttpActionResult GetRide()
        {
            return Json(db.Rides.ToList());
        }

        // GET: api/Rides/5
        [ResponseType(typeof(Ride))]
        public IHttpActionResult GetRide(int id)
        {
            Ride ride = db.Rides.Find(id);
            if (ride == null)
            {
                return NotFound();
            }

            return Ok(ride);
        }

        // PUT: api/Rides/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRide(int id, Ride ride)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ride.RideId)
            {
                return BadRequest();
            }

            db.Entry(ride).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RideExists(id))
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

        // POST: api/Rides
        [ResponseType(typeof(Ride))]
        public IHttpActionResult PostRide(Ride ride)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rides.Add(ride);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ride.RideId }, ride);
        }

        // POST: api/Rides
        [HttpPost]
        [Route("api/Rides/ConvertRideRequest")]
        [ResponseType(typeof(Ride))]
        public IHttpActionResult PostConvertRideRequest(RideRequest rideRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Params priceParam = new Params();
            decimal price = priceParam.Price * rideRequest.nbKm;
            Ride ride = new Ride()
            {
                RiderId = rideRequest.RiderId,
                DriverId = rideRequest.DriverId,
                PosXStart = rideRequest.PosXStart,
                PosXEnd = rideRequest.PosXEnd,
                PosYEnd = rideRequest.PosYEnd,
                PosYStart = rideRequest.PosYStart,
                PlaceNumber = rideRequest.PlaceNumber,
                DateCreation = DateTime.Now,
                DateStart = null,
                DateEnd = null,
                Price = price,
                Payment = null,
                PromotionCodeId = null
            };
            db.Rides.Add(ride);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ride.RideId }, ride);
        }

        // DELETE: api/Rides/5
        [ResponseType(typeof(Ride))]
        public IHttpActionResult DeleteRide(int id)
        {
            Ride ride = db.Rides.Find(id);
            if (ride == null)
            {
                return NotFound();
            }

            db.Rides.Remove(ride);
            db.SaveChanges();

            return Ok(ride);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RideExists(int id)
        {
            return db.Rides.Count(e => e.RideId == id) > 0;
        }
    }
}