using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using WebsiteTemplateProject.Models;

namespace WebsiteTemplateProject.Service
{

    public interface IUserService
    {
        User UpsertUserData(User webContent);
    }


    public class UserService
    {
        public readonly UsersDBEntities _context;

        public UserService(UsersDBEntities context)
        {
            _context = context;
        }

        public UserService()
        {

        }

        public User UpsertWebContent(User user, UsersDBEntities db)
        {
            using (db)
            {
                if (user.Id == default(int))
                {
                    db.Users.Add(user);
                }
                else
                {
                    db.Entry(user).State = EntityState.Modified;
                }

                db.SaveChanges();
                return user;
            }
        }
    }
}