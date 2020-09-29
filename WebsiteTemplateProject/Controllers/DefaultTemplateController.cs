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
using WebsiteTemplateProject;
using WebsiteTemplateProject.Models;
using WebsiteTemplateProject.Service;

namespace WebsiteTemplateProject.Controllers
{
    public class DefaultTemplateController : ApiController
    {
        private NewWebContentEntities2 db = new NewWebContentEntities2();

        // GET: api/DefaultTemplate
        public IQueryable<WebContent> GetWebContents()
        {
            return db.WebContents;
        }

        // GET: api/DefaultTemplate/5
        [ResponseType(typeof(WebContent))]
        public IHttpActionResult GetWebContent(int id)
        {
            WebContent webContent = db.WebContents.Find(id);
            if (webContent == null)
            {
                return NotFound();
            }

            return Ok(webContent);
        }

        // PUT: api/DefaultTemplate/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWebContent(int id, WebContent webContent)
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

        // POST: api/DefaultTemplate
        [ResponseType(typeof(WebContent))]
        public IHttpActionResult PostWebContent(WebContent webContent)
        {
            DefaultTemplateService defaultTemplateService = new DefaultTemplateService();

            defaultTemplateService.CreateHeroImage(webContent,db);
            defaultTemplateService.CreateTextHeader(webContent, db);

            return CreatedAtRoute("DefaultApi", new { id = webContent.Id }, webContent);
        }

        // DELETE: api/DefaultTemplate/5
        [ResponseType(typeof(WebContent))]
        public IHttpActionResult DeleteWebContent(int id)
        {
            WebContent webContent = db.WebContents.Find(id);
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