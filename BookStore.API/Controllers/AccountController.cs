using System.Threading.Tasks;
using BookStore.Repository.Dtos;
using BookStore.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            var result = await _accountRepository.SignUpAsync(signUpDto);

            if (result == null) return BadRequest("Could not create Account");

            return Ok(result);
        
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("admin/signup")]
        public async Task<IActionResult> AdminSignUp([FromBody] SignUpDto signUpDto)
        {
            var result = await _accountRepository.AdminSignUpAsync(signUpDto);

            if (result == null) return BadRequest("Could not create Account");

            return Ok(result);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInDto signInDto)
        {
            var result = await _accountRepository.LoginAsync(signInDto);

            if (result == null)
            {
                return Unauthorized("User does not exist");
            }

            return Ok(result);

        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
