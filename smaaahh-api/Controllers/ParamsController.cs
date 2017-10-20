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
    public class ParamsController : ApiController
    {
        private Db db = new Db();

        // GET: api/Params
        public IQueryable<Params> GetParams()
        {
            return db.Params;
        }

        // GET: api/Params/5
        [ResponseType(typeof(Params))]
        public IHttpActionResult GetParams(int id)
        {
            Params @params = db.Params.Find(id);
            if (@params == null)
            {
                return NotFound();
            }

            return Ok(@params);
        }

        // PUT: api/Params/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParams(int id, Params @params)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @params.ParamsId)
            {
                return BadRequest();
            }

            db.Entry(@params).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParamsExists(id))
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

        // POST: api/Params
        [ResponseType(typeof(Params))]
        public IHttpActionResult PostParams(Params @params)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Params.Add(@params);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = @params.ParamsId }, @params);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParamsExists(int id)
        {
            return db.Params.Count(e => e.ParamsId == id) > 0;
        }
    }
}