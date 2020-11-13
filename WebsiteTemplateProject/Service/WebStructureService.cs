using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebsiteTemplateProject.Models;

namespace WebsiteTemplateProject.Service
{
    public class WebStructureService
    {

        public Row UpsertRows(Row row, NewRowDBEntities db)
        {
            using (db)
            {



                if (row.RowId == default(int))
                {
                    row.RowClass = "row";
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

        public List<Row> GetRowsByPageId(int pageId, NewRowDBEntities db)
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

        //Upsert Columns

        public Column UpsertColumns(Column column, ColumnDBEntities db)
        {
            using (db)
            {
                if (column.ColumnId == default(int))
                {
                    column.ColumnClass = "col-" + column.ColumnClass;
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

        //Get columns by row id

        public List<Column> GetColumnsByRowId(int rowId, ColumnDBEntities db)
        {
            List<Column> columnsByRowId = new List<Column>();

            //Add all rows to new list
            foreach (var column in db.Columns)
            {
                columnsByRowId.Add(column);
            }

            // filter out list of rows
            foreach (var column in columnsByRowId.ToList())
            {
                if (column.RowId != rowId)
                {
                    columnsByRowId.Remove(column);
                }
            }

            return columnsByRowId.OrderBy(x => x.Id).ToList();
        }

        //Get columns by VM list

        public List<List<Column>> GetColumnVMLists(int rowId, ColumnDBEntities db)
        {
            ColumnVmList columnVMList = new ColumnVmList();
            List<Column> columnsByRowId = new List<Column>();
            List<Column> listToFilter = new List<Column>();

            //First add all columns to two arrays. columnsByRowId and Listtofilter

            foreach (var column in db.Columns)
            {
                listToFilter.Add(column);
            }

            foreach (var column in listToFilter.ToList())
            {
                columnsByRowId.Add(column);

                listToFilter.Remove(column); //20 - 1 is removed
            }

            //when nothing is left to filter, the columns get added to the return list

            List<List<Column>> columnVmList = new List<List<Column>>();
            
            columnVmList.Add(columnsByRowId);

            return columnVmList;
        }
    }
}