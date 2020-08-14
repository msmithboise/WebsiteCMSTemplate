using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;
using System.Data.Entity;

namespace WebsiteTemplateProject.Service
{

    public interface ICustomTextService
    {
        CustomText UpsertCustomText(CustomText customText);
    }


    public class CustomTextService
    {
        public readonly CustomTextDBEntities2 _context;

        public CustomTextService(CustomTextDBEntities2 context)
        {
            _context = context;
        }

        public CustomTextService()
        {

        }

        public CustomText UpsertCustomText(CustomText customText, CustomTextDBEntities2 db)
        {
            using (db)
            {
                if (customText.TextId == default(int))
                {
                    db.CustomTexts.Add(customText);
                }
                else
                {
                    db.Entry(customText).State = EntityState.Modified;
                }

                db.SaveChanges();
                return customText;
            }
        }

        public List<CustomText> GetTextByPageId(int pageId, CustomTextDBEntities2 db, List<CustomText> customTextList)
        {
            List<CustomText> customTextByPageId = new List<CustomText>();

            foreach (var image in db.CustomTexts)
            {
                customTextByPageId.Add(image);
            }

            foreach (var image in customTextByPageId.ToList())
            {
                if (image.PageId != pageId)
                {
                    customTextByPageId.Remove(image);
                }
            }

            return customTextByPageId;
        }
    }
}