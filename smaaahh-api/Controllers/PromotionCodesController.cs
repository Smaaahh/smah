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
    public class PromotionCodesController : ApiController
    {
        private Db db = new Db();

        // GET: api/PromotionCodes
        public IQueryable<PromotionCode> GetPromotionCodes()
        {
            return db.PromotionCodes;
        }

        // GET: api/PromotionCodes/5
        [ResponseType(typeof(PromotionCode))]
        public IHttpActionResult GetPromotionCode(int id)
        {
            PromotionCode promotionCode = db.PromotionCodes.Find(id);
            if (promotionCode == null)
            {
                return NotFound();
            }

            return Ok(promotionCode);
        }
        
        // PUT: api/PromotionCodes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPromotionCode(int id, PromotionCode promotionCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != promotionCode.PromotionCodeId)
            {
                return BadRequest();
            }

            db.Entry(promotionCode).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotionCodeExists(id))
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

        // POST: api/PromotionCodes
        [ResponseType(typeof(PromotionCode))]
        public IHttpActionResult PostPromotionCode(PromotionCode promotionCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            promotionCode.Code = PromotionCode.GenerateCoupon(5);
            db.PromotionCodes.Add(promotionCode);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = promotionCode.PromotionCodeId }, promotionCode);
        }

        [Route("api/PromotionCodes/{code}")]
        [AcceptVerbs("GET")]
        public bool VerifPromotionCode(string code)
        {
            PromotionCode promotionCode = null;
            try
            {
                promotionCode = db.PromotionCodes.First(f => f.Code == code && f.DateEnd >= DateTime.Now && f.DateStart <= DateTime.Now);
            }
            catch (InvalidOperationException e)
            {

            }

            if (promotionCode == null)
            {
                return false;
            }

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PromotionCodeExists(int id)
        {
            return db.PromotionCodes.Count(e => e.PromotionCodeId == id) > 0;
        }
    }
}