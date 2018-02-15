using FulbitoRest.Technical.Interception;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Controllers
{
    [ServiceFilter(typeof(LoggingFilterAttribute))]
    [ExceptionFormatter]
    public class BaseController : Controller
    {
    }
}
