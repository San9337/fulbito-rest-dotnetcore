using apidata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;

namespace FulbitoRest.Controllers
{
    [Produces("application/json")]
    [Route("api/test")]
    public class TestController : BaseController
    {
        [HttpGet]
        [Route("hello")]
        public string Hello()
        {
            //http://localhost:65520/api/test/hello
            return "Hi Eze";
        }

        [HttpGet]
        [Route("hellosecure")]
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
        [Route("param")]
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
        [Route("post")]
        public HttpResponseMessage Post([FromBody]UserCredentialsData data)
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Created,
                ReasonPhrase = "Created: " + data.User
            };
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(int id)
        {
            //If doesnt exist return 404
            return new HttpResponseMessage() {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = "Deleted: " + id.ToString()
            };
        }

        [HttpPut]
        [Route("put")]
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
        [Route("")]
        public ViewResult Index()
        {
            return View("Index");
        }
    }
}