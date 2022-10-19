using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GamerParadise.DataAccess.Data;
using GamerParadise.DataAccess.Models;
using GamerParadise.Models;
using GamerParadise.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GamerParadise.Controllers
{
    [Authorize]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppConfig _appConfig;
        private readonly DataService _dataService;

        public HomeController(ILogger<HomeController> logger, IOptions<AppConfig> appConfig, DataContext dataContext)
        {
            _logger = logger;
            _appConfig = appConfig.Value;
            _dataService = new DataService(dataContext);
        }

        [AllowAnonymous]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.ApplicationName = "Gamer Paradise";

            return View();
        }

        [AllowAnonymous]
        [HttpGet("support")]
        public IActionResult Support()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("support")]
        public IActionResult Support(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dataService.SubmitTicket(ticket);

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [Route("About")]
        public IActionResult About()
        {
            return View();
        }

        [Route("Game/{id:int}")]
        public IActionResult Game(int id)
        {
            User u = _dataService.GetUser(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            GameLibrary gl = _dataService.GetGameFromLibrary(id, u.Id);
            Game game = _dataService.GetGame(id);
            game.Developer = _dataService.GetDeveloper(game.DeveloperId);
            game.Publisher = _dataService.GetPublisher(game.PublisherId);
            game.Rating = _dataService.GetRating(game.RatingId);
            ReviewGameModel rgm = new ReviewGameModel();
            rgm.Game = game;
            rgm.Reviews = _dataService.GetGameReviews(id);
            rgm.user = u;
            rgm.gameLibrary = gl;
            return View(rgm);
        }

        [AllowAnonymous]
        [Route("Games")]
        public IActionResult Games(string sort, string filter, int? pageNumber)
        {
            List<Game> games = _dataService.GetGames();
            if (!String.IsNullOrEmpty(filter))
            {
                games = games
                    .Where(g => g.Name.ToLower().Contains(filter.ToLower())
                        || g.Description.ToLower().Contains(filter.ToLower())
                    )
                    .ToList();
            }
            switch (sort)
            {
                case "new":
                    games = games.Where(x => x.IsNew == true).ToList();
                    break;
                case "trending":
                    games = games.Where(x => x.IsTrending == true).ToList();
                    break;
                case "popular":
                    games = games.Where(x => x.IsPopular == true).ToList();
                    break;
                case "name_desc":
                    games = games.OrderByDescending(x => x.Name).ToList();
                    break;
                default:
                    games = games.OrderBy(x => x.Name).ToList();
                    break;
            }

            if (String.IsNullOrEmpty(sort) == true || sort == "trending" || sort == "popular" || sort == "new")
            {
                ViewData["NameSortParam"] = "name_desc";
            }
            else
            {
                ViewData["NameSortParam"] = "";
            }

            if (String.IsNullOrEmpty(sort) == true || sort == "trending" || sort == "popular" || sort == "name_desc")
            {
                ViewData["NewSortParam"] = "new";
            }
            else
            {
                ViewData["NewSortParam"] = "";
            }

            if (String.IsNullOrEmpty(sort) == true || sort == "trending" || sort == "new" || sort == "name_desc")
            {
                ViewData["PopularSortParam"] = "popular";
            }
            else
            {
                ViewData["PopularSortParam"] = "";
            }

            if (String.IsNullOrEmpty(sort) == true || sort == "new" || sort == "popular" || sort == "name_desc")
            {
                ViewData["TrendingSortParam"] = "trending";
            }
            else
            {
                ViewData["TrendingSortParam"] = "";
            }
            ViewData["Filter"] = filter;
            int pageSize = 8;

            return View(PaginatedList<Game>.Create(games, pageNumber ?? 1, pageSize));
        }

        [Authorize]
        [Route("profile")]
        public IActionResult Profile()
        {
            User user = _dataService.GetUser(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return View(user);
        }

        [Authorize]
        [Route("Subscription")]
        public IActionResult Subscription()
        {
            List<Plan> plans = _dataService.GetPlans();
            return View(plans);
        }

        [Authorize]
        [Route("Add-Subscription")]
        public IActionResult AddSubscription()
        {
            return View();
        }

        [Route("error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
