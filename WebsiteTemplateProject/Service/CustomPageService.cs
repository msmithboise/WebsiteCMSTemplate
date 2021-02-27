using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;
using System.Data.Entity;
using System.Web.UI;
using System.Web.Http.Results;

namespace WebsiteTemplateProject.Service
{

    public interface ICustomPageService
    {
       CustomPage UpsertCustomPage(CustomPage customPage);
    }


    public class CustomPageService
    {
        public readonly  NewClientWebPagesDBEntities _context;

        public CustomPageService(NewClientWebPagesDBEntities context)
        {
            _context = context;
        }

        public CustomPageService()
        {

        }

        public CustomPage UpsertCustomPage(CustomPage customPage, NewClientWebPagesDBEntities db)
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

        public List<CustomPage> GetAllPagesByClientUrl(string clientUrl, NewClientWebPagesDBEntities db)
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
            return pageByClientList.OrderByDescending(x => x.PageDescription == "Home").ThenBy(x => x.PageId).ToList();
        }
    }
}