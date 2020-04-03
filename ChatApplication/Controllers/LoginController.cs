using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChatApplication.Contracts;
using ChatApplication.Model;
using ChatApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;

namespace ChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IBaseRepository<User> userRepository;

        public LoginController(IBaseRepository<User> paramUserRepository)
        {
            userRepository = paramUserRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LoginViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Expression<Func<User, bool>> userPredicate = (x => x.Username == credentials.Username && x.Password == credentials.Password);
            
            var user = await this.userRepository.FindOneAsync(userPredicate);

            return Ok(user.ToString());
        }

        [HttpPost("NewUser")]
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

        [HttpGet("Ping")]
        public string Ping() {
            return "Api is running";
        }
    }
}