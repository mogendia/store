using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System.Security.Authentication;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPricipleExtension
    {

        public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> _user, ClaimsPrincipal claim)
        {
            var user = await _user.Users.FirstOrDefaultAsync(x=>x.Email == claim.GetEmail());
            if (user == null) throw new AuthenticationException("User not found");
            return user;
        }
        public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> _user, ClaimsPrincipal claim)
        {
            var user = await _user.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == claim.GetEmail());
            if (user == null) throw new AuthenticationException("User not found");
            return user;
        }
        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email)
                ?? throw new AuthenticationException("Email not found");
            return email;
        }
    }
}
