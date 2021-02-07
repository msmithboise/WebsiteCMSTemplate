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
    public class NavbarByClientUrlController : ApiController
    {
        private ClientNavbarDBEntities2 db = new ClientNavbarDBEntities2();

        // GET: api/NavbarByClientUrl
        public IQueryable<NavBar> GetNavBars()
        {
            return db.NavBars;
        }

        // GET: api/NavbarByClientUrl/5
        //[ResponseType(typeof(NavBar))]
        //public IHttpActionResult GetNavBar(string id)
        //{
        //    NavBar navBar = db.NavBars.Find(id);
        //    if (navBar == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(navBar);
        //}

        // GET: api/NavbarByClientUrl/5
        [ResponseType(typeof(NavBar))]
        public IHttpActionResult GetCustomPage(string id)
        {
            NavBarService navbarService = new NavBarService();

            var navbars = navbarService.GetAllNavbarsByClientUrl(id, db);

            return Ok(navbars);
        }

        // PUT: api/NavbarByClientUrl/5
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

        // POST: api/NavbarByClientUrl
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

        // DELETE: api/NavbarByClientUrl/5
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