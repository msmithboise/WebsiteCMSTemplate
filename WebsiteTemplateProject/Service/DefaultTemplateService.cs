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
          var newRow =  createRow(newPageId);

            //take id from that row
            var newRowId = newRow.RowId;

            //pass it to create a column
            var newColumn = createColumn(newRowId);

            //take column id

            //create new content and tie column id to it
        }

        public Row createRow(int? newPageId)
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

        public Column createColumn(int newRowId)
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
    }
}





        

      
    
