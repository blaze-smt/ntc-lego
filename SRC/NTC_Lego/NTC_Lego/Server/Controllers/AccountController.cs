using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;
using System.Security.Claims;

namespace NTC_Lego.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly DataService _dataService;
        private readonly ILogger<AccountController> _log;

        public AccountController(DataContext dataContext, ILogger<AccountController> log)
        {
            // Instantiate an instance of the data service.
            _dataService = new DataService(dataContext);
            _log = log;
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

            //todo: service call, add to user table and save
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
                // Set email address not registered error message.
                //ModelState.AddModelError("Error", "An account does not exist with that email address.");

                return BadRequest();
            }

            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            PasswordVerificationResult passwordVerificationResult =
                passwordHasher.VerifyHashedPassword(null, user.PasswordHash, userLogin.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                // Set invalid password error message.
                //ModelState.AddModelError("Error", "Invalid password.");
                _log.LogInformation($"Invalid login for {userLogin.Email} ({user.UserId}).");

                return BadRequest();
            }

            // Add the user's ID (NameIdentifier), first name and role
            // to the claims that will be put in the cookie.

/*            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties { };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
*/

            _log.LogInformation($"User logged in: {userLogin.Email} ({user.UserId}).");

            return Ok(user);
        }

        /*        [AllowAnonymous]
        [HttpGet("register")]
        public IActionResult Register()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }

            User existingUser = _dataService.GetUser(userRegister.Email);
            if (existingUser != null)
            {
                // Set email address already in use error message.
                //ModelState.AddModelError("EmailAddress", "An account already exists with that email address.");

                return Ok();
            }

            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

            User user = new User()
            {
                UserName = userRegister.UserName,
                UserEmail = userRegister.Email,
                PasswordHash = passwordHasher.HashPassword(null, userRegister.Password)
            };

            _dataService.AddUser(user);
            _log.LogInformation($"{userRegister.UserName} has been registered with {userRegister.Email} as their email address.");

            //NavigationManager.NavigateTo("login");

            return RedirectToAction(nameof(Login));
        }
*/

    }
}
