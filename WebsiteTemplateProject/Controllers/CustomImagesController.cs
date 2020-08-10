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
    public class CustomImagesController : ApiController
    {
        private CustomImageDBEntities2 db = new CustomImageDBEntities2();

        // GET: api/CustomImages
        public IQueryable<CustomImage> GetCustomImages()
        {
            return db.CustomImages;
        }

        // GET: api/CustomImages/5
        [ResponseType(typeof(CustomImage))]
        public IHttpActionResult GetCustomImage(int id)
        {
            CustomImage customImage = db.CustomImages.Find(id);
            if (customImage == null)
            {
                return NotFound();
            }

            return Ok(customImage);
        }

        // PUT: api/CustomImages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomImage(int id, CustomImage customImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customImage.ImageId)
            {
                return BadRequest();
            }

            db.Entry(customImage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomImageExists(id))
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

        // POST: api/CustomImages
        [ResponseType(typeof(CustomImage))]
        public IHttpActionResult PostCustomImage(CustomImage customImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomImages.Add(customImage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customImage.ImageId }, customImage);
        }

        // DELETE: api/CustomImages/5
        [ResponseType(typeof(CustomImage))]
        public IHttpActionResult DeleteCustomImage(int id)
        {
            CustomImage customImage = db.CustomImages.Find(id);
            if (customImage == null)
            {
                return NotFound();
            }

            db.CustomImages.Remove(customImage);
            db.SaveChanges();

            return Ok(customImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomImageExists(int id)
        {
            return db.CustomImages.Count(e => e.ImageId == id) > 0;
        }
    }
}