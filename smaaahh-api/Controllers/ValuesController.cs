using smaaahh_dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace smaaahh_api.Controllers
{
    public class ValuesController : ApiController
    {
        private Db db = new Db();
        // GET api/values
        public IEnumerable<string> Get()
        {

            return db.Drivers.ToList() as IEnumerable<string>;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
