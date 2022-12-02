using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using NTC_Lego.Server.Util;

namespace NTC_Lego.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly DataService _dataService;
        private readonly ILogger<AccountController> _log;
        private readonly IConfiguration _configuration;

        public AccountController(DataContext dataContext, ILogger<AccountController> log, IConfiguration configuration)
        {
            // Instantiate an instance of the data service.
            _dataService = new DataService(dataContext);
            _log = log;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegister user)
        {
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

            User newUser = new User()
            {
                UserName = user.UserName,
                UserEmail = user.Email,
                PasswordHash = passwordHasher.HashPassword(null, user.Password)
            };

            _dataService.AddUser(newUser);
            _log.LogInformation($"{newUser.UserName} has been registered with {newUser.UserEmail} as their email address.");

            return Ok(newUser);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromBody] UserLogin userLogin)
        {
            User user = _dataService.GetUser(userLogin.Email);

            if (user == null)
            {
                // returns Code 400 error code
                return BadRequest();
            }

            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            PasswordVerificationResult passwordVerificationResult =
                passwordHasher.VerifyHashedPassword(null, user.PasswordHash, userLogin.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                _log.LogInformation($"Invalid login for {userLogin.Email} ({user.UserId}).");

                // returns Code 400 error code
                return BadRequest();
            }

            _log.LogInformation($"User logged in: {userLogin.Email} ({user.UserId}).");

            UserTokenVM userToken = new UserTokenVM();
            userToken.User = user;
            userToken.Token = JWTUtil.CreateToken(user, _configuration);

            // returns 200 code with UserToken view model
            return Ok(userToken);
        }

        [HttpGet]
        [Route("user")]
        public UserVM GetUser(int userId)
        {
            User user = _dataService.GetUser(userId);

            UserVM userVM = new UserVM();
            userVM.UserId = user.UserId;
            userVM.UserName = user.UserName;
            userVM.UserEmail = user.UserEmail;

            return userVM;
        }

    }
}
