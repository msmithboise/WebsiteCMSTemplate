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

        public List<ViewModel> getImagesAndText(CustomTextDBEntities2 dbText, CustomImageDBEntities2 dbImage, int pageId)
        {

            //create method that will create a dictionary combining both tables from custom image and custom text and retrieve it by page id. 
            //our goal is to have our client side import it as a whole so that it can be iterated through and displayed to the page, this will
            //hopefully allow the all the content added, be displayed in that order

            //Side note:  A good way to keep all content in order is to time stamp each creation, then use an order by time stamp

            //Step one:  Start by combining the tables into one dictionary

           
                var orderedViewModelList = new List<ViewModel>();

            //Creating a list of all custom text by page id to later add to tuple
            using (dbText)
            {
                List<CustomText> customTextByPageId = new List<CustomText>();


                //add text tables to new list
                foreach (var text in dbText.CustomTexts)
                {
                    customTextByPageId.Add(text);
                }

                //Removes any text linked to a page id that isn't the specified one
                foreach (var text in customTextByPageId.ToList())
                {
                    if (text.PageId != pageId)
                    {
                        customTextByPageId.Remove(text);
                    }
                }

                //Create viewModel list

                List<ViewModel> viewModelList = new List<ViewModel>();

                //Add text list to viewModel list

                viewModelList.Add(new ViewModel()
                {
                    ViewModelIdentifier = customTextByPageId.First().TextId,
                    ViewModelObject = customTextByPageId
                });

                //Creating a list of all custom images by page id to later add to tuple
                using (dbImage)
                {
                    List<CustomImage> customImagesByPageId = new List<CustomImage>();

                    foreach (var image in dbImage.CustomImages)
                    {
                        customImagesByPageId.Add(image);
                    }

                    //Removes any image linked to a page id that isn't the specified one
                    foreach (var image in customImagesByPageId.ToList())
                    {
                        if (image.PageId != pageId)
                        {
                            customImagesByPageId.Remove(image);
                        }
                    }
                    //Add images list to viewModel list

                    viewModelList.Add(new ViewModel()
                    {
                        ViewModelIdentifier = customImagesByPageId.First().ImageId,
                        ViewModelObject = customImagesByPageId
                });

                    orderedViewModelList = viewModelList.OrderBy(x => x.ViewModelIdentifier).ToList();

                    //place to break

                    var stringValue = string.Empty;



                    
                }

            }
            

                return orderedViewModelList;
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