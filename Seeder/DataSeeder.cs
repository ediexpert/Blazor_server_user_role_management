using EMS.Amaanat.Web.Data;
using EMS.Amaanat.Web.Data.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Amaanat.Web.Seeder
{
    public static class DataSeeder
    {
        public static void SeedRolesAndAdminUser(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            ApplicationDbContext _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }
            string[] roles = new string[] { "Administrator", "Manager", "User" };
            string adminUserName = "admin";
            string adminEmail = "admin@identity.de";
            string adminPassword = "Test@123#";
            // seeding roles required for the application
            foreach (var roleName in roles)
            {
                if (!_context.Roles.Any(x => x.Name == roleName))
                {
                    _context.Roles.Add(new IdentityRole { Name = roleName, NormalizedName = roleName.ToUpper() });
                    _context.SaveChanges();
                }
            }

            // seeding admin user 
            if (!_context.Users.Any(x => x.UserName == adminUserName))
            {
                //a hasher to hash the password before seeding the user to the db
                var hasher = new PasswordHasher<IdentityUser>();
                _context.Users.Add(new IdentityUser
                {
                    UserName = adminUserName,
                    NormalizedUserName = adminUserName,
                    Email = adminEmail,
                    NormalizedEmail = adminEmail,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null, adminPassword)
                });
                _context.SaveChanges();
            }

            // adding amdin user to administrativa role
            IdentityUser? adminUsr = _context.Users.FirstOrDefault(x => x.UserName == adminUserName);
            IdentityRole? role = _context.Roles.FirstOrDefault(x => x.Name == "Administrator");
            if (adminUsr != null && role != null && !_context.UserRoles.Any(x => x.UserId == adminUsr.Id && x.RoleId == role.Id))
            {
                _context.UserRoles.Add(new IdentityUserRole<string>()
                {
                    RoleId = role.Id,
                    UserId = adminUsr.Id,
                });
                _context.SaveChanges();
            }
        }
    }
}
