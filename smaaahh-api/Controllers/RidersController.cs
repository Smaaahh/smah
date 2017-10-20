﻿using System;
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
    public class RidersController : ApiController
    {
        private Db db = new Db();

        // GET: api/Riders
        public IQueryable<Rider> GetRiders()
        {
            return db.Riders;
        }

        // GET: api/Riders/5
        [ResponseType(typeof(Rider))]
        public IHttpActionResult GetRider(int id)
        {
            Rider rider = db.Riders.Find(id);
            if (rider == null)
            {
                return NotFound();
            }

            return Ok(rider);
        }

        // PUT: api/Riders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRider(int id, Rider rider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rider.UserId)
            {
                return BadRequest();
            }

            db.Entry(rider).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiderExists(id))
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

        // POST: api/Riders
        [ResponseType(typeof(Rider))]
        public IHttpActionResult PostRider(Rider rider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Riders.Add(rider);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rider.UserId }, rider);
        }

        // DELETE: api/Riders/5
        [ResponseType(typeof(Rider))]
        public IHttpActionResult DeleteRider(int id)
        {
            Rider rider = db.Riders.Find(id);
            if (rider == null)
            {
                return NotFound();
            }

            rider.State = Rider.RiderState.Disabled;
            db.Entry(rider).Property("State").IsModified = true;
            db.SaveChanges();

            return Ok(rider);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RiderExists(int id)
        {
            return db.Riders.Count(e => e.UserId == id) > 0;
        }
    }
}