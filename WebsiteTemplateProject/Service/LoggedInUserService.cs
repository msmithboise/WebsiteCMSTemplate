using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using WebsiteTemplateProject.Models;
using System.Text;
using System.Data.Entity.Validation;
using WebsiteTemplateProject.Validation;
using System.Data.Entity.Migrations;
using Microsoft.Owin.Security.OAuth;

namespace WebsiteTemplateProject.Service
{

    public interface ILoggedInUserService
    {

    }


    public class LoggedInUserService
    {
        public readonly LoggedInUserDBEntities _context;
        public MyUsersDBEntities userDb;



        public LoggedInUserService(LoggedInUserDBEntities context)
        {
            _context = context;
        }

        public LoggedInUserService()
        {

        }

        public loggedInUser PostDataForCurrentUserOnLogin(loggedInUser user, LoggedInUserDBEntities db, MyUsersDBEntities userDb)
        {


            
            db.SaveChanges();
            return user;
        }
    }
}