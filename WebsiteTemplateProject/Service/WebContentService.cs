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
        public readonly MyContentDBEntities _context;

        public WebContentService(MyContentDBEntities context)
        {
            _context = context;
        }

        public WebContentService()
        {

        }

        public WebContent UpsertWebContent(WebContent webContent, MyContentDBEntities db)
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

       

        public List<WebContent> GetWebContentByPageId(int pageId, MyContentDBEntities db)
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

        public List<WebContent> GetWebContentById(int id, MyContentDBEntities db)
        {
            List<WebContent> webContentById = new List<WebContent>();

            foreach (var webContent in db.WebContents)
            {
                webContentById.Add(webContent);
            }

            foreach (var webContent in webContentById.ToList())
            {
                if (webContent.Id != id)
                {
                    webContentById.Remove(webContent);
                }
            }
            return webContentById.OrderBy(x => x.Id).ToList();
        }

    }
}