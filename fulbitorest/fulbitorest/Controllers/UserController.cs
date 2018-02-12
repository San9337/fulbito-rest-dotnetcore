using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    public class UserController : Controller
    {
        [HttpPut]
        public void UploadData(string data)
        {

        }
    }
}