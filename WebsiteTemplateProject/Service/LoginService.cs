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

    public interface ILoginService
    {
        User UpsertUserData(User webContent);
    }


    public class LoginService
    {
        public readonly MyUsersDBEntities _context;
        public bool isLoggedIn = false;
        public bool isPasswordHashed = false;
        public UserService userService = new UserService();

        public LoginService(MyUsersDBEntities context)
        {
            _context = context;
        }

        public LoginService()
        {

        }

        public User PostDataOnLogin(User user, MyUsersDBEntities db)
        {

            UserService userService = new UserService();
            foreach (var u in db.Users.Where(x => x.Username == user.Username && x.Hash == user.Hash))
            {
                if (u.Username == user.Username && u.Hash == user.Hash)
                {

                    user.Username = u.Username;
                    user.EmailAddress = u.EmailAddress;

                    if (user.isPasswordHashed == true)
                    {
                        var password = user.Hash;
                        var salt = u.Salt;
                        var hash = u.Hash;


                    }
                    else
                    {
                        
                    user.Hash = userService.encryptPassword(u, db);
                    }

                    user.isPasswordHashed = true;
                    user.Id = u.Id;
                    user.FirstName = u.FirstName;
                    user.LastName = u.LastName;
                    user.Organization = u.Organization;
                    user.Salt = u.Salt;
                    u.isLoggedIn = true;
                    user.isLoggedIn = u.isLoggedIn;
                    user.Token = "test";
                    user.timeLoggedIn = DateTime.Now;


                    

                    db.Set<User>().AddOrUpdate(user);




                }
            }
            db.SaveChanges();
            return user;
        }


        //Need to write method that takes the salt from the data base, and the hash that was saved and on login, have 
        //an algorythm that brings them together, this means, the rng to create the salt is fine, but no more rng after that

        public string reEncryptPassword(string password, string salt, string hash, User user )
        {

            var newHash = userService.CreateHash(password, salt, user );



            return newHash;
        }

    }
}