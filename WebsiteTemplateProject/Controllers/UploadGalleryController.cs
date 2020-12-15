using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WebsiteTemplateProject.Models;
using WebsiteTemplateProject.Validation;

namespace WebsiteTemplateProject.Controllers
{
    public class UploadGalleryController : ApiController
    {


        [HttpPost]
        [Route("api/UploadGallery")]

        public HttpResponseMessage UploadImage()
        {

            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            var postedFormPageId = httpRequest.Form["pageId"];
            var postedFormColumnId = httpRequest.Form["columnId"];


            var postedFormUrl = httpRequest.Form["imageUrl"];

            var postedFile = httpRequest.Files["imageUrl"];



            if (postedFormUrl != null && postedFile == null)
            {
                var fireBaseUrl = postedFormUrl;
                var pageId = Int32.Parse(postedFormPageId);
                var columnId = Int32.Parse(postedFormColumnId);

                //Save to DB
                using (NewWebContent1 db = new NewWebContent1())
                {
                    Models.WebContent uploadImage = new Models.WebContent()
                    {
                        ImageUrl = fireBaseUrl,
                        PageId = pageId,
                        ColumnId = columnId,
                        width = "400",
                        height = "400"

                    };
                    db.WebContents.Add(uploadImage);

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

