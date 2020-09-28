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
    public class loggedInUserController : ApiController
    {
        private LoggedInUserDBEntities db = new LoggedInUserDBEntities();
        private MyUsersDBEntities userDb = new MyUsersDBEntities();

        // GET: api/loggedInUser
        public IQueryable<loggedInUser> GetloggedInUsers()
        {
            return db.loggedInUsers;
        }

        // GET: api/loggedInUser/5
        [ResponseType(typeof(loggedInUser))]
        public IHttpActionResult GetloggedInUser(int id)
        {
            loggedInUser loggedInUser = db.loggedInUsers.Find(id);
            if (loggedInUser == null)
            {
                return NotFound();
            }

            return Ok(loggedInUser);
        }

        // PUT: api/loggedInUser/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutloggedInUser(int id, loggedInUser loggedInUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loggedInUser.loggedInUserId)
            {
                return BadRequest();
            }

            db.Entry(loggedInUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!loggedInUserExists(id))
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

        // POST: api/loggedInUser
        [ResponseType(typeof(loggedInUser))]
        public IHttpActionResult PostloggedInUser(loggedInUser loggedInUser)
        {

            LoggedInUserService loggedInUserService = new LoggedInUserService();

            loggedInUserService.PostDataForCurrentUserOnLogin(loggedInUser, db, userDb);          

            return CreatedAtRoute("DefaultApi", new { id = loggedInUser.loggedInUserId }, loggedInUser);
        }

        // DELETE: api/loggedInUser/5
        [ResponseType(typeof(loggedInUser))]
        public IHttpActionResult DeleteloggedInUser(int id)
        {
            loggedInUser loggedInUser = db.loggedInUsers.Find(id);
            if (loggedInUser == null)
            {
                return NotFound();
            }

            db.loggedInUsers.Remove(loggedInUser);
            db.SaveChanges();

            return Ok(loggedInUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool loggedInUserExists(int id)
        {
            return db.loggedInUsers.Count(e => e.loggedInUserId == id) > 0;
        }
    }
}