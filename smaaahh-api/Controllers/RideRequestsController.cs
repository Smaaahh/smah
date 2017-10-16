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
    public class RideRequestsController : ApiController
    {
        private Db db = new Db();

        // GET: api/RideRequests
        public IQueryable<RideRequest> GetRideRequests()
        {
            return db.RideRequests;
        }

        // GET: api/RideRequests/5
        [ResponseType(typeof(RideRequest))]
        public IHttpActionResult GetRideRequest(int id)
        {
            RideRequest rideRequest = db.RideRequests.Find(id);
            if (rideRequest == null)
            {
                return NotFound();
            }

            return Ok(rideRequest);
        }

        // PUT: api/RideRequests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRideRequest(int id, RideRequest rideRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rideRequest.RideRequestID)
            {
                return BadRequest();
            }

            db.Entry(rideRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RideRequestExists(id))
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

        // POST: api/RideRequests
        [ResponseType(typeof(RideRequest))]
        public IHttpActionResult PostRideRequest(RideRequest rideRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RideRequests.Add(rideRequest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rideRequest.RideRequestID }, rideRequest);
        }

        // DELETE: api/RideRequests/5
        [ResponseType(typeof(RideRequest))]
        public IHttpActionResult DeleteRideRequest(int id)
        {
            RideRequest rideRequest = db.RideRequests.Find(id);
            if (rideRequest == null)
            {
                return NotFound();
            }

            db.RideRequests.Remove(rideRequest);
            db.SaveChanges();

            return Ok(rideRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RideRequestExists(int id)
        {
            return db.RideRequests.Count(e => e.RideRequestID == id) > 0;
        }
    }
}