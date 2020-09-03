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
    public class CustomTextController : ApiController
    {
        private CustomTextDBEntities2 db = new CustomTextDBEntities2();
        private CustomImageDBEntities2 dbImage = new CustomImageDBEntities2();

        // GET: api/CustomText
        public IQueryable<CustomText> GetCustomTexts()
        {
            

            return db.CustomTexts;
        }

        // GET: api/CustomText/5
        [ResponseType(typeof(CustomText))]
        public IHttpActionResult GetCustomText(int id)
        {
            CustomTextService customTextservice = new CustomTextService();

            List<CustomText> customTextByPageId = new List<CustomText>();

          var content =  customTextservice.getImagesAndText(db, dbImage,id );

            //customTextByPageId = customTextservice.GetTextByPageId(id, db, customTextByPageId);

            return Ok(content);
        }

        // PUT: api/CustomText/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomText(int id, CustomText customText)
        {
            CustomTextService customTextService = new CustomTextService();

            customTextService.UpsertCustomText(customText, db);

            return CreatedAtRoute("DefaultApi", new { id = customText.TextId }, customText);
        }

        // POST: api/CustomText
        [ResponseType(typeof(CustomText))]
        public IHttpActionResult PostCustomText(CustomText customText)
        {
            CustomTextService customTextService = new CustomTextService();

            customTextService.UpsertCustomText(customText, db);

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