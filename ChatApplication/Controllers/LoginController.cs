using System.Threading.Tasks;
using ChatApplication.Contracts;
using ChatApplication.Model;
using ChatApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IBaseRepository<User> baseRepository;

        public LoginController(IBaseRepository<User> paramBaseRepository)
        {
            baseRepository = paramBaseRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LoginViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
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

            user.Addresses = new Address();
            user.Addresses.Name = viewModel.AddressName;
            user.Addresses.AddressLine = viewModel.AddressLine;

            user.PersonalInformation = new PersonalInformation();
            user.PersonalInformation.FirstName = viewModel.FirstName;
            user.PersonalInformation.LastName = viewModel.LastName;
            user.PersonalInformation.ContactNumber = viewModel.ContactNumber;
            user.PersonalInformation.Email = viewModel.Email;

            await baseRepository.InsertOneAsync(user);

            return Ok();
        }

        [HttpGet("Ping")]
        public string Ping() {
            return "Api is running";
        }
    }
}