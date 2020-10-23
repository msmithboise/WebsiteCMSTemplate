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
        public readonly WebUserDBEntities _context;

        public WebContentService(WebUserDBEntities context)
        {
            _context = context;
        }

        public WebContentService()
        {

        }

        public WebsiteTemplateProject.Models.WebContent UpsertWebContent(WebsiteTemplateProject.Models.WebContent webContent, WebUserDBEntities db)
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

        



        public List<Models.WebContent> GetWebContentByPageId(int pageId, WebUserDBEntities db)
        {
            List<Models.WebContent> webContentByPageId = new List<Models.WebContent>();


            //Ignore any content that is tied to a subpage id

            foreach (var webContent in db.WebContents)
            {
                if (webContent.SubPageId == null)
                {
                webContentByPageId.Add(webContent);

                }

            }

            foreach (var webContent in webContentByPageId.ToList())
            {
                //Ignore any content that is tied to a subpage id
                if (webContent.SubPageId == null)
                {
                if (webContent.PageId != pageId)
                {
                    webContentByPageId.Remove(webContent);
                }

                }

            }


            return webContentByPageId.OrderBy(x => x.Id).ToList();
        }

        public List<Models.WebContent> GetWebContentById(int id, WebUserDBEntities db)
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