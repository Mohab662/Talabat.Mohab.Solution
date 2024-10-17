using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Data.Identity
{
    public static class AppIdentityDbContextSeed
    {

        public static async Task SeedUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var User = new AppUser
                {
                    DisplayName = "Mohab Mohamed",
                    Email = "Mohab@gmail.com",
                    UserName = "Belkan",
                    PhoneNumber = "01112585780"
                };
                await _userManager.CreateAsync(User, "Mohab@265");
            }

        }
    }
}
