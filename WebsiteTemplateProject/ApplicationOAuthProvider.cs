using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebsiteTemplateProject.Models;
using System.Security.Claims;
using WebAPI.Models;
using Microsoft.Owin.Security;
using WebsiteTemplateProject.Service;

namespace WebsiteTemplateProject
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            UserService userservice = new UserService();
            MyUserDBEntities userDb = new MyUserDBEntities();
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            using (var db = new MyUserDBEntities())
            {
                var user = db.Users.ToList();

                if (user != null)
                {
                    if (!string.IsNullOrEmpty(user.Where(x => x.Username == context.UserName && x.Hash == context.Password).FirstOrDefault().Username))
                    {
                        var currentUser = user.Where(x => x.Username == context.UserName && x.Hash == context.Password).FirstOrDefault();

                        foreach (var u in db.Users.Where(x => x.Username == context.UserName && x.Hash == context.Password))
                        {

                        userservice.encryptPassword(u,db);
                        }



                        identity.AddClaim(new Claim("UserName", currentUser.Username));
                        identity.AddClaim(new Claim("Id", Convert.ToString(currentUser.Id)));

                        var props = new AuthenticationProperties(new Dictionary<string, string>
                        {
                            {
                                "Username", context.UserName
                            },

                        });

                        var ticket = new AuthenticationTicket(identity, props);
                        context.Validated(ticket);
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Provided username and password is not matching, Please retry.");
                        context.Rejected();

                    }




                }
                else
                {
                    context.SetError("invalid_grant", "Provided username and password is not matching, Please retry.");
                    context.Rejected();
                }
                return;
            }

        }

    }
}
