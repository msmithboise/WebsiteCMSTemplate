using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WebsiteTemplateProject.Models;

namespace WebsiteTemplateProject.Controllers
{
    public class UploadImageController : ApiController
    {
        [HttpPost]
        [Route("api/UploadImage")]

        public HttpResponseMessage UploadImage()
        {
            string imageUrl = null;
            var httpRequest = HttpContext.Current.Request;
            //Upload Image

            var postedFile = httpRequest.Files["Image"];
            //Create custom filename
            imageUrl = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageUrl = imageUrl + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Images/" + imageUrl);
            postedFile.SaveAs(filePath);

            //Save to DB
            using (MyContentDBEntities db = new MyContentDBEntities())
            {
                WebContent uploadImage = new WebContent()
                {
                    ImageUrl = imageUrl
                };
                db.WebContents.Add(uploadImage);
                db.SaveChanges();
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
