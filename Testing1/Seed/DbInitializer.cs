using Microsoft.AspNetCore.Identity;
using Testing1.Models;

namespace Testing1.Seed
{
    public class DbInitializer
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception($"Admin rolu yaradıla bilmədi: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                    }
                }

                var adminEmail = "admin@example.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true,

                        Fullname = "Admin"
                    };

                  

                    var createResult = await userManager.CreateAsync(adminUser, "Admin123!");
                    if (!createResult.Succeeded)
                    {
                        throw new Exception($"Admin istifadəçi yaradıla bilmədi: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                    }

                    var roleAssignResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                    if (!roleAssignResult.Succeeded)
                    {
                        throw new Exception($"Admin rol təyini uğursuz oldu: {string.Join(", ", roleAssignResult.Errors.Select(e => e.Description))}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DbInitializer xətası: " + ex.Message);
                throw; 
            }
        }
    }
}
