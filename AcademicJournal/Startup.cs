﻿using Microsoft.Owin;
using Owin;
using AcademicJournal.DAL.Context;
using Microsoft.AspNet.Identity.EntityFramework;
using AcademicJournal.DAL.Models;
using Microsoft.AspNet.Identity;

[assembly: OwinStartupAttribute(typeof(AcademicJournal.Startup))]
namespace AcademicJournal
{
    public partial class Startup
    {
        private const string ADMIN_USER = "admin@epam.com";

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private async void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {
                // first we create Admin role   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            var adminUser = userManager.FindByName(ADMIN_USER);
            if (adminUser == null)
            {
                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser() { Email = ADMIN_USER, UserName = ADMIN_USER};
                string userPWD = "1";
                var result = await userManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin   
                if (result.Succeeded)
                {
                    if(!userManager.IsInRole(user.Id, "Admin"))
                    {
                        var result1 = userManager.AddToRole(user.Id, "Admin");
                    }
                }
            }

            // creating Creating Mentor role    
            if (!roleManager.RoleExists("Mentor"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Mentor";
                roleManager.Create(role);
            }

            // creating Creating Student role    
            if (!roleManager.RoleExists("Student"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);
            }
        }
    }
}
