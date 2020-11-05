using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebsiteTemplateProject.Models;

namespace WebsiteTemplateProject.Service
{
    public class WebStructureService
    {

        public Row UpsertRows(Row row, RowDBEntities db)
        {
            using (db)
            {



                if (row.RowId == default(int))
                {
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

        public List<Row> GetRowsByPageId(int pageId, RowDBEntities db)
        {
            List<Row> rowsByPageId = new List<Row>();


            //Add all rows to new list
            foreach (var row in db.Rows)
            {
               
                
                    rowsByPageId.Add(row);

                

            }

            // filter out list of rows
            foreach (var row in rowsByPageId.ToList())
            {
               
                    if (row.PageId != pageId)
                    {
                        rowsByPageId.Remove(row);
                    }

                

            }


            return rowsByPageId.OrderBy(x => x.Id).ToList();
        }
    }
}