﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using WebsiteTemplateProject.Models;
using System.Web.UI.WebControls;
using System.Data.Entity.Validation;
using WebsiteTemplateProject.Validation;

namespace WebsiteTemplateProject.Service
{

    public interface IWebContentService
    {
        WebsiteTemplateProject.Models.WebContent UpsertWebContent(WebsiteTemplateProject.Models.WebContent webContent);
    }


    public class WebContentService
    {
        public readonly GlobalContentDBEntities _context;

        public WebContentService(GlobalContentDBEntities context)
        {
            _context = context;
        }

        public WebContentService()
        {

        }

        public WebsiteTemplateProject.Models.WebContent UpsertWebContent(WebsiteTemplateProject.Models.WebContent webContent, GlobalContentDBEntities db)
        {
            using (db)
            {



                if (webContent.Id == default(int))
                {
                    //Default settings when first creating text
                    if (webContent.TextBody != null)
                    {
                        webContent.textMobile = webContent.TextBody;
                        webContent.textTablet = webContent.TextBody;
                        webContent.textLaptop = webContent.TextBody;

                        webContent.textAlign = "center";
                    }

                    //Default settings when first creating background image


                    db.WebContents.Add(webContent);
                }
                else
                {
                   
                    db.Entry(webContent).State = EntityState.Modified;
                }

                try
                {

                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    var newException = new FormattedDbEntityValidationException(e);
                    throw newException;
                }

             
                return webContent;
            }
        }

       

        
        public List<Models.WebContent> GetWebContentByPageId(int pageId, GlobalContentDBEntities db)
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

        public List<Models.WebContent> GetWebContentById(int id, GlobalContentDBEntities db)
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

        //Get all content by column array method here

        public List<Models.WebContent> GetContentByColumnId(int columnId, GlobalContentDBEntities db)
        {
            List<Models.WebContent> contentByColumnId = new List<Models.WebContent>();


            //Ignore any content that is tied to a subpage id

            foreach (var webContent in db.WebContents)
            {
                
                    contentByColumnId.Add(webContent);

                

            }

            foreach (var webContent in contentByColumnId.ToList())
            {
               
                    if (webContent.ColumnId!= columnId)
                    {
                        contentByColumnId.Remove(webContent);
                    }

             

            }


            return contentByColumnId.OrderBy(x => x.Id).ToList();
        }

        public List<List<Models.WebContent>> GetContentVMLists(int columnId, GlobalContentDBEntities db)
        {
            List<List<Models.WebContent>> contentVmList = new List<List<Models.WebContent>>();

            List<Models.WebContent> contentByColumnId = new List<Models.WebContent>();

          



            foreach (var content in db.WebContents)
            {
                if (content.ColumnId == columnId)
                {
                contentByColumnId.Add(content);

                }

            }

            

            contentVmList.Add(contentByColumnId);


            return contentVmList;
        }

    }
}