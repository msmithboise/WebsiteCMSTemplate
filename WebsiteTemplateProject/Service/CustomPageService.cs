using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;
using System.Data.Entity;

namespace WebsiteTemplateProject.Service
{

    public interface ICustomPageService
    {
       CustomPage UpsertCustomPage(CustomPage customPage);
    }


    public class CustomPageService
    {
        public readonly WebsiteDBCustomPageEntities _context;

        public CustomPageService(WebsiteDBCustomPageEntities context)
        {
            _context = context;
        }

        public CustomPageService()
        {

        }

        public CustomPage UpsertCustomPage(CustomPage customPage, WebsiteDBCustomPageEntities db)
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