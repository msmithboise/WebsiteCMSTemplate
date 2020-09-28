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
            foreach (var u in db.Users.Where(x => x.Username == user.Username && x.Hash == user.Hash))
            {
              

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
    }
}