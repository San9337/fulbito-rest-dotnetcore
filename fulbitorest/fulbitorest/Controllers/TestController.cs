using apidata.DataContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using model.Enums;
using model.Exceptions;

namespace FulbitoRest.Controllers
{
    /// <summary>
    /// For testing stuff
    /// </summary>
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
                NickName = "test",
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
                ReasonPhrase = "Created: " + data.NickName
            };
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(string id)
        {
            //If doesnt exist return 404
            return new HttpResponseMessage() {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = "Deleted: " + id
            };
        }

        [HttpPut]
        [Route("put/{id}")]
        public HttpResponseMessage Put(string id, [FromBody]UserCredentialsData data)
        {
            //If doesnt exist return 404
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = "Replaced: " + id
            };
        }

        [HttpPatch]
        [Route("patch/{id}")]
        [Consumes("application/json")]
        public HttpResponseMessage Patch(string id, [FromBody]UserCredentialsData newData)
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = "Updated: " + id
            };
        }

        [HttpGet]
        [Route("error")]
        public void Error(string msg)
        {
            throw new ApplicationException(msg);
        }

        [HttpGet]
        [Route("")]
        public ViewResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        [Route("httpreq")]
        public async void DoHttpRequest()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(new Uri("https://www.facebook.com/"));

            if (!response.IsSuccessStatusCode)
            {
                var contentError = await response.Content.ReadAsStringAsync();
                throw new FulbitoException(contentError);
            }
        }
    }
}


