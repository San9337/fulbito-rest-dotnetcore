using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using apidata.DataContracts;
using FulbitoRest.Repositories;
using FulbitoRest.Exceptions;
using apidata.Mapping;
using Microsoft.EntityFrameworkCore;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    //[Authorize]
    public class UserController : Controller
    {
        private readonly FulbitoDbContext _context;

        public UserController(FulbitoDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public UserData Get(int id)
        {
            var user = GetUser(id);

            return user.Map();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public string Delete(int id)
        {
            var user = GetUser(id);

            _context.Remove(user);
            _context.SaveChanges();

            return "user deleted";
        }

        //[HttpPatch]
        //[Route("")]
        public UserData Patch([FromBody]UserData data)
        {
            var user = GetUser(data.Id);

            user.Age = data.Age;
            user.CountryName = data.CountryName;
            user.Gender = data.Gender == "male" ? model.Enums.Gender.Male : model.Enums.Gender.Female;
            user.LocationName = data.LocationName;
            user.ProfilePictureUrl = data.ProfilePictureUrl;
            user.RealTeam = data.RealTeam;
            user.SkilledFoot = data.SkilledFoot;

            _context.SaveChanges();

            return data;
        }

        private model.Model.User GetUser(int id)
        {
            var user = _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Credentials)
                .FirstOrDefault();
            if (user == null)
                throw new UnexpectedInputException("User doesn't exist");
            return user;
        }
    }
}