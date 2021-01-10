using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebsiteTemplateProject.Controllers
{
    public class PagesByClientController : ApiController
    {
        // GET: api/PagesByClient
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PagesByClient/5
        public string Get(string id)
        {
            return "value";
        }

        // POST: api/PagesByClient
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PagesByClient/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PagesByClient/5
        public void Delete(int id)
        {
        }
    }
}
