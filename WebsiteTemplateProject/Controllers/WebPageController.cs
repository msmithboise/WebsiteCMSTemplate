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

namespace WebsiteTemplateProject.Controllers
{
    public class WebPageController : ApiController
    {
        private WebsiteDBEntities db = new WebsiteDBEntities();

        // GET: api/WebPage
        public IQueryable<WebPage> GetWebPages()
        {
            return db.WebPages;
        }

        // GET: api/WebPage/5
        [ResponseType(typeof(WebPage))]
        public IHttpActionResult GetWebPage(int id)
        {
            WebPage webPage = db.WebPages.Find(id);
            if (webPage == null)
            {
                return NotFound();
            }

            return Ok(webPage);
        }

        // PUT: api/WebPage/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWebPage(int id, WebPage webPage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != webPage.UserID)
            {
                return BadRequest();
            }

            db.Entry(webPage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebPageExists(id))
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

        // POST: api/WebPage
        [ResponseType(typeof(WebPage))]
        public IHttpActionResult PostWebPage(WebPage webPage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WebPages.Add(webPage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = webPage.UserID }, webPage);
        }

        // DELETE: api/WebPage/5
        [ResponseType(typeof(WebPage))]
        public IHttpActionResult DeleteWebPage(int id)
        {
            WebPage webPage = db.WebPages.Find(id);
            if (webPage == null)
            {
                return NotFound();
            }

            db.WebPages.Remove(webPage);
            db.SaveChanges();

            return Ok(webPage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WebPageExists(int id)
        {
            return db.WebPages.Count(e => e.UserID == id) > 0;
        }
    }
}