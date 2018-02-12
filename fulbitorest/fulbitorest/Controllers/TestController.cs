using apidata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;

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
        [Authorize]
        public string HelloSecure()
        {
            //JwtRegisteredClaimNames.Sub, userCredentials.User)
            //JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //ClaimTypes.NameIdentifier, userCredentials.User)
            var user = base.User;
            var claims = user.Claims;

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