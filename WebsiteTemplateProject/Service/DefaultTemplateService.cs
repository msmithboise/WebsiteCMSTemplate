using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using WebsiteTemplateProject.Models;

namespace WebsiteTemplateProject.Service
{

    public class DefaultTemplateService
    {
        
        public void CreateTemplate(Models.WebContent webContent)
        {
            var newPageId = webContent.PageId;

            //Add a new row
          var newRow =  CreateRow(newPageId);

            //take id from that row
            var newRowId = newRow.RowId;

            //pass it to create a column
            var newColumn = CreateColumn(newRowId);

            //take column id
            var newColumnId = newColumn.ColumnId;

            //create new content and tie column id to it

            CreateHeroImage(newColumnId);

            //Create second row

            var newRowTwo = CreateRow(newPageId);

            //take id from that row
            var newRowTwoId = newRowTwo.RowId;

            //pass it to create a column
            var newColumnTwo = CreateColumn(newRowTwoId);

            //take column id
            var newColumnTwoId = newColumnTwo.ColumnId;

            CreateHeroImageText(newColumnTwoId);



        }

        public Row CreateRow(int? newPageId)
        {
            Row row = new Row();
            NewRowDBEntities db = new NewRowDBEntities();

            using (db)
            {
                if (row.RowId == default(int))
                {
                    row.RowClass = "row";
                    row.PageId = newPageId;
                    db.Rows.Add(row);
                }
                else
                {
                    db.Entry(row).State = EntityState.Modified;
                }

                db.SaveChanges();
                return row;
            }
        }

        public Column CreateColumn(int newRowId)
        {
            Column column = new Column();
            ColumnDBEntities db = new ColumnDBEntities();

            using (db)
            {
                if (column.ColumnId == default(int))
                {
                    column.ColumnClass = "col-12";
                    column.RowId = newRowId;
                    db.Columns.Add(column);
                }
                else
                {
                    db.Entry(column).State = EntityState.Modified;
                }

                db.SaveChanges();
                return column;
            }
        }

        public void CreateHeroImage(int newColumnId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {

                    webContent.ColumnId = newColumnId;

                    //set background image
                    webContent.backgroundImage = "https://images.unsplash.com/photo-1494253109108-2e30c049369b?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80";
                    //Set image height to 1000px
                    webContent.height = "1000px";
                    webContent.animationName = "";


                    //SETTING MOBILE
                    webContent.heightMobile = "800px";

                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();
                
            }
        }

        public void CreateHeroImageText(int newColumnId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {
                    webContent.ColumnId = newColumnId;

                    //SETTING DESKTOP

                    //Set text
                    webContent.TextBody = "Your site name here";
                    //Set position: absolute
                    webContent.position = "absolute";
                    //Set left 30%
                    webContent.left = "30%";
                    //Font color
                    webContent.color = "#f6f6f6";
                    //Set font size:
                    webContent.fontSize = "90px";
                    //Set bottom to 500px
                    webContent.bottom = "500px";

                    //SETTING MOBILE

                    webContent.positionMobile = "absolute";
                    webContent.leftMobile = "20%";
                    webContent.bottomMobile = "350px";
                    webContent.fontSizeMobile = "40px";

                    //SETTING TABLET
                    webContent.positionTablet = "absolute";
                    webContent.leftTablet = "25%";
                    webContent.bottomTablet = "200px";
                    webContent.fontSizeTablet = "60px";


                    //SETTING LAPTOP
                    webContent.positionLaptop = "absolute";
                    webContent.leftLaptop = "30%";
                    webContent.bottomLaptop = "150px";
                    webContent.fontSizeLaptop = "60px";

                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }
    }
}





        

      
    
