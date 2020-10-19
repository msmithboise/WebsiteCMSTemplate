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
    public class SubPagesController : ApiController
    {
        private SubPageDBEntities db = new SubPageDBEntities();

        // GET: api/SubPages
        public IQueryable<SubPage> GetSubPages()
        {
            return db.SubPages;
        }

        // GET: api/SubPages/5
        [ResponseType(typeof(SubPage))]
        public IHttpActionResult GetSubPage(int id)
        {
            SubPageService subPageService = new SubPageService();

            var subPages = subPageService.GetSubPagesByPageId(id, db);

            return Ok(subPages);
        }

        // PUT: api/SubPages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubPage(int id, SubPage subPage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subPage.SubPageId)
            {
                return BadRequest();
            }

            db.Entry(subPage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubPageExists(id))
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

        // POST: api/SubPages
        [ResponseType(typeof(SubPage))]
        public IHttpActionResult PostSubPage(SubPage subPage)
        {
            SubPageService subPageService = new SubPageService();

            subPageService.UpsertSubPage(subPage, db);

            return CreatedAtRoute("DefaultApi", new { id = subPage.SubPageId }, subPage);
        }

        // DELETE: api/SubPages/5
        [ResponseType(typeof(SubPage))]
        public IHttpActionResult DeleteSubPage(int id)
        {
            SubPage subPage = db.SubPages.Find(id);
            if (subPage == null)
            {
                return NotFound();
            }

            db.SubPages.Remove(subPage);
            db.SaveChanges();

            return Ok(subPage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubPageExists(int id)
        {
            return db.SubPages.Count(e => e.SubPageId == id) > 0;
        }
    }
}