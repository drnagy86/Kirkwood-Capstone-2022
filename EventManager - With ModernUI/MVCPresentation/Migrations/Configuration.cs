namespace MVCPresentation.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MVCPresentation.Models;
    using LogicLayer;
    using DataObjects;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCPresentation.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MVCPresentation.Models.ApplicationDbContext";
        }

        protected override void Seed(MVCPresentation.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string admin = "admin@company.com";
            const string adminPassword = "P@ssw0rd";
            LogicLayerInterfaces.IUserManager userMgr = new LogicLayer.UserManager();
            var roles = userMgr.RetrieveAllRoles();
            foreach (var role in roles)
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = role });
                if (!roles.Contains("Administrator"))
                {
                    context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Administrator" });
                }
            }

            if (!context.Users.Any(u => u.UserName == admin))
            {
                var user = new ApplicationUser()
                {
                    UserName = admin,
                    Email = admin,
                    GivenName = "Admin",
                    FamilyName = "McGuffin",
                    City = "Cedar Rapids",
                    State = "IA",
                    Zip = 52402
                };
                IdentityResult result = userManager.Create(user, adminPassword);
                context.SaveChanges();
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                    context.SaveChanges();
                    var usrMgr = new UserManager();
                    try
                    {
                        User newUser = new User()
                        {
                            EmailAddress = admin,
                            GivenName = "Admin",
                            FamilyName = "McGuffin",
                            City = "Cedar Rapids",
                            State = "IA",
                            Zip = 52402
                        };
                        usrMgr.CreateUser(newUser);
                        // Refresh the newly-added user to get the user ID.
                        newUser = usrMgr.RetrieveUserByEmail(newUser.EmailAddress);
                        usrMgr.AddUserRole(newUser.UserID, "Administrator");
                    } catch(Exception ex)
                    {
                        // We want it to be brought to the user's attention that this didn't work
                        throw;
                    }
                }
            }
        }
    }
}
