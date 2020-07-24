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
    public class TextBoxOneController : ApiController
    {
        private TextBoxOneDBEntities db = new TextBoxOneDBEntities();

        // GET: api/TextBoxOne
        public IQueryable<TextBox> GetTextBoxes()
        {
            return db.TextBoxes;
        }

        // GET: api/TextBoxOne/5
        [ResponseType(typeof(TextBox))]
        public IHttpActionResult GetTextBox(int id)
        {
            TextBox textBox = db.TextBoxes.Find(id);
            if (textBox == null)
            {
                return NotFound();
            }

            return Ok(textBox);
        }

        // PUT: api/TextBoxOne/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTextBox(int id, TextBox textBox)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != textBox.Id)
            {
                return BadRequest();
            }

            db.Entry(textBox).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TextBoxExists(id))
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

        // POST: api/TextBoxOne
        [ResponseType(typeof(TextBox))]
        public IHttpActionResult PostTextBox(TextBox textBox)
        {
            TextBoxOneService textBoxOneService = new TextBoxOneService();

            textBoxOneService.UpsertTextBoxOne(textBox, db);

           

            return CreatedAtRoute("DefaultApi", new { id = textBox.Id }, textBox);
        }

        // DELETE: api/TextBoxOne/5
        [ResponseType(typeof(TextBox))]
        public IHttpActionResult DeleteTextBox(int id)
        {
            TextBox textBox = db.TextBoxes.Find(id);
            if (textBox == null)
            {
                return NotFound();
            }

            db.TextBoxes.Remove(textBox);
            db.SaveChanges();

            return Ok(textBox);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TextBoxExists(int id)
        {
            return db.TextBoxes.Count(e => e.Id == id) > 0;
        }
    }
}