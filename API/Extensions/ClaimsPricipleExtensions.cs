using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPricipleExtemsions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            // return user.FindFirst(ClaimTypes.Name)?.Value;
            if (user == null)
            {
                return null;
            }

            var userNameClaim = user.FindFirst(ClaimTypes.Name);
            if (userNameClaim == null)
            {
                return null;
            }

            return userNameClaim.Value;
        }
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userId);
        }
    }

}