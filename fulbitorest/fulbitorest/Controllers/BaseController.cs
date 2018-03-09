using FulbitoRest.Technical.Interception;
using FulbitoRest.Technical.Security;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace FulbitoRest.Controllers
{
    /// <summary>
    /// Functionality that all controllers share
    /// </summary>
    [ServiceFilter(typeof(LoggingFilterAttribute))]
    [ExceptionFormatter]
    public class BaseController : Controller
    {
        public Claim UserIdClaim { get {
                return HttpContext.User.Claims.First(c => c.Type == FulbitoClaims.UserId);
            }
        }
    }
}
