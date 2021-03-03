using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;

namespace WebsiteTemplateProject.Service
{
    public class SubPageByClientUrlService
    {
        public CustomPage UpsertSubPage(CustomPage customPage, NewClientWebPagesDBEntities db)
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

        public List<CustomPage> GetSubNavLinks(NewClientWebPagesDBEntities db, int id)
        {

            List<CustomPage> subPagesByParentId = new List<CustomPage>();

            using (db)
            {
                foreach (var page in db.CustomPages)
                {
                    if (page.ParentId == id)
                    {
                        subPagesByParentId.Add(page);
                    }
                }


                return subPagesByParentId.ToList();
            }
        }
    }
}