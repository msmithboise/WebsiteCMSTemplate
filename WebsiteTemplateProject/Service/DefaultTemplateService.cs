using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace WebsiteTemplateProject.Service
{

    public interface IDefaultTemplateService
    {
        WebContent UpsertDefaultTemplate(WebContent webContent);
    }


    public class DefaultTemplateService
    {
        public readonly NewWebContentEntities2 _context;

        public DefaultTemplateService(NewWebContentEntities2 context)
        {
            _context = context;
        }

        public DefaultTemplateService()
        {

        }

        public WebContent CreateTemplate(WebContent webContent, WebsiteTemplateProject.Models.NewWebContent1 db)
        {
            //Add a new row




            return webContent;
        }

        //public WebContent CreateHeroImage(WebContent webContent, NewWebContentEntities2 db)
        //{


        //    webContent.ImageUrl = "https://images.unsplash.com/photo-1600867317504-3ea8cff32509?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1364&q=80";
        //    webContent.PageId = 1;


        //    db.WebContents.Add(webContent);

        //    db.SaveChanges();
        //    return webContent;
        //}


        //public WebContent CreateTextHeader(WebContent wc, NewWebContentEntities2 db)
        //{
        //    wc.ImageUrl = "";

        //    wc.PageId = 1;
        //    wc.TextBody = "www.test.com";
        //    wc.color = "blue";


        //    db.WebContents.Add(wc);

        //    db.SaveChanges();
        //    return wc;
        //}
    }
}





        

      
    
