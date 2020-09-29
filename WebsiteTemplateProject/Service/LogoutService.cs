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

    public interface ILogoutService
    {
       
    }


    public class LogoutService
    {
        public readonly MyUsersDBEntities _context;
 
      

        public LogoutService(MyUsersDBEntities context)
        {
            _context = context;
        }

        public LogoutService()
        {

        }

        public User PostDataOnLogout(User user, MyUsersDBEntities db)
        {

            UserService userService = new UserService();
            foreach (var u in db.Users.Where(x => x.Username == user.Username))
            {



                user.EmailAddress = u.EmailAddress;
                    user.isPasswordHashed = true;
                    user.Id = u.Id;
                    user.FirstName = u.FirstName;
                    user.LastName = u.LastName;
                    user.Organization = u.Organization;
                    user.Salt = u.Salt;
                    user.Hash = u.Hash;
                    user.isLoggedIn = false;
                    user.Token = "test";
                    user.timeLoggedOut = DateTime.Now;


                    db.Set<User>().AddOrUpdate(user);
                
            }
            db.SaveChanges();
            return user;
        }

        public loggedInUser PostDataForLoggedOutUser(User user, MyUsersDBEntities db, LoggedInUserDBEntities loggedInUserDb)
        {

            var loggedInUserToReturn = new loggedInUser();

            List<loggedInUser> loggedInUsersList = new List<loggedInUser>();

            loggedInUsersList = loggedInUserDb.loggedInUsers.ToList();

            if (loggedInUsersList.Count <= 0)
            {
                loggedInUserToReturn = new loggedInUser();

                loggedInUserToReturn.loggedInUserId = user.Id;
                loggedInUserToReturn.UserName = user.Username;
                loggedInUserToReturn.Hash = DateTime.Now.ToString();
                loggedInUserToReturn.IsLoggedIn = false;


            }
            else
            {

                foreach (var loggedInUser in loggedInUserDb.loggedInUsers)
                {

                    loggedInUser.UserName = user.Username;
                    loggedInUser.Hash = DateTime.Now.ToString();
                    loggedInUser.IsLoggedIn = false;

                    loggedInUserToReturn = loggedInUser;
                }

            }

            loggedInUserDb.Set<loggedInUser>().AddOrUpdate(loggedInUserToReturn);

            loggedInUserDb.SaveChanges();
            return loggedInUserToReturn;
        }
    }
}