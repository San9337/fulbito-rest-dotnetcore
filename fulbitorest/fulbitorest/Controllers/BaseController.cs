using FulbitoRest.Technical.Interception;
using Microsoft.AspNetCore.Mvc;

namespace FulbitoRest.Controllers
{
    [ServiceFilter(typeof(LoggingFilterAttribute))]
    [ExceptionFormatter]
    public class BaseController : Controller
    {
    }
}
