using FulbitoRest.Technical.Interception;
using Microsoft.AspNetCore.Mvc;

namespace FulbitoRest.Controllers
{
    /// <summary>
    /// Functionality that all controllers share
    /// </summary>
    [ServiceFilter(typeof(LoggingFilterAttribute))]
    [ExceptionFormatter]
    public class BaseController : Controller
    {
    }
}
