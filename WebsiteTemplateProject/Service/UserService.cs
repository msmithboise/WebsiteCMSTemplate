using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using WebsiteTemplateProject.Models;
using System.Text;
using System.Data.Entity.Validation;
using WebsiteTemplateProject.Validation;

namespace WebsiteTemplateProject.Service
{

    public interface IUserService
    {
        User UpsertUserData(User webContent);
    }


    public class UserService
    {
        public readonly NewUserDbEntities _context;

        public UserService(NewUserDbEntities context)
        {
            _context = context;
        }

        public UserService()
        {

        }

        public User UpsertWebContent(User user, NewUserDbEntities db)
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

                encryptPassword(user, db);

                try
                {

                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    var newException = new FormattedDbEntityValidationException(e);
                    throw newException;
                }


                return user;
            }
        }

        private void encryptPassword(User user, NewUserDbEntities db)
        {
            string salt = CreateSalt(10);
            string hashedPassword = CreateHash(user.Hash, salt, user);
        }


        public string CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);


            return Convert.ToBase64String(buff);

        }

        public string CreateHash(string password, string salt, User user)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            user.Salt = salt;
            password = BtyeArrayToHexString(hash);
            user.Hash = password;

            return password;

            
        }

        public static string BtyeArrayToHexString(byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder(byteArray.Length);
            foreach (byte b in byteArray)
            {
                hex.AppendFormat("{0}", b);

            }
                return hex.ToString();
        }

    }
}