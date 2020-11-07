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
    public class ColumnsController : ApiController
    {
        private ColumnDBEntities db = new ColumnDBEntities();

        // GET: api/Columns
        public IQueryable<Column> GetColumns()
        {
            return db.Columns;
        }

        // GET: api/Columns/5
        [ResponseType(typeof(Column))]
        public IHttpActionResult GetColumn(int id)
        {
            WebStructureService webStructureService = new WebStructureService();

            var content = webStructureService.GetColumnsByRowId(id, db);

            return Ok(content);
        }

        // PUT: api/Columns/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutColumn(int id, Column column)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != column.ColumnId)
            {
                return BadRequest();
            }

            db.Entry(column).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColumnExists(id))
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

        // POST: api/Columns
        [ResponseType(typeof(Column))]
        public IHttpActionResult PostColumn(Column column)
        {
            WebStructureService webStructureService = new WebStructureService();

            webStructureService.UpsertColumns(column, db);

            return CreatedAtRoute("DefaultApi", new { id = column.ColumnId }, column);
        }

        // DELETE: api/Columns/5
        [ResponseType(typeof(Column))]
        public IHttpActionResult DeleteColumn(int id)
        {
            Column column = db.Columns.Find(id);
            if (column == null)
            {
                return NotFound();
            }

            db.Columns.Remove(column);
            db.SaveChanges();

            return Ok(column);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ColumnExists(int id)
        {
            return db.Columns.Count(e => e.ColumnId == id) > 0;
        }
    }
}