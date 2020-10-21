using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebsiteTemplateProject.Models;

namespace WebsiteTemplateProject.Controllers
{
    public class SubContentController : ApiController
    {
        private ContentDBEntities db = new ContentDBEntities();

        // GET: api/SubContent
        public IEnumerable<Models.WebContent> Get()
        {
            // return new string[] { "value1", "value2" };

            return db.WebContents;
        }


        // GET: api/SubContent/5
        [Route("api/SubContent/{id}/{subId}")]
        public string Get(int id, int subId)
        {
            return "value";
        }

        // POST: api/SubContent
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SubContent/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SubContent/5
        public void Delete(int id)
        {
        }
    }
}
