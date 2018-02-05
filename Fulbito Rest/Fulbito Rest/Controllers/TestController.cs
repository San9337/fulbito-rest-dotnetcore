using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FulbitoRest.Technical.Interception;

namespace FulbitoRest.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : BaseController
    {
        [HttpGet]
        [Route("Hello")]
        public string Hello()
        {
            //http://localhost:65520/api/test/hello
            return "Hi Eze";
        }

        [HttpGet]
        [Route("Param")]
        public string Param(string param)
        {
            //http://localhost:65520/api/test/param?param="hi"
            return param;
        }

        [HttpGet]
        [Route("Index")]
        public ViewResult Index()
        {
            return View("Index");
        }
    }
}