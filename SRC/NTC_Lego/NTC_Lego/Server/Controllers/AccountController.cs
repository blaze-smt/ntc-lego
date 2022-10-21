using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NTC_Lego.Server.Services;

namespace NTC_Lego.Server.Controllers
{
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


    }
}
