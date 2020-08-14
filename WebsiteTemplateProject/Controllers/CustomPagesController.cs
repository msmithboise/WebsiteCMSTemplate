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

namespace WebsiteTemplateProject.Controllers
{
    public class CustomPagesController : ApiController
    {
        private WebsiteDBCustomPageEntities db = new WebsiteDBCustomPageEntities();

        // GET: api/CustomPages
        public IQueryable<CustomPage> GetCustomPages()
        {
            return db.CustomPages;
        }

        // GET: api/CustomPages/5
        [ResponseType(typeof(CustomPage))]
        public IHttpActionResult GetCustomPage(int id)
        {
            CustomPage customPage = db.CustomPages.Find(id);
            if (customPage == null)
            {
                return NotFound();
            }

            return Ok(customPage);
        }

        // PUT: api/CustomPages/5
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

        // POST: api/CustomPages
        [ResponseType(typeof(CustomPage))]
        public IHttpActionResult PostCustomPage(CustomPage customPage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomPages.Add(customPage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customPage.PageId }, customPage);
        }

        // DELETE: api/CustomPages/5
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