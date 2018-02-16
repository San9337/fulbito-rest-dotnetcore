using apidata.DataContracts;
using apidata.Mapping;
using FulbitoRest.Exceptions;
using FulbitoRest.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [Authorize]
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

        [HttpPatch]
        [Route("patch/{id}")]
        public UserData Patch(string id, [FromBody]UserData data)
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