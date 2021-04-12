using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using WebsiteTemplateProject.Models;
using System.Data.Entity.Migrations.Model;

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

            //pass it to create a 12 size column
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

            //Create 1 row with 3 col size-4

            var rowThree = CreateRow(newPageId);

            var rowThreeId = rowThree.RowId;

            //Create the 3 col size 4

            var leftHeaderCol = CreateFourSizeColumn(rowThreeId);
            var leftHeaderColId = leftHeaderCol.ColumnId;

            var midHeaderCol = CreateFourSizeColumn(rowThreeId);
            var midHeaderColId = midHeaderCol.ColumnId;

            var rightHeaderCol = CreateFourSizeColumn(rowThreeId);
            var rightHeaderColId = rightHeaderCol.ColumnId;

            //Add header to middle header column

            CreateHeaderForMidColumn(midHeaderColId);

            //Add a new row
            var rowFour = CreateRow(newPageId);

            //Add 3 col size 4 columns

            var leftTextCol = CreateFourSizeColumn(rowThreeId);
            var leftTextColId = leftTextCol.ColumnId;

            var midTextCol = CreateFourSizeColumn(rowThreeId);
            var midTextColId = midTextCol.ColumnId;

            var rightTextCol = CreateFourSizeColumn(rowThreeId);
            var rightTextColId = rightTextCol.ColumnId;

            //Add text to mid column

            CreateTextForMidColumn(midTextColId);

            //Add new row

            var rowFive = CreateRow(newPageId);
            var rowFiveId = rowFive.RowId;

            //Add a column12 

            var newColumnThree = CreateColumn(rowFiveId);
            var newColumnThreeId = newColumnThree.ColumnId;

            //Add image to column

            CreateHalfPageHeroImage(newColumnThreeId);

            //Add a new row

            var rowSix = CreateRow(newPageId);
            var rowSixId = rowSix.RowId;

            //Create 3 col-4

            var leftHeaderColTwo = CreateFourSizeColumn(rowSixId);
            var leftHeaderColIdTwo = leftHeaderColTwo.ColumnId;

            var midHeaderColTwo = CreateFourSizeColumn(rowSixId);
            var midHeaderColIdTwo = midHeaderColTwo.ColumnId;

            var rightHeaderColTwo = CreateFourSizeColumn(rowSixId);
            var rightHeaderColIdTwo = rightHeaderColTwo.ColumnId;

            CreateHeaderForLeftColumnTwo(leftHeaderColIdTwo);
            CreateHeaderForMidColumnTwo(midHeaderColIdTwo);
            CreateHeaderForRightColumnTwo(rightHeaderColIdTwo);

            //Create new row

            var rowSeven = CreateRow(newPageId);
            var rowSevenId = rowSeven.RowId;

            //Add 3 col size 4 to row

            var leftTextColTwo = CreateFourSizeColumn(rowSevenId);
            var leftTextColIdTwo = leftTextColTwo.ColumnId;

            var midTextColTwo = CreateFourSizeColumn(rowSevenId);
            var midTextColIdTwo = midTextColTwo.ColumnId;

            var rightTextColTwo = CreateFourSizeColumn(rowSevenId);
            var rightTextColIdTwo = rightTextColTwo.ColumnId;

            //Add text to new columns

            CreateTextForLeftColumnTwo(leftTextColIdTwo);
            CreateTextForMidColumnTwo(midTextColIdTwo);
            CreateTextForRightColumnTwo(rightTextColIdTwo);

            //Create new row

            var rowEight = CreateRow(newPageId);
            var rowEightId = rowEight.RowId;

            //Create 4 col-3

            var leftImageCol = CreateThreeSizeColumn(rowEightId);
            var leftImageColId = leftImageCol.ColumnId;

            var leftMidImageCol = CreateThreeSizeColumn(rowEightId);
            var leftMidImageColId = leftMidImageCol.ColumnId;

            var rightMidImageCol = CreateThreeSizeColumn(rowEightId);
            var rightMidImageColId = rightMidImageCol.ColumnId;

            var rightImageCol = CreateThreeSizeColumn(rowEightId);
            var rightImageColId = rightImageCol.ColumnId;

            //Add new image to each col

            CreateImageForLeftColumn(leftImageColId);
            CreateImageForLeftMidColumn(leftMidImageColId);
            CreateImageForRightMidColumn(rightMidImageColId);
            CreateImageForRightColumn(rightImageColId);








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
            NewColumnDBEntities2 db = new NewColumnDBEntities2();

            using (db)
            {
                if (column.ColumnId == default(int))
                {
                    column.ColumnClass = "col-12";
                    column.ColumnClassMobile = "col-12";
                    column.ColumnClassLaptop = "col-12";
                    column.ColumnClassTablet = "col-12";
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

        public Column CreateFourSizeColumn(int newRowId)
        {
            Column column = new Column();
            NewColumnDBEntities2 db = new NewColumnDBEntities2();

            using (db)
            {
                if (column.ColumnId == default(int))
                {
                    column.ColumnClass = "col-4";
                    column.ColumnClassMobile = "col-12";
                    column.ColumnClassLaptop = "col-4";
                    column.ColumnClassTablet = "col-4";
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

        public Column CreateThreeSizeColumn(int newRowId)
        {
            Column column = new Column();
            NewColumnDBEntities2 db = new NewColumnDBEntities2();

            using (db)
            {
                if (column.ColumnId == default(int))
                {
                    column.ColumnClass = "col-3";
                    column.ColumnClassMobile = "col-12";
                    column.ColumnClassLaptop = "col-3";
                    column.ColumnClassTablet = "col-3";
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
                    //blue orange
                    webContent.backgroundImage = "https://images.unsplash.com/photo-1494253109108-2e30c049369b?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80";
                    
                    //palm trees
                    //webContent.backgroundImage = "https://images.unsplash.com/photo-1601012608788-21e9c7c0661b?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1340&q=80";


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

                    //SETTING Global

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
                    //Set font family
                    webContent.fontFamily = "Work Sans";

                    //SETTING MOBILE
                    webContent.textMobile = "Your site name here";
                    webContent.positionMobile = "absolute";
                    webContent.leftMobile = "18%";
                    webContent.bottomMobile = "350px";
                    webContent.fontSizeMobile = "40px";

                    //SETTING TABLET
                    webContent.textTablet = "Your site name here";
                    webContent.positionTablet = "absolute";
                    webContent.leftTablet = "25%";
                    webContent.bottomTablet = "250px";
                    webContent.fontSizeTablet = "60px";


                    //SETTING LAPTOP
                    webContent.textLaptop = "Your site name here";
                    webContent.positionLaptop = "absolute";
                    webContent.leftLaptop = "30%";
                    webContent.bottomLaptop = "300px";
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

        public void CreateHeaderForMidColumn(int midHeaderColId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {
                    webContent.ColumnId = midHeaderColId;

                    //SETTING Global

                    //Set text
                    webContent.TextBody = "This is your header";
                   
                    //Font color
                    webContent.color = "#222222";
                    //Set font size:
                    webContent.fontSize = "25px";
                    //Set font family
                    webContent.fontFamily = "Work Sans";
                    //Set padding
                    webContent.padding = "15px";
                    //Set text align
                    webContent.textAlign = "center";
                    //Set font wieght
                    webContent.fontWieght = "bold";


                    //SETTING MOBILE

                    webContent.textMobile = "This is your header";
                    webContent.fontSizeMobile = "25px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignMobile = "center";
                    

                    //SETTING TABLET

                    webContent.textTablet = "This is your header";

                    webContent.fontSizeTablet = "25px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignTablet = "center";
                    webContent.paddingMobile = "15px";
                    webContent.paddingTablet = "15px";
                    webContent.paddingLaptop = "15px";


                    //SETTING LAPTOP

                    webContent.textLaptop = "This is your header";

                    webContent.fontSizeLaptop = "25px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignLaptop = "center";

                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }

        public void CreateHeaderForLeftColumnTwo(int midHeaderColId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {
                    webContent.ColumnId = midHeaderColId;

                    //SETTING Global

                    //Set text
                    webContent.TextBody = "Left Header";

                    //Font color
                    webContent.color = "#222222";
                    //Set font size:
                    webContent.fontSize = "25px";
                    //Set font family
                    webContent.fontFamily = "Work Sans";
                    //Set padding
                    webContent.padding = "15px";
                    //Set text align
                    webContent.textAlign = "center";
                    // Set font wieght
                    webContent.fontWieght = "bold";


                    webContent.paddingTablet = "15px";
                    webContent.paddingLaptop = "15px";

                    //SETTING MOBILE

                    webContent.textMobile = "";

                    webContent.fontSizeMobile = "25px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignMobile = "center";

                    //SETTING TABLET

                    webContent.textTablet = "Left Header";

                    webContent.fontSizeTablet = "25px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignTablet = "center";


                    //SETTING LAPTOP

                    webContent.textLaptop = "Left Header";

                    webContent.fontSizeLaptop = "25px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignLaptop = "center";

                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }

        public void CreateHeaderForMidColumnTwo(int midHeaderColId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {
                    webContent.ColumnId = midHeaderColId;

                    //SETTING Global

                    webContent.paddingMobile = "15px";
                    webContent.paddingTablet = "15px";
                    webContent.paddingLaptop = "15px";

                    //Set text
                    webContent.TextBody = "Middle Header";

                    //Font color
                    webContent.color = "#222222";
                    //Set font size:
                    webContent.fontSize = "25px";
                    //Set font family
                    webContent.fontFamily = "Work Sans";
                    //Set padding
                    webContent.padding = "30px";
                    //Set text align
                    webContent.textAlign = "center";
                    //Set font wieght
                    webContent.fontWieght = "bold";


                    //SETTING MOBILE

                    webContent.textMobile = "Another Middle Header";

                    webContent.fontSizeMobile = "25px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignMobile = "center";

                    //SETTING TABLET

                    webContent.textTablet = "Middle Header";

                    webContent.fontSizeTablet = "25px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignTablet = "center";


                    //SETTING LAPTOP

                    webContent.textLaptop = "Middle Header";

                    webContent.fontSizeLaptop = "25px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignLaptop = "center";
                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }

        public void CreateHeaderForRightColumnTwo(int midHeaderColId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {
                    webContent.ColumnId = midHeaderColId;

                    //SETTING Global

                    //Set text
                    webContent.TextBody = "Right Header";

                    //Font color
                    webContent.color = "#222222";
                    //Set font size:
                    webContent.fontSize = "25px";
                    //Set font family
                    webContent.fontFamily = "Work Sans";
                    //Set padding
                    webContent.padding = "15px";
                    //Set text align
                    webContent.textAlign = "center";
                  
                    webContent.paddingTablet = "30px";
                    webContent.paddingLaptop = "30px";

                    //Set font wieght
                    webContent.fontWieght = "bold";


                    //SETTING MOBILE

                    webContent.textMobile = "";

                    webContent.fontSizeMobile = "25px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignMobile = "center";

                    //SETTING TABLET

                    webContent.textTablet = "Right Header";

                    webContent.fontSizeTablet = "25px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignTablet = "center";


                    //SETTING LAPTOP

                    webContent.textLaptop= "Right Header";

                    webContent.fontSizeLaptop = "25px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignLaptop = "center";

                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }

        public void CreateTextForMidColumn(int midHeaderColId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {
                    webContent.ColumnId = midHeaderColId;

                    //SETTING Global

                    //Set text
                    webContent.TextBody = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

                    //Font color
                    webContent.color = "#222222";
                    //Set font size:
                    webContent.fontSize = "18px";
                    //Set font family
                    webContent.fontFamily = "Work Sans";
                    //Set padding
                    webContent.padding = "15px";
                    //Set text align
                    webContent.textAlign = "center";
                    webContent.paddingMobile = "15px";
                    webContent.paddingTablet = "15px";
                    webContent.paddingLaptop = "15px";


                    //SETTING MOBILE

                    webContent.textMobile = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
                    webContent.fontSizeMobile = "18px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignMobile = "center";

                    //SETTING TABLET

                    webContent.textTablet = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
                    webContent.fontSizeTablet = "18px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignTablet = "center";

                    //SETTING LAPTOP

                    webContent.textLaptop = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
                    webContent.fontSizeLaptop = "18px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignLaptop = "center";

                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }

        public void CreateHalfPageHeroImage(int newColumnId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {

                    webContent.ColumnId = newColumnId;

                    //set background image
                    
                    webContent.backgroundImage = "https://images.unsplash.com/photo-1505739998589-00fc191ce01d?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80";

                    //set height to 500px

                    webContent.height = "400px";
                    
                    webContent.animationName = "";


                    //SETTING MOBILE
                    webContent.heightMobile = "400px";

                    db.WebContents.Add(webContent);

                    //SETTING LAPTOP
                    webContent.heightLaptop = "400px";

                    //SETTING TABLET
                    webContent.heightTablet = "400px";
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }

        public void CreateTextForLeftColumnTwo(int colId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {
                    webContent.ColumnId = colId;

                    //SETTING Global

                    //Set text
                    webContent.TextBody = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

                    //Font color
                    webContent.color = "#222222";
                    //Set font size:
                    webContent.fontSize = "18px";
                    //Set font family
                    webContent.fontFamily = "Work Sans";
                    //Set padding
                    webContent.padding = "15px";
                    //Set text align
                    webContent.textAlign = "center";
                    webContent.paddingMobile = "30px";
                    webContent.paddingTablet = "30px";
                    webContent.paddingLaptop = "30px";


                    //SETTING MOBILE

                    webContent.textMobile = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
                    webContent.fontSizeMobile = "18px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignMobile = "center";

                    //SETTING TABLET

                    webContent.textTablet = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
                    webContent.fontSizeTablet = "18px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignTablet = "center";


                    //SETTING LAPTOP

                    webContent.textLaptop = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
                    webContent.fontSizeLaptop = "18px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignLaptop = "center";

                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }

        public void CreateTextForMidColumnTwo(int colId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {
                    webContent.ColumnId = colId;

                    //SETTING Global

                    //Set text
                    webContent.TextBody = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

                    //Font color
                    webContent.color = "#222222";
                    //Set font size:
                    webContent.fontSize = "18px";
                    //Set font family
                    webContent.fontFamily = "Work Sans";
                    //Set padding
                    webContent.padding = "15px";
                    //Set text align
                    webContent.textAlign = "center";
                    webContent.paddingMobile = "15px";
                    webContent.paddingTablet = "15px";
                    webContent.paddingLaptop = "15px";


                    //SETTING MOBILE

                    webContent.textMobile = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
                    webContent.fontSizeMobile = "18px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignMobile = "center";

                    //SETTING TABLET

                    webContent.textTablet = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
                    webContent.fontSizeTablet = "18px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignTablet = "center";


                    //SETTING LAPTOP

                    webContent.textLaptop = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
                    webContent.fontSizeLaptop = "18px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignLaptop = "center";

                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }

        public void CreateTextForRightColumnTwo(int colId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {
                    webContent.ColumnId = colId;

                    //SETTING Global

                    //Set text
                    webContent.TextBody = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

                    //Font color
                    webContent.color = "#222222";
                    //Set font size:
                    webContent.fontSize = "18px";
                    //Set font family
                    webContent.fontFamily = "Work Sans";
                    //Set padding
                    webContent.padding = "15px";
                    //Set text align
                    webContent.textAlign = "center";
                    webContent.paddingMobile = "15px";
                    webContent.paddingTablet = "15px";
                    webContent.paddingLaptop = "15px";


                    //SETTING MOBILE

                    webContent.textMobile= "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
                    webContent.fontSizeMobile = "18px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignMobile = "center";

                    //SETTING TABLET

                    webContent.textTablet= "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
                    webContent.fontSizeTablet = "18px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignTablet = "center";


                    //SETTING LAPTOP

                    webContent.textLaptop = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
                    webContent.fontSizeLaptop = "18px";
                    webContent.fontFamily = "Work Sans";
                    webContent.textAlignLaptop = "center";

                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }

        public void CreateImageForLeftColumn(int colId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {

                    webContent.ColumnId = colId;

                    //set background image
                    //blue orange
                    webContent.backgroundImage = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80";
                    webContent.animationName = "pulse";
                    webContent.HyperLink = "https://www.google.com/";

                   


                    //Set image height to 1000px
                    webContent.height = "400px";
                    webContent.width = "400px";

                    


                    //SETTING MOBILE
                    

                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }

        public void CreateImageForLeftMidColumn(int colId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {

                    webContent.ColumnId = colId;

                    //set background image
                    //blue orange
                    webContent.backgroundImage = "https://images.unsplash.com/photo-1561641377-f7456d23aa9b?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1226&q=80";
                    webContent.animationName = "pulse";
                    webContent.HyperLink = "https://www.google.com/";



                    //Set image height to 1000px
                    webContent.height = "400px";
                    webContent.width = "400px";

                    


                    //SETTING MOBILE


                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }

        public void CreateImageForRightMidColumn(int colId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {

                    webContent.ColumnId = colId;

                    //set background image
                    //blue orange
                    webContent.backgroundImage = "https://images.unsplash.com/photo-1494252713559-f26b4bf0b174?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1350&q=80";
                    webContent.animationName = "pulse";
                    webContent.HyperLink = "https://www.google.com/";



                    //Set image height to 1000px
                    webContent.height = "400px";
                    webContent.width = "400px";

               


                    //SETTING MOBILE


                    db.WebContents.Add(webContent);
                }
                else
                {
                    db.Entry(webContent).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }

        public void CreateImageForRightColumn(int colId)
        {
            Models.WebContent webContent = new Models.WebContent();
            GlobalContentDBEntities db = new GlobalContentDBEntities();

            using (db)
            {
                if (webContent.Id == default(int))
                {

                    webContent.ColumnId = colId;

                    //set background image
                    //blue orange
                    webContent.backgroundImage = "https://images.unsplash.com/photo-1517816428104-797678c7cf0c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1350&q=80";
                    webContent.animationName = "pulse";
                    webContent.HyperLink = "https://www.google.com/";



                    //Set image height to 1000px
                    webContent.height = "400px";
                    webContent.width = "400px";

                    


                    //SETTING MOBILE


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





        

      
    
