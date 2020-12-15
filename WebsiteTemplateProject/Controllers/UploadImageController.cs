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
    public class UploadImageController : ApiController
    {
        

        [HttpPost]
        [Route("api/UploadImage")]

        public HttpResponseMessage UploadImage()
        {
           
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            var postedFormPageId = httpRequest.Form["pageId"];
            var postedFormColumnId = httpRequest.Form["columnId"];


            var postedFormUrl = httpRequest.Form["imageUrl"];
           // var postedBackgroundImage = httpRequest.Form["backgroundImage"];
            var postedFile = httpRequest.Files["imageUrl"];
            var postedUrl400px = httpRequest.Form["body"];


            if (postedFormUrl != null && postedFile == null)
            {
                var fireBaseUrl = postedFormUrl;
                var pageId = Int32.Parse(postedFormPageId);
                var columnId = Int32.Parse(postedFormColumnId);
                //var bgImage = postedBackgroundImage;

             

               

                //Save to DB
                using (NewWebContent1 db = new NewWebContent1))
                {
                    Models.WebContent uploadImage = new Models.WebContent()
                    {
                        //ImageUrl = fireBaseUrl,
                        PageId = pageId,
                        ColumnId = columnId,
                    


                      backgroundImage = fireBaseUrl
                  
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

            if (postedUrl400px != null && postedFormUrl == null)
            {
                var fireBaseUrl = postedUrl400px;
                var pageId = Int32.Parse(postedFormPageId);
                var columnId = Int32.Parse(postedFormColumnId);
                //var bgImage = postedBackgroundImage;

                //Save to DB
                using (NewWebContent1 db = new NewWebContent1())
                {
                    Models.WebContent uploadImage = new Models.WebContent()
                    {
                        //ImageUrl = fireBaseUrl,
                        PageId = pageId,
                        ColumnId = columnId,



                        Body = fireBaseUrl

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

        

        //Download Image file

        [HttpGet]
        [Route("api/GetImage")]

        public HttpResponseMessage GetImage(string image)
        {
            //Create HTTP Response.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            //Set the File Path.
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/ Images /") + image + ".PNG";
            //Check whether file exists.
            if (!File.Exists(filePath))
            {
                //throw 404 (Not Found) eception if File not found.
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", image);
                throw new HttpResponseException(response);
            }

            //Read the File in a Byte Array.
            byte[] bytes = File.ReadAllBytes(filePath);

            //Set the Response Content.
            response.Content = new ByteArrayContent(bytes);

            //Set the reponse content length.

            response.Content.Headers.ContentLength = bytes.LongLength;

            //Set the Content Disposition Header Value and FileName.

            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = image + ".PNG";
            //Set the file Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(image + ".PNG"));
            return response;
            
        }

    }
}
