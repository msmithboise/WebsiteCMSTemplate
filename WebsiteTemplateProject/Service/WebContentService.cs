using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using WebsiteTemplateProject.Models;

namespace WebsiteTemplateProject.Service
{

    public interface IWebContentService
    {
        WebsiteTemplateProject.Models.WebContent UpsertWebContent(WebsiteTemplateProject.Models.WebContent webContent);
    }


    public class WebContentService
    {
        public readonly ContentDBEntities _context;

        public WebContentService(ContentDBEntities context)
        {
            _context = context;
        }

        public WebContentService()
        {

        }

        public WebsiteTemplateProject.Models.WebContent UpsertWebContent(WebsiteTemplateProject.Models.WebContent webContent, ContentDBEntities db)
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

        



        public List<Models.WebContent> GetWebContentByPageId(int pageId, ContentDBEntities db)
        {
            List<Models.WebContent> webContentByPageId = new List<Models.WebContent>();

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

        public List<Models.WebContent> GetWebContentById(int id, ContentDBEntities db)
        {
            List<Models.WebContent> webContentById = new List<Models.WebContent>();

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