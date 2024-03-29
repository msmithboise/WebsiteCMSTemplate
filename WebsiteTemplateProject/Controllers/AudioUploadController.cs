﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebsiteTemplateProject.Models;
using WebsiteTemplateProject.Validation;

namespace WebsiteTemplateProject.Controllers
{
    public class AudioUploadController : ApiController
    {
    [HttpPost]
    [Route("api/UploadAudio")]
    public HttpResponseMessage UploadAudio()
        {
            var httpRequest = HttpContext.Current.Request;
            //Upload Audio
            var postedFormPageId = httpRequest.Form["pageId"];

            var postedFormColumnId = httpRequest.Form["columnId"];
        
            var postedFormUrl = httpRequest.Form["audioUrl"];
           
            var postedFile = httpRequest.Files["audioUrl"];

            if (postedFormUrl != null && postedFile == null)
            {
                var fireBaseUrl = postedFormUrl;
                var pageId = Int32.Parse(postedFormPageId);
                var columnId = Int32.Parse(postedFormColumnId);




                //Save to DB
                using (NewWebContent1 db = new NewWebContent1())
                {
                    Models.WebContent uploadAudio = new Models.WebContent()
                    {
                        AudioUrl = fireBaseUrl,
                        PageId = pageId,
                        ColumnId = columnId,
                      
                       
                      
                    };
                    db.WebContents.Add(uploadAudio);

                    try
                    {

                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        var newException = new FormattedDbEntityValidationException(e);
                        throw newException;
                    }


                }

            }





            return Request.CreateResponse(HttpStatusCode.Created);
        }

    }
}
