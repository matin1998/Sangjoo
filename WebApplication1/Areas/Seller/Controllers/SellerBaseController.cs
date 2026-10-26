using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sangjoo.Presentation.Areas.Seller.Controllers;

[Area("Seller")]
[Authorize(Roles = "Seller")]
public abstract class SellerBaseController : Controller
{
    protected long CurrentUserId
    {
        get
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException();

            return long.Parse(userId);
        }
    }
}
