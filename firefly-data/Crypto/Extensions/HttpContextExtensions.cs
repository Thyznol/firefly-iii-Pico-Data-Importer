using System.Security.Principal;

namespace firefly_iii_odata.Extensions;

public static class HttpContextExtensions
{
    public static uint FireflyUserId(this HttpContext? context)
        => uint.TryParse(
            context?.User.Identity?.Name ?? throw new Exception("Could not find user name from HttpContext"),
            out var userId
        )
        ? userId
        : throw new Exception("Could not parse identity name as user id");
}