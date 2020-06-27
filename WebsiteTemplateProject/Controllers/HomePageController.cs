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
    public class HomePageController : ApiController
    {
        private HomePageDBEntities1 db = new HomePageDBEntities1();

        //IHomePageService HomePageService;

        //public HomePageController(IHomePageService homePageService)
        //{
        //    HomePageService = homePageService;
        //}

        public HomePageController()
        {

        }

        // GET: api/HomePage
        public IQueryable<HomePage> GetHomePages()
        {
            return db.HomePages;
        }

        // GET: api/HomePage/5
        [ResponseType(typeof(HomePage))]
        public IHttpActionResult GetHomePage(int id)
        {
            HomePage homePage = db.HomePages.Find(id);
            if (homePage == null)
            {
                return NotFound();
            }

            return Ok(homePage);
        }

        // PUT: api/HomePage/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHomePage(int id, HomePage homePage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != homePage.UserID)
            {
                return BadRequest();
            }

            db.Entry(homePage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomePageExists(id))
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

        // POST: api/HomePage
        [ResponseType(typeof(HomePage))]
        public IHttpActionResult PostHomePage(HomePage homePage)
        {
            HomePageService homeService = new HomePageService();

            homeService.UpsertHomePage(homePage,db);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //db.HomePages.Add(homePage);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = homePage.UserID }, homePage);
        }

        // DELETE: api/HomePage/5
        [ResponseType(typeof(HomePage))]
        public IHttpActionResult DeleteHomePage(int id)
        {
            HomePage homePage = db.HomePages.Find(id);
            if (homePage == null)
            {
                return NotFound();
            }

            db.HomePages.Remove(homePage);
            db.SaveChanges();

            return Ok(homePage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HomePageExists(int id)
        {
            return db.HomePages.Count(e => e.UserID == id) > 0;
        }
    }
}