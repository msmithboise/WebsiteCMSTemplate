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
        public readonly  NewCustomPagesDBEntities _context;

        public CustomPageService(NewCustomPagesDBEntities context)
        {
            _context = context;
        }

        public CustomPageService()
        {

        }

        public CustomPage UpsertCustomPage(CustomPage customPage, NewCustomPagesDBEntities db)
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

        public List<CustomPage> GetAllPagesByClientUrl(string clientUrl, NewCustomPagesDBEntities db)
        {
            //Write method to grab all pages from the db that are attached to the clientUrl

            List<CustomPage> pageByClientList = new List<CustomPage>();

            foreach (var page in db.CustomPages)
            {
                if (page.ClientUrl == clientUrl)
                {
                    pageByClientList.Add(page);
                }
            }
            return pageByClientList;
        }
    }
}