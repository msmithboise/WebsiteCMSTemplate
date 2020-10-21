using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebsiteTemplateProject.Models;
using WebsiteTemplateProject.Service;

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
        [Route("api/SubContent/{subPageId}/{pageId}")]
        [ResponseType(typeof(Models.WebContent))]
        public IHttpActionResult Get(int pageId, int subPageId)
        {
            SubContentService subContentService = new SubContentService();

           var content = subContentService.GetWebContentByPageIdAndSubPageId(pageId, subPageId, db);

            return Ok(content);
        }

        // POST: api/SubContent
        [ResponseType(typeof(Models.WebContent))]
        public IHttpActionResult Post([FromBody]Models.WebContent webContent)
        {
            SubContentService subContentService = new SubContentService();

            subContentService.UpsertSubContent(webContent,db);


            return CreatedAtRoute("DefaultApi", new { id = webContent.Id }, webContent);
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
