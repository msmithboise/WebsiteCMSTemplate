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
    public class NavBarController : ApiController
    {
        private NavBarDBEntities db = new NavBarDBEntities();

        // GET: api/NavBar
        public IQueryable<NavBar> GetNavBars()
        {
            return db.NavBars;
        }

        // GET: api/NavBar/5
        [ResponseType(typeof(NavBar))]
        public IHttpActionResult GetNavBar(int id)
        {
            NavBar navBar = db.NavBars.Find(id);
            if (navBar == null)
            {
                return NotFound();
            }

            return Ok(navBar);
        }

        // PUT: api/NavBar/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNavBar(int id, NavBar navBar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != navBar.NavBarId)
            {
                return BadRequest();
            }

            db.Entry(navBar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NavBarExists(id))
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

        // POST: api/NavBar
        [ResponseType(typeof(NavBar))]
        public IHttpActionResult PostNavBar(NavBar navBar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NavBars.Add(navBar);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = navBar.NavBarId }, navBar);
        }

        // DELETE: api/NavBar/5
        [ResponseType(typeof(NavBar))]
        public IHttpActionResult DeleteNavBar(int id)
        {
            NavBar navBar = db.NavBars.Find(id);
            if (navBar == null)
            {
                return NotFound();
            }

            db.NavBars.Remove(navBar);
            db.SaveChanges();

            return Ok(navBar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NavBarExists(int id)
        {
            return db.NavBars.Count(e => e.NavBarId == id) > 0;
        }
    }
}