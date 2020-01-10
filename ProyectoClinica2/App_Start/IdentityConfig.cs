using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ProyectoClinica2.Models;

namespace ProyectoClinica2
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            var adminRole = CrearRoles(context);
            CrearDefaultAdmin(context, adminRole);
            return manager;
        }

        private static string CrearRoles(IOwinContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            string[] roleNames = { "Admin", "Paciente", "Agendador" };
            IdentityResult roleResult;
            
            foreach (var roleName in roleNames)
            {
                var roleExist = roleManager.RoleExists(roleName);
                if (!roleExist)
                {
                    roleResult = roleManager.Create(new IdentityRole(roleName));
                }
            }

            return roleManager.FindByName(roleNames[0]).Name;
        }

        private static void CrearDefaultAdmin(IOwinContext context, string adminRole)
        {
            var userStore = new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>());
            var userManager = new UserManager<ApplicationUser>(userStore);

            var user = new ApplicationUser { UserName = "admin"};
            userManager.Create(user);

            var userCreated = userManager.FindByName("admin");
            userManager.AddPassword(userCreated.Id, "default-password-0000");
            userManager.AddToRole(userCreated.Id, adminRole);
        }
    }
}
