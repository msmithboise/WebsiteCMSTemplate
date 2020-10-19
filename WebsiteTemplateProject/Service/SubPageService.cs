using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;
using System.Data.Entity;

namespace WebsiteTemplateProject.Service
{
    public interface ISubPageService
    {
        SubPage UpsertSubPage(SubPage customPage);
    }


    public class SubPageService
    {
        public readonly SubPageDBEntities _context;

        public SubPageService(SubPageDBEntities context)
        {
            _context = context;
        }

        public SubPageService()
        {

        }

        public SubPage UpsertSubPage(SubPage subPage, SubPageDBEntities db)
        {
            using (db)
            {
                if (subPage.SubPageId == default(int))
                {
                    db.SubPages.Add(subPage);
                }
                else
                {
                    db.Entry(subPage).State = EntityState.Modified;
                }

                db.SaveChanges();
                return subPage;
            }
        }

        public List<SubPage> GetSubPagesByPageId(int pageId, SubPageDBEntities db)
        {
            List<SubPage> subPagesByPageId = new List<SubPage>();

            foreach (var subPage in db.SubPages)
            {
                subPagesByPageId.Add(subPage);
            }

            foreach (var subPage in subPagesByPageId.ToList())
            {
                if (subPage.PageId != pageId)
                {
                    subPagesByPageId.Remove(subPage);
                }
            }


            return subPagesByPageId.OrderBy(x => x.PageId).ToList();
        }
    }
}