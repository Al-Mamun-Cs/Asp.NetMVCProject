using AuthATMProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthATMProject.DBInitializer
{
    public class IdentityHelper
    {
        internal static void SeedIdentities(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists(InitialRole.ROLE_ADMINISTRATOR))
            {
                var roleresult = roleManager.Create(new IdentityRole(InitialRole.ROLE_ADMINISTRATOR));
                if (roleresult.Succeeded)
                {
                    string userName = "admin@gmail.com";
                    string password = "@Admin123";
                    ApplicationUser user = userManager.FindByName(userName);
                    if (user == null)
                    {
                        user = new ApplicationUser()
                        {
                            UserName = userName,
                            Email = userName,
                            EmailConfirmed = true
                        };
                        IdentityResult userResult = userManager.Create(user, password);
                        if (userResult.Succeeded)
                        {
                            var result = userManager.AddToRole(user.Id, InitialRole.ROLE_ADMINISTRATOR);
                        }
                    }
                }

            }

        }
    }
}