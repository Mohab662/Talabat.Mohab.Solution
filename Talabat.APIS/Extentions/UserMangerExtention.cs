using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIS.Extentions
{
    public static class UserMangerExtention
    {
        public async static Task<AppUser> FindUserByAddressWithEmailAsync(this UserManager<AppUser> UserManager,ClaimsPrincipal User) 
        {
            var email=User.FindFirstValue(ClaimTypes.Email);
            var user = UserManager.Users.Include(x => x.Address).FirstOrDefault(u=>u.NormalizedEmail==email.ToUpper());

            return user;
        
        
        }
    }
}
