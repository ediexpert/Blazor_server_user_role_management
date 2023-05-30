using EMS.Amaanat.Web.Data;
using EMS.Amaanat.Web.Data.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EMS.Amaanat.Web.Seeder
{
    public class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            string[] roles = new string[] { "Administrator", "Manager", "User" };

            foreach (string role in roles)
            {
                RoleHelper.CreateRole(context, role);
            }

            var user = new IdentityUser("admin@events.com");
            user.Email = "admin@events.com";
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.SecurityStamp= Guid.NewGuid().ToString("D");

        }
    }
}
