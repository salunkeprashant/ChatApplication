using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Contracts;
using ChatApplication.Helper;
using ChatApplication.Model;
using ChatApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;

namespace ChatApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IBaseRepository<User> userRepository;

        public UsersController(IBaseRepository<User> paramUserRepository)
        {
            userRepository = paramUserRepository;
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]LoginViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Expression<Func<User, bool>> userPredicate = (x => x.Username == credentials.Username && x.Password == credentials.Password);

            var user = await this.userRepository.FindOneAsync(userPredicate);

            // authentication successful so generate jwt token
            user.BearerToken = GenerateBearerToken(user);

            return Ok(user);
        }

        private string GenerateBearerToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constants.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody]UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Assign properties from view models
            // TODO: Implement custom automapper for entity and viewmodel 
            var user = new User();
            user.Username = viewModel.Username;

            List<Address> addresses = new List<Address>();
            addresses.Add(new Address
            {
                Name = viewModel.AddressName,
                AddressLine = viewModel.AddressLine
            });

            user.Addresses = addresses.ToArray();
            user.PersonalInformation = new PersonalInformation
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                ContactNumber = viewModel.ContactNumber,
                Email = viewModel.Email
            };

            await userRepository.InsertOneAsync(user);

            return Ok();
        }

        [HttpGet("list")]
        public IQueryable<User> ListUsers()
        {
            return userRepository.AsQueryable();
        }

        [HttpGet("Ping"), AllowAnonymous]
        public string Ping()
        {
            return "Api is up and running";
        }

        [HttpGet("AuthPing")]
        public string AuthPing()
        {
            return "You are authorized to access the API";
        }
    }
}