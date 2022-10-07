using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using GamerParadise.DataAccess.Models;
using GamerParadise.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using GamerParadise.DataAccess.Data;
using Microsoft.AspNetCore.Authentication;
using GamerParadise.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace GamerParadise.Controllers
{
    [Authorize]
    [Route("")]
    public class AccountController : Controller
    {
        private readonly DataService _dataService;

        private readonly UspsService _uspsService;

        private readonly AppConfig _appConfig;

        public AccountController(DataContext dataContext, IOptions<AppConfig> appConfig, UspsService uspsService)
        {
            _appConfig = appConfig.Value;
            // Instantiate an instance of the data service.
            _dataService = new DataService(dataContext);
            _uspsService = uspsService;
        }

        [Route("access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("/sign-in")]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("/sign-in")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = _dataService.GetUser(loginViewModel.EmailAddress);

            if (user == null)
            {
                // Set email address not registered error message.
                ModelState.AddModelError("Error", "An account does not exist with that email address.");

                return View();
            }

            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            PasswordVerificationResult passwordVerificationResult =
                passwordHasher.VerifyHashedPassword(null, user.PasswordHash, loginViewModel.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                // Set invalid password error message.
                ModelState.AddModelError("Error", "Invalid password.");

                return View();
            }

            // Add the user's ID (NameIdentifier), first name and role
            // to the claims that will be put in the cookie.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties { };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect(returnUrl);
            }
        }

        [HttpGet("/sign-out")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("/register")]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User existingUser = _dataService.GetUser(registerViewModel.EmailAddress);
            if (existingUser != null)
            {
                // Set email address already in use error message.
                ModelState.AddModelError("EmailAddress", "An account already exists with that email address.");

                return View();
            }

            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

            User user = new User()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                EmailAddress = registerViewModel.EmailAddress,
                Username = registerViewModel.Username,
                ProfileImage = null,
                PasswordHash = passwordHasher.HashPassword(null, registerViewModel.Password)
            };

            _dataService.AddUser(user);

            return RedirectToAction(nameof(Login));
        }

        [HttpGet("add-credit-card")]
        public IActionResult AddCreditCard()
        {
            return View();
        }

        [HttpPost("add-credit-card")]
        public IActionResult AddCreditCard(CreditCard creditCard)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Validate address.
            bool addressVerified = _uspsService.ValidateAddress(creditCard.Address, creditCard.Address2, creditCard.City, creditCard.State, creditCard.Zip).Result;
            if (!addressVerified)
            {
                ModelState.AddModelError("Address", "The address you entered is invalid.");
                return View(creditCard);
            }

            // NameIdentifier sets the UserId of the current User to the CreditCard User value.
            creditCard.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _dataService.AddCreditCard(creditCard);

            return RedirectToAction("Profile", "Home");
        }

        [HttpGet("addgamelibrary/{id:int}")]
        [Route("addgamelibrary/{id:int}")]
        public IActionResult AddGameLibrary(int id)
        {
            User u = _dataService.GetUser(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            if (_dataService.AddGameToLibrary(u, id))
            {
                return RedirectToAction(nameof(Library));
            }
            return RedirectToAction("Subscription", "Home");
        }

        [HttpGet("edit-review/{id:int}")]
        [Route("edit-review/{id:int}")]
        public IActionResult EditReview(int id)
        {
            Review review = _dataService.GetReview(id);
            return View(review);
        }

        [HttpPost("edit-review/{id:int}")]
        public IActionResult EditReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                review.Game = _dataService.GetGame(review.GameId);
                return View(review);
            }

            _dataService.UpdateReview(review);
            return RedirectToAction(nameof(Library));
        }

        [HttpGet("add-review/{id:int}")]
        public IActionResult AddReview(int id)
        {
            Game game = _dataService.GetGame(id);
            Review r = new Review();
            r.Game = game;
            return View(r);
        }

        [HttpPost("add-review/{id:int}")]
        public IActionResult AddReview(Review review, int id)
        {
            if (!ModelState.IsValid)
            {
                review.Game = _dataService.GetGame(id);
                return View(review);
            }

            review.GameId = id;
            review.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _dataService.AddReview(review);
            return RedirectToAction("Library", "Account");
        }

        [HttpGet("delete-review/{id:int}")]
        public IActionResult DeleteReviewConfirm(int id)
        {
            Review r = _dataService.GetReview(id);
            return View(r);
        }

        [HttpPost("delete-review/{id:int}")]
        public IActionResult DeleteReview(int id)
        {
            _dataService.DeleteReview(id);

            return RedirectToAction(nameof(Library));
        }

        [HttpGet("add-subscription/{id:int}")]
        public IActionResult AddSubscription(int id)
        {
            // Checks if user has subscription already, and if so redirects them to the EditSubscription.
            User user = _dataService.GetUser(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            Subscription subscription = user.Subscription;
            if (user.Subscription == null)
            {
                subscription = new Subscription();
                subscription.Plan = _dataService.GetPlan(id);
                subscription.PlanId = id;
                subscription.User = user;
                subscription.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            else
            {
                return RedirectToAction("EditSubscription", "Account", new { id = user.Subscription.SubscriptionId });
            }

            return View(subscription);
        }

        [HttpPost("add-subscription/{id:int}")]
        public IActionResult AddSubscription(Subscription subscription)
        {
            if (!ModelState.IsValid)
            {
                subscription.Plan = _dataService.GetPlan(subscription.PlanId);
                subscription.User = _dataService.GetUser(subscription.UserId);
                return View(subscription);
            }
            subscription.ExpirationDate = DateTime.Now.AddDays(30);
            subscription.IsActive = true;
            _dataService.AddSubscription(subscription);

            return RedirectToAction("Profile", "Home");
        }

        [HttpGet("cancel-subscription/{id:int}")]
        public IActionResult CancelSubscription(int id)
        {
            SubscriptionViewModel subscriptionViewModel = new SubscriptionViewModel();
            subscriptionViewModel.Subscription = _dataService.GetSubscription(id);
            return View(subscriptionViewModel);
        }

        [HttpPost("cancel-subscription/{id:int}")]
        public IActionResult CancelSubscription(SubscriptionViewModel subscriptionViewModel)
        {
            _dataService.CancelSubscription(subscriptionViewModel.Subscription);
            return RedirectToAction("Profile", "Home");
        }

        [Route("credit-cards")]
        public IActionResult CreditCards()
        {
            List<CreditCard> model = _dataService.GetCreditCards();

            return View(model);
        }

        [HttpGet("delete-credit-card/{id:int}")]
        public IActionResult DeleteCreditCard(int id)
        {
            CreditCard cc = _dataService.GetCreditCard(id);
            return View(cc);
        }

        [HttpPost("delete-credit-card/{id:int}")]
        public IActionResult DeleteCreditCardConfirm(int id)
        {
            _dataService.DeleteCreditCard(id);

            return RedirectToAction("Profile", "Home");
        }

        [HttpGet("edit-credit-card/{id:int}")]
        public IActionResult EditCreditCard(int id)
        {
            CreditCard model = _dataService.GetCreditCard(id);
            return View(model);
        }

        [HttpPost("edit-credit-card/{id:int}")]
        public IActionResult EditCreditCard(CreditCard creditCard)
        {
            if (!ModelState.IsValid)
            {
                return View(creditCard);
            }

            // Validate address.
            bool addressVerified = _uspsService.ValidateAddress(creditCard.Address, creditCard.Address2, creditCard.City, creditCard.State, creditCard.Zip).Result;
            if (!addressVerified)
            {
                ModelState.AddModelError("Address", "The address you entered is invalid.");
                return View(creditCard);
            }

            // NameIdentifier sets the UserId of the current User to the CreditCard User value.
            creditCard.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _dataService.UpdateCreditCard(creditCard);

            return RedirectToAction("Profile", "Home");
        }

        [HttpGet("edit-profile")]
        public IActionResult EditProfile()
        {
            // Get user id.
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Get user.
            User u = _dataService.GetUser(userId);

            // Populate view model.
            EditProfileViewModel vm = new EditProfileViewModel()
            {
                EmailAddress = u.EmailAddress,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Username
            };

            return View(vm);
        }

        [HttpPost("edit-profile")]
        public IActionResult EditProfile(EditProfileViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // Get current user.
            User current = _dataService.GetUser(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));

            PasswordHasher<string> hasher = new PasswordHasher<string>();

            // Confirm password.
            if (hasher.VerifyHashedPassword(null, current.PasswordHash, vm.OldPassword) == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("OldPassword", "Your password is incorrect.");

                return View(vm);
            }

            // Set user fields.
            current.FirstName = vm.FirstName;
            current.LastName = vm.LastName;
            current.EmailAddress = vm.EmailAddress;
            current.Username = vm.Username;
            if (vm.NewProfileImage != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    vm.NewProfileImage.CopyTo(ms);
                    current.ProfileImage = ms.ToArray();
                }
            }

            // Check if we should be updating the password.
            if (!string.IsNullOrEmpty(vm.NewPassword))
            {
                // Hash password.
                current.PasswordHash = hasher.HashPassword(null, vm.NewPassword);
            }

            _dataService.UpdateUser(current);

            return RedirectToAction("profile", "home");
        }

        [HttpGet("edit-subscription/{id:int}")]
        public IActionResult EditSubscription(int id)
        {
            SubscriptionViewModel subscriptionViewModel = new SubscriptionViewModel();
            subscriptionViewModel.Subscription = _dataService.GetSubscription(id);
            if (subscriptionViewModel.Subscription.IsActive == false)
            {
                subscriptionViewModel.Subscription.ExpirationDate = DateTime.Now.AddDays(30);
            }
            subscriptionViewModel.Subscription.Plan = _dataService.GetPlan(subscriptionViewModel.Subscription.PlanId);
            subscriptionViewModel.Subscription.User = _dataService.GetUser(subscriptionViewModel.Subscription.UserId);
            subscriptionViewModel.Subscription.UserId = subscriptionViewModel.Subscription.UserId;
            subscriptionViewModel.AllPlans = _dataService.GetPlans();
            return View(subscriptionViewModel);
        }

        [HttpPost("edit-subscription/{id:int}")]
        public IActionResult EditSubscription(SubscriptionViewModel subscriptionViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(subscriptionViewModel);
            }
            subscriptionViewModel.Subscription.IsActive = true; // Added to make original SubscriptionId active when resubscribing
            _dataService.UpdateSubscription(subscriptionViewModel.Subscription);

            return RedirectToAction("Profile", "Home");
        }

        [Route("Library")]
        public IActionResult Library()
        {
            List<GameLibrary> gl = _dataService.GetGamesInLibrary(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            GamelibraryReviewModel glrm = new GamelibraryReviewModel();
            User u = _dataService.GetUser(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            glrm.gameLibraries = gl;
            glrm.user = u;
            glrm.Reviews = _dataService.GetUserReviews(u.Id);
            if (gl.Count == 0)
            {
                return View();
            }

            return View(glrm);
        }

        [Route("subscriptions")]
        public IActionResult Subscriptions()
        {
            List<Subscription> model = _dataService.GetSubscriptions();

            return View(model);
        }

        [HttpGet("uninstall-game/{id:int}")]
        public IActionResult UninstallGameConfirm(int id)
        {
            GameLibrary gl = _dataService.GetGameInLibrary(id);
            return View(gl);
        }

        [HttpPost("uninstall-game/{id:int}")]
        public IActionResult UninstallGame(int id)
        {
            _dataService.DeleteGameFromLibrary(id);

            return RedirectToAction(nameof(Library));
        }

        [HttpGet("view-plan/{id:int}")]
        public IActionResult ViewPlan(int id)
        {
            SubscriptionViewModel subscriptionViewModel = new SubscriptionViewModel();
            subscriptionViewModel.Subscription = _dataService.GetSubscription(id);
            subscriptionViewModel.Subscription.Plan = _dataService.GetPlan(subscriptionViewModel.Subscription.PlanId);
            return View(subscriptionViewModel);
        }
    }
}