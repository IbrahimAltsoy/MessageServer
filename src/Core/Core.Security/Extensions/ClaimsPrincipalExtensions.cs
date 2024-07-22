using System.Security.Claims;

namespace Core.Security.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static List<string>? Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        List<string>? result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
        return result;
    }

    public static List<string>? ClaimRoles(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal?.Claims(ClaimTypes.Role);

    public static Guid? GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        //var userIdClaim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "uid");
        var userIdClaim = claimsPrincipal?.Claims.SingleOrDefault(c => c.Type == "uid");

        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return userId;
        }

        return null;
    }

    public static string? GetUserEmail(this ClaimsPrincipal claimsPrincipal)
    {
        var userMailClaim = claimsPrincipal?.Claims.SingleOrDefault(c => c.Type == "email");

        if (userMailClaim != null)
        {
            return userMailClaim.Value; 
        }

        return null;
    }

}
