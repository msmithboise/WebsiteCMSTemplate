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

namespace WebsiteTemplateProject.Service
{

    public interface IUserService
    {
        User UpsertUserData(User webContent);
    }


    public class UserService
    {
        public readonly MyUsersDBEntities _context;
        public bool isLoggedIn = false;
        public bool isPasswordHashed = false;

        public UserService(MyUsersDBEntities context)
        {
            _context = context;
        }

        public UserService()
        {

        }

        public User UpsertWebContent(User user, MyUsersDBEntities db)
        {

            //If user is logging on after already being registered and already has a set Id, this will encrypt password, and post isLoggedIn to be true

           
                    




            using (db)
            {
                if (user.Id == default(int))
                {
                    db.Users.Add(user);
                }
                //else
                //{
                //    db.Entry(user).State = EntityState.Modified;
                //}

               // encryptPassword(user, db);

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

        public string encryptPassword(User user, MyUsersDBEntities db)
        {
            string salt = CreateSalt(10);
            string hashedPassword = CreateHash(user.Hash, salt, user);

            return hashedPassword;
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
            password = ByteArrayToHexString(hash);
            user.Hash = password;

            return password;

            
        }

        public static string ByteArrayToHexString(byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder(byteArray.Length * 2);
            foreach (byte b in byteArray)
            {
                hex.AppendFormat("{0:x2}", b);

            }
                return hex.ToString();
        }

        public User PostDataOnLogin(User user, MyUsersDBEntities db)
        {
            foreach (var u in db.Users.Where(x => x.Username == user.Username && x.Hash == user.Hash))
            {
                if (u.Username == user.Username && u.Hash == user.Hash)
                {

                    user.Username = u.Username;
                    user.EmailAddress = u.EmailAddress;
                    user.Hash = u.Hash;
                    user.Id = u.Id;
                    user.FirstName = u.FirstName;
                    user.LastName = u.LastName;
                    user.Organization = u.Organization;
                    user.Salt = u.Salt;
                    u.isLoggedIn = true;
                    user.isLoggedIn = u.isLoggedIn;
                    user.Token = "test";


                    //encryptPassword(user, db);

                    // db.Entry(user).State = EntityState.Modified;

                    db.Set<User>().AddOrUpdate(user);




                }
            }
            db.SaveChanges();
            return user;
        }


        //Need to write method that takes the salt from the data base, and the hash that was saved and on login, have 
        //an algorythm that brings them together, this means, the rng to create the salt is fine, but no more rng after that

        public string RecreateHashedPassword()
        {
            return null;
        }

    }
}