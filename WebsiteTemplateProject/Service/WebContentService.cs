using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;
using System.Data.Entity;

namespace WebsiteTemplateProject.Service
{

    public interface IWebContentService
    {
        WebContent UpsertWebContent(WebContent webContent);
    }


    public class WebContentService
    {
        public readonly WebContentDBEntities _context;

        public WebContentService(WebContentDBEntities context)
        {
            _context = context;
        }

        public WebContentService()
        {

        }

        public WebContent UpsertWebContent(WebContent webContent, WebContentDBEntities db)
        {
            using (db)
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
        }

       

        public List<WebContent> GetWebContentByPageId(int pageId, WebContentDBEntities db)
        {
            List<WebContent> webContentByPageId = new List<WebContent>();

            foreach (var webContent in db.WebContents)
            {
                webContentByPageId.Add(webContent);
            }

            foreach (var webContent in webContentByPageId.ToList())
            {
                if (webContent.PageId != pageId)
                {
                    webContentByPageId.Remove(webContent);
                }
            }


            return webContentByPageId.OrderBy(x => x.Id).ToList();
        }
    }
}