using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynamics365AppRegistration.Data;
using Microsoft.AspNetCore.Identity;
using Dynamics365AppRegistration.Models;

namespace Dynamics365AppRegistration.Data
{
    public static class DbInitializer
    {
        public async static void InitializeUsers(UserManager<ApplicationUser> userManager, AppSettings appSettings)
        {
            if (userManager.Users.Any())
            {
                return;
            }

            var user = new ApplicationUser();
            user.UserName = appSettings.InitialUser.email;
            user.Email = user.UserName;

            var result = await userManager.CreateAsync(user, appSettings.InitialUser.password);
        }
    }
}
