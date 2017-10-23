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
using System.Drawing;
using System.Web.Http.Cors;
using smaaahh_api.Models;

namespace smaaahh_api.Controllers
{
    [EnableCors(origins: "*",headers:"*",methods:"*")]
    public class DriversController : ApiController
    {
        private Db db = new Db();

        // GET: api/Drivers
        public IQueryable<Driver> GetDrivers()
        {
            return db.Drivers;
        }
        
        [Route("api/Drivers/Free")]
        public IQueryable<Driver> GetDriversFree()
        {
            return db.Drivers.Where(f =>f.Free == true && f.Active == true && f.State == Driver.DriverState.Enabled);
        }
       
        // GET: api/Drivers/5
        [ResponseType(typeof(Driver))]
        public IHttpActionResult GetDriver(int id)
        {
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        // PUT: api/Drivers/5
        
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDriver(int id, Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != driver.UserId)
            {
                return BadRequest();
            }

            db.Entry(driver).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
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

        // POST: api/Drivers
        [ResponseType(typeof(Driver))]
        public IHttpActionResult PostDriver(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool Error = Users.verifEmail(driver.Email);

            if ( !Error)
            {
                db.Drivers.Add(driver);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = driver.UserId }, driver);
            }
            else
            {
                return BadRequest("Cette adresse mail est déjà utilisée");
            }
        }

        // DELETE: api/Drivers/5
        [ResponseType(typeof(Driver))]
        public IHttpActionResult DeleteDriver(int id)
        {
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return NotFound();
            }
            driver.State = Driver.DriverState.Disabled;
            db.Entry(driver).Property("State").IsModified = true;
            db.SaveChanges();

            return Ok(driver);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DriverExists(int id)
        {
            return db.Drivers.Count(e => e.UserId == id) > 0;
        }
        
    }
}