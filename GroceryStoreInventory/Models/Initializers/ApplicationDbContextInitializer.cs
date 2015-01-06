using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.Models.Initializers
{
    public class ApplicationDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string name = "Admin";
            string password = "@abcd1234";
            string adminRole = "admin";

            if(!RoleManager.RoleExists(adminRole))
            {
                RoleManager.Create(new IdentityRole(adminRole));
            }

            var user = new ApplicationUser();
            user.UserName = name;
            var adminresult = UserManager.Create(user, password);
            if(adminresult.Succeeded)
            {
                UserManager.AddToRole(user.Id, adminRole);
            }
        }
    }
}