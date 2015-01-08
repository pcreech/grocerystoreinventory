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
        void EnsureRole(RoleManager<IdentityRole> RoleManager, string role)
        {
            if (!RoleManager.RoleExists(role))
            {
                RoleManager.Create(new IdentityRole(role));
            }
        }

        void EnsureUser(UserManager<ApplicationUser> UserManager, string userName, string password, IEnumerable<string> roles)
        {
            ApplicationUser user = UserManager.FindByName(userName);
            if (user == null)
            {
                user = new ApplicationUser();
                user.UserName = userName;
                UserManager.Create(user, password);
            }
            foreach(var role in roles)
            {
                if(!UserManager.IsInRole(user.Id, role))
                {
                    UserManager.AddToRole(user.Id, role);
                }
            }
        }
        
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string adminUsername = "owner";
            string adminpassword = "@abcd1234";
            string adminRole = "admin"; //Admin Role

            string employeeUsername = "employee";
            string employeePassword = "@1234abcd";
            string baseRole = "user";  //Employee/Base role

            EnsureRole(RoleManager, adminRole);
            EnsureRole(RoleManager, baseRole);
            EnsureUser(UserManager, adminUsername, adminpassword, new string[] { adminRole, baseRole });
            EnsureUser(UserManager, employeeUsername, employeePassword, new string[] { baseRole });
           
        }
    }
}