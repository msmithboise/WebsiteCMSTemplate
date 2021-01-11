using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebsiteTemplateProject.Models;
using WebsiteTemplateProject.Service;

namespace WebsiteTemplateProject.Controllers
{
    public class PagesByClientController : ApiController
    {

        public NewCustomPagesDBEntities db = new NewCustomPagesDBEntities();
        // GET: api/PagesByClient
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //[ResponseType(typeof(CustomPage))]
        //public IHttpActionResult GetCustomPage(int id)
        //{
        //    CustomPage customPage = db.CustomPages.Find(id);
        //    if (customPage == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(customPage);
        //}

        // GET: api/PagesByClient/5
        [ResponseType(typeof(CustomPage))]
        public IHttpActionResult GetPagesByClient(string id, NewCustomPagesDBEntities db)
        {
            CustomPageService customPageService = new CustomPageService();

           // var content = webContentService.GetWebContentByPageId(id, db);

            var pages = customPageService.GetAllPagesByClientUrl(id, db);



            return Ok(pages);
        }

        // POST: api/PagesByClient
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PagesByClient/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PagesByClient/5
        public void Delete(int id)
        {
        }
    }
}
