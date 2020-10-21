using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;

namespace WebsiteTemplateProject.Service
{
    public class SubContentService
    {
        public readonly ContentDBEntities _context;

        public SubContentService(ContentDBEntities context)
        {
            _context = context;
        }

        public SubContentService()
        {

        }

        public WebsiteTemplateProject.Models.WebContent UpsertSubContent(Models.WebContent webContent, ContentDBEntities db)
        {
            using (db)
            {
                if (webContent != null)
                {

                    if (webContent.Id == default(int))
                    {
                        db.WebContents.Add(webContent);
                    }
                    else
                    {
                        db.Entry(webContent).State = EntityState.Modified;
                    }


                    db.SaveChanges();
                    return webContent;
                }
                else
                {
                    return webContent;
                }
            }
        }
    }
}