using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace NotesBackend.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetLoggedInUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.GivenName);
        }

    }
}