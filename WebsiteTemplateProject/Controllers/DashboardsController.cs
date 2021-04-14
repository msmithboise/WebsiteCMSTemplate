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
    public class DashboardsController : ApiController
    {
        private DashboardDBEntities2 db = new DashboardDBEntities2();

        // GET: api/Dashboards
        public IQueryable<Dashboard> GetDashboards()
        {
            return db.Dashboards;
        }

        // GET: api/Dashboards/5
        [ResponseType(typeof(Dashboard))]
        public IHttpActionResult GetDashboard(int id)
        {
            Dashboard dashboard = db.Dashboards.Find(id);
            if (dashboard == null)
            {
                return NotFound();
            }

            return Ok(dashboard);
        }

        // PUT: api/Dashboards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDashboard(int id, Dashboard dashboard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dashboard.presetId)
            {
                return BadRequest();
            }

            db.Entry(dashboard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DashboardExists(id))
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

        // POST: api/WebContent
        [ResponseType(typeof(WebContent))]
        //public IHttpActionResult PostWebContent(Models.WebContent webContent)
        //{
        //    WebContentService webContentService = new WebContentService();

        //    webContentService.UpsertWebContent(webContent, db);

        //    return CreatedAtRoute("DefaultApi", new { id = webContent.Id }, webContent);
        //}

        // POST: api/Dashboards
        [ResponseType(typeof(Dashboard))]
        public IHttpActionResult PostDashboard(Dashboard dashboard)
        {
            DashboardPresetService dashboardPresetService = new DashboardPresetService();

            dashboardPresetService.UpsertDashboardPresets(dashboard,db);

            return CreatedAtRoute("DefaultApi", new { id = dashboard.presetId }, dashboard);
        }

        // DELETE: api/Dashboards/5
        [ResponseType(typeof(Dashboard))]
        public IHttpActionResult DeleteDashboard(int id)
        {
            Dashboard dashboard = db.Dashboards.Find(id);
            if (dashboard == null)
            {
                return NotFound();
            }

            db.Dashboards.Remove(dashboard);
            db.SaveChanges();

            return Ok(dashboard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DashboardExists(int id)
        {
            return db.Dashboards.Count(e => e.presetId == id) > 0;
        }
    }
}