﻿using System;
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
    public class SubPagesByClientUrlController : ApiController
    {
        private NewClientWebPagesDBEntities db = new NewClientWebPagesDBEntities();

        // GET: api/SubPagesByClientUrl
        public IQueryable<CustomPage> GetCustomPages()
        {
            return db.CustomPages;
        }

        // GET: api/SubPagesByClientUrl/5
        [ResponseType(typeof(List<CustomPage>))]
        public IHttpActionResult GetCustomPage(int id)
        {
            SubPageByClientUrlService subPageByClientUrlService = new SubPageByClientUrlService();

            var subPages = subPageByClientUrlService.GetSubNavLinks(db, id);

            return Ok(subPages);
        }

        // PUT: api/SubPagesByClientUrl/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomPage(int id, CustomPage customPage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customPage.PageId)
            {
                return BadRequest();
            }

            db.Entry(customPage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomPageExists(id))
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

        // POST: api/SubPagesByClientUrl
        [ResponseType(typeof(CustomPage))]
        public IHttpActionResult PostCustomPage(CustomPage customPage)
        {

            SubPageByClientUrlService subPageByClientUrlService = new SubPageByClientUrlService();

            subPageByClientUrlService.UpsertSubPage(customPage, db);

            return CreatedAtRoute("DefaultApi", new { id = customPage.PageId }, customPage);
        }

        // DELETE: api/SubPagesByClientUrl/5
        [ResponseType(typeof(CustomPage))]
        public IHttpActionResult DeleteCustomPage(int id)
        {
            CustomPage customPage = db.CustomPages.Find(id);
            if (customPage == null)
            {
                return NotFound();
            }

            db.CustomPages.Remove(customPage);
            db.SaveChanges();

            return Ok(customPage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomPageExists(int id)
        {
            return db.CustomPages.Count(e => e.PageId == id) > 0;
        }
    }
}