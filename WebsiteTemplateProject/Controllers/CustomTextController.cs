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
    public class CustomTextController : ApiController
    {
        private CustomTextDBEntities2 db = new CustomTextDBEntities2();

        // GET: api/CustomText
        public IQueryable<CustomText> GetCustomTexts()
        {
            return db.CustomTexts;
        }

        // GET: api/CustomText/5
        [ResponseType(typeof(CustomText))]
        public IHttpActionResult GetCustomText(int id)
        {
            CustomText customText = db.CustomTexts.Find(id);
            if (customText == null)
            {
                return NotFound();
            }

            return Ok(customText);
        }

        // PUT: api/CustomText/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomText(int id, CustomText customText)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customText.TextId)
            {
                return BadRequest();
            }

            db.Entry(customText).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomTextExists(id))
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

        // POST: api/CustomText
        [ResponseType(typeof(CustomText))]
        public IHttpActionResult PostCustomText(CustomText customText)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomTexts.Add(customText);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customText.TextId }, customText);
        }

        // DELETE: api/CustomText/5
        [ResponseType(typeof(CustomText))]
        public IHttpActionResult DeleteCustomText(int id)
        {
            CustomText customText = db.CustomTexts.Find(id);
            if (customText == null)
            {
                return NotFound();
            }

            db.CustomTexts.Remove(customText);
            db.SaveChanges();

            return Ok(customText);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomTextExists(int id)
        {
            return db.CustomTexts.Count(e => e.TextId == id) > 0;
        }
    }
}