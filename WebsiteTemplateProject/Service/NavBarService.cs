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
    public List<NavBar> GetAllNavbarsByClientUrl(string clientUrl, ClientNavbarDBEntities2 db)
    {
        //Write method to grab all pages from the db that are attached to the clientUrl

        List<NavBar> navbarsByClientList = new List<NavBar>();

        foreach (var navbar in db.NavBars)
        {
            if (navbar.ClientUrl == clientUrl)
            {
                navbarsByClientList.Add(navbar);
            }
        }
        return navbarsByClientList;
    }

        public NavBar UpsertNavBarDataByClientUrl(NavBar navBarData, ClientNavbarDBEntities2 db)
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