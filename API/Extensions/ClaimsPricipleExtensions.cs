using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPricipleExtemsions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
        public static int  GetUserId(this ClaimsPrincipal user)
        {
            var userId =  user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
             return int.Parse(userId);
        }
    }

}