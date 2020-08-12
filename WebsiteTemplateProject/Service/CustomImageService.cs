using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;
using System.Data.Entity;

namespace WebsiteTemplateProject.Service
{

    public interface ICustomImageService
    {
        CustomImage UpsertCustomImage(CustomImage customImage);
    }


    public class CustomImageService
    {
        public readonly CustomImageDBEntities2 _context;

        public CustomImageService(CustomImageDBEntities2 context)
        {
            _context = context;
        }

        public CustomImageService()
        {

        }

        public CustomImage UpsertCustomImage(CustomImage customImage, CustomImageDBEntities2 db)
        {
            using (db)
            {
                if (customImage.ImageId == default(int))
                {
                    db.CustomImages.Add(customImage);
                }
                else
                {
                    db.Entry(customImage).State = EntityState.Modified;
                }

                db.SaveChanges();
                return customImage;
            }
        }

        public List<CustomImage>  GetImagesByPageId(int pageId, CustomImageDBEntities2 db, List<CustomImage> customImagesList)
        {
            List<CustomImage> customImagesByPageId = new List<CustomImage>();

            foreach (var image in db.CustomImages)
            {
                customImagesByPageId.Add(image);
            }

            foreach (var image in customImagesByPageId.ToList())
            {
                if (image.PageId != pageId)
                {
                    customImagesByPageId.Remove(image);
                }
            }

            return customImagesByPageId;
        }
    }
}