using EMS.Amaanat.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EMS.Amaanat.Web.Data.Helpers
{
    public class RoleHelper
    {
        static RoleStore<IdentityRole> roleStrore = null;
        public static Result CreateRole(ApplicationDbContext dbContext ,string roleName)
        {            
            Initialize(dbContext);

            roleName = roleName.Trim().ToUpper();
            if(!dbContext.Roles.Any(x => x.Name == roleName))
            {               
                var identityResult = Task.Run(async () => await roleStrore.CreateAsync(new IdentityRole(roleName))).Result;
                if (identityResult.Succeeded)
                    return new Result($"Role({roleName}) created successfully.");

                return new Result(string.Format("ERROR: {0}", string.Join(',', identityResult.Errors.Select(x => x.Description))));
            }
            else
            {
                return new Result( $"Role({roleName}) already exist.", false);
            }
        }
        
        public static async Task RemoveRole(ApplicationDbContext dbContext ,IdentityRole  role)
        {
            Initialize(dbContext);
            await roleStrore.DeleteAsync(role);
            //Task.Run(async () => await roleStrore.DeleteAsync(role));
        }

        static void Initialize(ApplicationDbContext dbContext)
        {
            if (roleStrore == null)
                roleStrore = new RoleStore<IdentityRole>(dbContext);
        }
    }
}
