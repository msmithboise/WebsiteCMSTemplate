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
    public class RowController : ApiController
    {
        private RowDBEntities db = new RowDBEntities();

        // GET: api/Row
        public IQueryable<Row> GetRows()
        {
            return db.Rows;
        }

        // GET: api/Row/5
        [ResponseType(typeof(Row))]
        public IHttpActionResult GetRow(int id)
        {
            Row row = db.Rows.Find(id);
            if (row == null)
            {
                return NotFound();
            }

            return Ok(row);
        }

        // PUT: api/Row/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRow(int id, Row row)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != row.RowId)
            {
                return BadRequest();
            }

            db.Entry(row).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RowExists(id))
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

        // POST: api/Row
        [ResponseType(typeof(Row))]
        public IHttpActionResult PostRow(Row row)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rows.Add(row);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RowExists(row.RowId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = row.RowId }, row);
        }

        // DELETE: api/Row/5
        [ResponseType(typeof(Row))]
        public IHttpActionResult DeleteRow(int id)
        {
            Row row = db.Rows.Find(id);
            if (row == null)
            {
                return NotFound();
            }

            db.Rows.Remove(row);
            db.SaveChanges();

            return Ok(row);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RowExists(int id)
        {
            return db.Rows.Count(e => e.RowId == id) > 0;
        }
    }
}