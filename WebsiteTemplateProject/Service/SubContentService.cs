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
        public readonly NewWebContent1 _context;

        public SubContentService(NewWebContent1 context)
        {
            _context = context;
        }

        public SubContentService()
        {

        }

        public List<Models.WebContent> GetWebContentByPageIdAndSubPageId(int pageId, int subPageId, NewWebContent1 db)
        {
            List<Models.WebContent> webContentByPageIdAndSubId = new List<Models.WebContent>();

            //First adds all content to list
            foreach (var webContent in db.WebContents)
            {
                webContentByPageIdAndSubId.Add(webContent);
            }




            //Removes any content that doesn't match both page id and sub page id
            foreach (var webContent in webContentByPageIdAndSubId.ToList())
            {
                //Remove any content that doesn't have a page id or sub id
                if (webContent.PageId == null || webContent.SubPageId == null)
                {
                    webContentByPageIdAndSubId.Remove(webContent);
                }


                //If page or subpage id is not null
                if (webContent.PageId != null && webContent.SubPageId != null)
                {
                    //Remove content if pageid doesn't equal page id
                    if (webContent.PageId != pageId)
                    {
                        webContentByPageIdAndSubId.Remove(webContent);

                    }
                    //Remove content if pageid doesn't equal page id

                    if (webContent.SubPageId != subPageId)
                    {
                        webContentByPageIdAndSubId.Remove(webContent);

                    }

                }

            }

            //Return list ordered by Id
            return webContentByPageIdAndSubId.OrderBy(x => x.Id).ToList();
        }

        public WebsiteTemplateProject.Models.WebContent UpsertSubContent(Models.WebContent webContent, NewWebContent1 db)
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