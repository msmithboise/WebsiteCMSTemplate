﻿using System;
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
    public class RowController : ApiController
    {
        private NewRowDBEntities db = new NewRowDBEntities();

        // GET: api/Row
        public IQueryable<Row> GetRows()
        {
            return db.Rows;
        }

        // GET: api/Row/5
        [ResponseType(typeof(Row))]
        public IHttpActionResult GetRow(int id)
        {
            WebStructureService webStructureService = new WebStructureService();

            var content = webStructureService.GetRowsByPageId(id, db);

            return Ok(content);
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
            WebStructureService webStructureService = new WebStructureService();

           webStructureService.UpsertRows(row, db);

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