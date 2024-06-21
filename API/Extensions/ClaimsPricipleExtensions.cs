using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPricipleExtemsions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }

}