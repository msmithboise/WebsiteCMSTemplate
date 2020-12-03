using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;
using System.Data.Entity;
using System.Web.UI;

namespace WebsiteTemplateProject.Service
{

    public interface ICustomPageService
    {
       CustomPage UpsertCustomPage(CustomPage customPage);
    }


    public class CustomPageService
    {
        public readonly  PagesDBEntities _context;

        public CustomPageService(PagesDBEntities context)
        {
            _context = context;
        }

        public CustomPageService()
        {

        }

        public CustomPage UpsertCustomPage(CustomPage customPage, PagesDBEntities db)
        {
            using (db)
            {
                if (customPage.PageId == default(int))
                {
                    db.CustomPages.Add(customPage);
                }
                else
                {
                    db.Entry(customPage).State = EntityState.Modified;
                }

                db.SaveChanges();
                return customPage;
            }
        }
    }
}