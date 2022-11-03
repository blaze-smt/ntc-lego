using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;

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

        [AllowAnonymous]
        [HttpGet("sign-in")]
        public IActionResult Login()
        {
            return View();
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
