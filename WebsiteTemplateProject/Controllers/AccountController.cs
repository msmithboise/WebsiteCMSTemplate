using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebsiteTemplateProject.Models;

namespace WebsiteTemplateProject.Controllers
{
    public class AccountController : ApiController
    {
        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(User model)
        {
            var userStore = new UserStore<User>(new NewUserDbEntities());
            var manager = new UserManager<User>(userStore);
            var user = new User() {
                UserName = model.Username,
                EmailAddress = model.EmailAddress

            };
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3
            };
            IdentityResult result = manager.Create(user, model.Hash);
            return result;
        }

        [HttpGet]
        [Route("api/GetUserClaims")]
        public User GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            User model = new User()
            {
                Username = identityClaims.FindFirst("Username").Value,
                EmailAddress = identityClaims.FindFirst("EmailAddress").Value,
                FirstName = identityClaims.FindFirst("FirstName").Value,
                LastName = identityClaims.FindFirst("LastName").Value,
                Organization = identityClaims.FindFirst("Organization").Value
            };
            return model;
        }
    }
}
