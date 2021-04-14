using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;
using WebsiteTemplateProject.Validation;

namespace WebsiteTemplateProject.Service
{
    public class DashboardPresetService
    {
        public readonly DashboardDBEntities2 _context;

        public DashboardPresetService(DashboardDBEntities2 context)
        {
            _context = context;
        }

        public DashboardPresetService()
        {

        }

        public WebsiteTemplateProject.Models.Dashboard UpsertDashboardPresets(WebsiteTemplateProject.Models.Dashboard dashboard, DashboardDBEntities2 db)
        {
            using (db)
            {



                if (dashboard.presetId == default(int))
                {
                    db.Dashboards.Add(dashboard);
                }
                else
                {

                    db.Entry(dashboard).State = EntityState.Modified;
                }

                try
                {

                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    var newException = new FormattedDbEntityValidationException(e);
                    throw newException;
                }


                return dashboard;
            }
        }
    }
}