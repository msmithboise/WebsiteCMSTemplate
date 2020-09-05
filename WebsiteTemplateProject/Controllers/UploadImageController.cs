using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            string imageName = null;
            var httpRequest = HttpContext.Current.Request;
            //Upload Image

            var postedFile = httpRequest.Files["Image"];
            //Create custom filename
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Images/" + imageName);
            postedFile.SaveAs(filePath);

            //Save to DB
            using (UploadImageDBEntities db = new UploadImageDBEntities())
            {
                UploadImage uploadImage = new UploadImage()
                {
                    ImageName = imageName
                };
                db.UploadImages.Add(uploadImage);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.Created);




        }
    }
}
