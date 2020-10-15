using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace WebsiteTemplateProject.Service
{

    public interface IWebContentService
    {
        WebContent UpsertWebContent(WebContent webContent);
    }


    public class WebContentService
    {
        public readonly NewWebContentEntities2 _context;

        public WebContentService(NewWebContentEntities2 context)
        {
            _context = context;
        }

        public WebContentService()
        {

        }

        public WebContent UpsertWebContent(WebContent webContent, NewWebContentEntities2 db)
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

        



        public List<WebContent> GetWebContentByPageId(int pageId, NewWebContentEntities2 db)
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

        public List<WebContent> GetWebContentById(int id, NewWebContentEntities2 db)
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