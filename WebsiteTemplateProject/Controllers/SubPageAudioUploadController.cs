using System;
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
    public class SubPageAudioUploadController : ApiController
    {
        [HttpPost]
        [Route("api/SubPageAudioUpload")]
        public HttpResponseMessage UploadAudio()
        {
            var httpRequest = HttpContext.Current.Request;
            //Upload Audio
            var postedFormPageId = httpRequest.Form["pageId"];
            var postedFormSubPageId = httpRequest.Form["subPageId"];

            var postedFormUrl = httpRequest.Form["audioUrl"];

            var postedFile = httpRequest.Files["audioUrl"];

            if (postedFormUrl != null && postedFile == null)
            {
                var fireBaseUrl = postedFormUrl;
                var pageId = Int32.Parse(postedFormPageId);
                var subPageId = Int32.Parse(postedFormSubPageId);






                //Save to DB
                using (NewWebContent1 db = new NewWebContent1())
                {
                    Models.WebContent uploadAudio = new Models.WebContent()
                    {
                        AudioUrl = fireBaseUrl,
                        PageId = pageId,
                        SubPageId = subPageId



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
