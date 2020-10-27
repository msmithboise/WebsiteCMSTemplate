using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;

namespace WebsiteTemplateProject.Service
{
    public class NavBarService
    {
        public NavBar UpsertNavBarData(NavBar navBarData, NavBarDBEntities db)
        {
            using (db)
            {
                if (navBarData.NavBarId == default(int))
                {
                    db.NavBars.Add(navBarData);
                }
                else
                {
                    db.Entry(navBarData).State = EntityState.Modified;
                }

                db.SaveChanges();
                return navBarData;
            }
        }
    }
}