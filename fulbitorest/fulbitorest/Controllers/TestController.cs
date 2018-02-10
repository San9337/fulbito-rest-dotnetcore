using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FulbitoRest.Technical.Interception;
using apidata;
using System.Net.Http;
using System.Net;
using FulbitoRest.Technical.Security;

namespace FulbitoRest.Controllers
{
    [Produces("application/json")]
    public class TestController : BaseController
    {
        [HttpGet]
        public string Hello()
        {
            //http://localhost:65520/api/test/hello
            return "Hi Eze";
        }

        [HttpGet]
        [ServiceFilter(typeof(AuthenticateAttribute))]
        public string HelloSecure()
        {
            return "Authenticated Hi";
        }

        [HttpGet]
        public UserCredentialsData Param(string param)
        {
            //http://localhost:65520/api/test/param?param="hi"
            return new UserCredentialsData()
            {
                User = "test",
                Password = param
            };
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]UserCredentialsData data)
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Created,
                ReasonPhrase = "Created: " + data.User
            };
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            //If doesnt exist return 404
            return new HttpResponseMessage() {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = "Deleted: " + id.ToString()
            };
        }

        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]UserCredentialsData data)
        {
            //If doesnt exist return 404
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = "Updated: " + id.ToString()
            };
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View("Index");
        }
    }
}