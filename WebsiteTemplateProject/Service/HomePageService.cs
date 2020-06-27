using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;
using System.Data.Entity;

namespace WebsiteTemplateProject.Service
{

    public interface IHomePageService
    {
        HomePage UpsertHomePage(HomePage homePage);
    }


    public class HomePageService
    {
    public readonly HomePageDBEntities1 _context;

    public HomePageService(HomePageDBEntities1 context)
    {
        _context = context;
    }

    public HomePageService()
        {

        }

        public HomePage UpsertHomePage(HomePage homePage, HomePageDBEntities1 db)
        {
            using (db)
            {
                if (homePage.UserID == default(int))
                {
                    db.HomePages.Add(homePage);
                }
                else
                {
                    db.Entry(homePage).State = EntityState.Modified;
                }

                db.SaveChanges();
                return homePage;
            }
        }
    }
}