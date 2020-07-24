using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using WebsiteTemplateProject.Models;

namespace WebsiteTemplateProject.Service
{
    public interface ITextBoxOneService
    {
        TextBox UpsertTextBoxOne(TextBox textBoxOne);
    }


    public class TextBoxOneService
    {
        public readonly TextBoxOneDBEntities _context;

        public TextBoxOneService(TextBoxOneDBEntities context)
        {
            _context = context;
        }

        public TextBoxOneService()
        {

        }


        public TextBox UpsertTextBoxOne(TextBox textBox, TextBoxOneDBEntities db)
        {
            using (db)
            {
                if(textBox.Id == default(int))
                {
                    db.TextBoxes.Add(textBox);
                }
                else
                {
                    db.Entry(textBox).State = EntityState.Modified;
                }

                db.SaveChanges();
                return textBox;
            }
        }


    }
}