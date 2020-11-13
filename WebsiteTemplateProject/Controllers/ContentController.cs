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
using WebsiteTemplateProject.Models;
using WebsiteTemplateProject.Service;

namespace WebsiteTemplateProject.Controllers
{
    public class ContentController : ApiController
    {
        private NewWebUserDBEntities db = new NewWebUserDBEntities();

        // GET: api/Content
        public IQueryable<WebsiteTemplateProject.Models.WebContent> GetWebContents()
        {
            return db.WebContents;
        }

        // GET: api/Content/5
        [ResponseType(typeof(WebsiteTemplateProject.Models.WebContent))]
        public IHttpActionResult GetWebContent(int id)
        {
            WebContentService webContentService = new WebContentService();

          var content =  webContentService.GetContentVMLists(id,db);

            return Ok(content);
        }

        // PUT: api/Content/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWebContent(int id, WebsiteTemplateProject.Models.WebContent webContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != webContent.Id)
            {
                return BadRequest();
            }

            db.Entry(webContent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebContentExists(id))
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

        // POST: api/Content
        [ResponseType(typeof(WebsiteTemplateProject.Models.WebContent))]
        public IHttpActionResult PostWebContent(WebsiteTemplateProject.Models.WebContent webContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WebContents.Add(webContent);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = webContent.Id }, webContent);
        }

        // DELETE: api/Content/5
        [ResponseType(typeof(WebsiteTemplateProject.Models.WebContent))]
        public IHttpActionResult DeleteWebContent(int id)
        {
            WebsiteTemplateProject.Models.WebContent webContent = db.WebContents.Find(id);
            if (webContent == null)
            {
                return NotFound();
            }

            db.WebContents.Remove(webContent);
            db.SaveChanges();

            return Ok(webContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WebContentExists(int id)
        {
            return db.WebContents.Count(e => e.Id == id) > 0;
        }
    }
}