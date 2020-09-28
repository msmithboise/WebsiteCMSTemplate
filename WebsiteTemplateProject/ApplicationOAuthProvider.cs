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
            MyUsersDBEntities userDb = new MyUsersDBEntities();
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            string contextPassword = "";

            using (var db = new MyUsersDBEntities())
            {
                var user = db.Users.ToList();

                if (user != null)
                {

                    foreach (var u in db.Users.Where(x => x.isPasswordHashed == true && x.Username == context.UserName))
                    {
                        LoginService loginService = new LoginService();

                        contextPassword = context.Password.ToString() ;
                        

                        contextPassword = loginService.reEncryptPassword(contextPassword, u.Salt, u.Hash, u);

                        if (!string.IsNullOrEmpty(user.Where(x => x.Username == context.UserName && x.Hash == contextPassword).FirstOrDefault().Username))
                        {
                            var currentUser = user.Where(x => x.Username == context.UserName && x.Hash == contextPassword).FirstOrDefault();





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

                    foreach (var u in db.Users.Where(x => x.isPasswordHashed == null && x.Username == context.UserName))
                    {
                        contextPassword = context.Password;


                        if (!string.IsNullOrEmpty(user.Where(x => x.Username == context.UserName && x.Hash == contextPassword).FirstOrDefault().Username))
                        {
                            var currentUser = user.Where(x => x.Username == context.UserName && x.Hash == contextPassword).FirstOrDefault();

                         



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
