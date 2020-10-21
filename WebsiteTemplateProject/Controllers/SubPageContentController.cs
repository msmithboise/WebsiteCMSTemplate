using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteTemplateProject.Controllers
{
   
    public class SubPageContentController : Controller
    {
    private NewWebContentEntities2 db = new NewWebContentEntities2();

        [HttpGet]
        // GET: SubPageContent
        public ActionResult Index()
        {
            // To open a connection to the database 
            using (db)
            {
                var data = db.WebContents.ToList(); // Return the list of data from the database
                return View(data);
            }
        }



        [HttpPost]
        public ActionResult Create(WebContent content, string pageId, string subPageId)
        {

            // To open a connection to the database 
            using (var context = new NewWebContentEntities2())
            {
                // Add data to the particular table 
                context.WebContents.Add(content);

                // save the changes 
                context.SaveChanges();
            }

            return View();
          
        }
    }
}