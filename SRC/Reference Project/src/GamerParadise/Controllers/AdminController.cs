using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GamerParadise.DataAccess.Data;
using GamerParadise.DataAccess.Models;
using GamerParadise.Services;
using GamerParadise.Models;

namespace GamerParadise.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppConfig _appConfig;
        private readonly DataService _dataService;

        public AdminController(ILogger<HomeController> logger, IOptions<AppConfig> appConfig, DataContext dataContext)
        {
            _logger = logger;
            _appConfig = appConfig.Value;
            _dataService = new DataService(dataContext);
        }

        [HttpGet("add-developer")]
        public ActionResult AddDeveloper()
        {
            return View();
        }

        [HttpPost("add-developer")]
        public ActionResult AddDeveloper(Developer developer)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dataService.AddDeveloper(developer);

            return RedirectToAction(nameof(Developers));
        }

        [HttpGet("add-game")]
        public IActionResult AddGame()
        {
            GameViewModel gameViewModel = new GameViewModel();
            gameViewModel.AllGenres = _dataService.GetGenres();
            gameViewModel.Publishers = _dataService.GetPublishers();
            gameViewModel.Developers = _dataService.GetDevelopers();
            gameViewModel.Ratings = _dataService.GetRatings();
            return View(gameViewModel);
        }

        [HttpPost("add-game")]
        public IActionResult AddGame(GameViewModel gameViewModel)
        {
            if (!ModelState.IsValid)
            {
                gameViewModel.AllGenres = _dataService.GetGenres();
                gameViewModel.Publishers = _dataService.GetPublishers();
                gameViewModel.Developers = _dataService.GetDevelopers();
                gameViewModel.Ratings = _dataService.GetRatings();

                return View(gameViewModel);
            }

            if (gameViewModel.SelectedGameGenreIds != null)
            {
                gameViewModel.Game.GameGenres = new List<GameGenre>();
                foreach (int gameGenreId in gameViewModel.SelectedGameGenreIds)
                {
                    gameViewModel.Game.GameGenres.Add(new GameGenre { GenreId = gameGenreId });
                }
            }

            _dataService.AddGame(gameViewModel.Game);

            return RedirectToAction(nameof(Games));
        }

        [HttpGet("add-genre")]
        public ActionResult AddGenre()
        {
            return View();
        }

        [HttpPost("add-genre")]
        public ActionResult AddGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dataService.AddGenre(genre);

            return RedirectToAction(nameof(Genres));
        }

        [HttpGet("add-plan")]
        public ActionResult AddPlan()
        {
            return View();
        }

        [HttpPost("add-plan")]
        public ActionResult AddPlan(Plan plan)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dataService.AddPlan(plan);

            return RedirectToAction(nameof(Plans));
        }

        [HttpGet("add-publisher")]
        public ActionResult AddPublisher()
        {
            return View();
        }

        [HttpPost("add-publisher")]
        public ActionResult AddPublisher(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dataService.AddPublisher(publisher);

            return RedirectToAction(nameof(Publishers));
        }

        [HttpGet("add-rating")]
        public ActionResult AddRating()
        {
            return View();
        }

        [HttpPost("add-rating")]
        public ActionResult AddRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dataService.AddRating(rating);

            return RedirectToAction(nameof(Ratings));
        }

        [HttpPost("delete-developer/{id:int}")]
        public IActionResult DeleteDeveloper(int id)
        {
            _dataService.DeleteDeveloper(id);

            return RedirectToAction(nameof(Developers));
        }

        [HttpGet("delete-developer/{id:int}")]
        public IActionResult DeleteDeveloperConfirm(int id)
        {
            Developer developer = _dataService.GetDeveloper(id);
            return View(developer);
        }

        [HttpPost("delete-genre/{id:int}")]
        public IActionResult DeleteGenre(int id)
        {
            _dataService.DeleteGenre(id);

            return RedirectToAction(nameof(Genres));
        }

        [HttpGet("delete-genre/{id:int}")]
        public IActionResult DeleteGenreConfirm(int id)
        {
            Genre genre = _dataService.GetGenre(id);
            return View(genre);
        }

        [HttpPost("delete-plan/{id:int}")]
        public IActionResult DeletePlan(int id)
        {
            _dataService.DeletePlan(id);

            return RedirectToAction(nameof(Plans));
        }

        [HttpGet("delete-plan/{id:int}")]
        public IActionResult DeletePlanConfirm(int id)
        {
            Plan plan = _dataService.GetPlan(id);
            return View(plan);
        }

        [HttpPost("delete-rating/{id:int}")]
        public IActionResult DeleteRating(int id)
        {
            _dataService.DeleteRating(id);

            return RedirectToAction(nameof(Ratings));
        }

        [HttpGet("delete-rating/{id:int}")]
        public IActionResult DeleteRatingConfirm(int id)
        {
            Rating rating = _dataService.GetRating(id);
            return View(rating);
        }

        [HttpPost("delete-publisher/{id:int}")]
        public IActionResult DeletePublisher(int id)
        {
            _dataService.DeletePublisher(id);

            return RedirectToAction(nameof(Publishers));
        }

        [HttpGet("delete-publisher/{id:int}")]
        public IActionResult DeletePublisherConfirm(int id)
        {
            Publisher publisher = _dataService.GetPublisher(id);
            return View(publisher);
        }

        [Route("developers")]
        public IActionResult Developers()
        {
            List<Developer> model = _dataService.GetDevelopers();

            return View(model);
        }

        [HttpGet("edit-developer/{id:int}")]
        public IActionResult EditDeveloper(int id)
        {
            Developer model = _dataService.GetDeveloper(id);
            return View(model);
        }

        [HttpPost("edit-developer/{id:int}")]
        public IActionResult EditDeveloper(Developer developer)
        {
            if (!ModelState.IsValid)
            {
                return View(developer);
            }
            _dataService.UpdateDeveloper(developer);

            return RedirectToAction(nameof(Developers));
        }

        [HttpGet("edit-game/{id:int}")]
        public IActionResult EditGame(int id)
        {
            GameViewModel gameViewModel = new GameViewModel();
            gameViewModel.AllGenres = _dataService.GetGenres();
            gameViewModel.Publishers = _dataService.GetPublishers();
            gameViewModel.Developers = _dataService.GetDevelopers();
            gameViewModel.Ratings = _dataService.GetRatings();
            gameViewModel.Game = _dataService.GetGame(id);
            gameViewModel.SelectedGameGenreIds = new List<int>();
            foreach (GameGenre gg in gameViewModel.Game.GameGenres)
            {
                gameViewModel.SelectedGameGenreIds.Add(gg.GenreId);
            }
            return View(gameViewModel);
        }

        [HttpPost("edit-game/{id:int}")]
        public IActionResult EditGame(GameViewModel gameViewModel)
        {
            if (!ModelState.IsValid)
            {
                Game dbGame = _dataService.GetGame(gameViewModel.Game.GameId);

                gameViewModel.SelectedGameGenreIds = new List<int>();
                foreach (GameGenre gg in dbGame.GameGenres)
                {
                    gameViewModel.SelectedGameGenreIds.Add(gg.GenreId);
                }
                gameViewModel.Publishers = _dataService.GetPublishers();
                gameViewModel.Developers = _dataService.GetDevelopers();
                gameViewModel.Ratings = _dataService.GetRatings();
                gameViewModel.AllGenres = _dataService.GetGenres();
                gameViewModel.Game = dbGame;
                return View(gameViewModel);
            }

            if (gameViewModel.SelectedGameGenreIds != null)
            {
                gameViewModel.Game.GameGenres = new List<GameGenre>();
                foreach (int gameGenreId in gameViewModel.SelectedGameGenreIds)
                {
                    gameViewModel.Game.GameGenres.Add(new GameGenre { GenreId = gameGenreId });
                }
            }

            _dataService.UpdateGame(gameViewModel.Game);

            return RedirectToAction(nameof(Games));
        }

        [HttpGet("edit-genre/{id:int}")]
        public IActionResult EditGenre(int id)
        {
            Genre model = _dataService.GetGenre(id);
            return View(model);
        }

        [HttpPost("edit-genre/{id:int}")]
        public IActionResult EditGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }
            _dataService.UpdateGenre(genre);

            return RedirectToAction(nameof(Genres));
        }

        [HttpGet("edit-plan/{id:int}")]
        public IActionResult EditPlan(int id)
        {
            Plan model = _dataService.GetPlan(id);
            return View(model);
        }

        [HttpPost("edit-plan/{id:int}")]
        public IActionResult EditPlan(Plan plan)
        {
            if (!ModelState.IsValid)
            {
                return View(plan);
            }
            _dataService.UpdatePlan(plan);

            return RedirectToAction(nameof(Plans));
        }

        [HttpGet("edit-publisher/{id:int}")]
        public IActionResult EditPublisher(int id)
        {
            Publisher model = _dataService.GetPublisher(id);
            return View(model);
        }

        [HttpPost("edit-publisher/{id:int}")]
        public IActionResult EditPublisher(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);
            }
            _dataService.UpdatePublisher(publisher);

            return RedirectToAction(nameof(Publishers));
        }

        [HttpGet("edit-rating/{id:int}")]
        public IActionResult EditRating(int id)
        {
            Rating model = _dataService.GetRating(id);
            return View(model);
        }

        [HttpPost("edit-rating/{id:int}")]
        public IActionResult EditRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return View(rating);
            }
            _dataService.UpdateRating(rating);

            return RedirectToAction(nameof(Ratings));
        }

        [Route("games")]
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

        [Route("tickets")]
        public IActionResult Tickets()
        {
            List<Ticket> tickets = _dataService.GetTickets();
            return View(tickets);
        }

        [HttpPost("ticket/{id:int}")]
        public IActionResult RespondToTicket(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Ticket ticket = _dataService.GetTicket(id);
            ticket.Responded = true;
            _dataService.RespondToTicket(ticket);

            return RedirectToAction(nameof(Tickets));
        }

        [Route("genres")]
        public IActionResult Genres()
        {
            List<Genre> model = _dataService.GetGenres();

            return View(model);
        }

        [Route("plans")]
        public IActionResult Plans()
        {
            List<Plan> model = _dataService.GetPlans();

            return View(model);
        }

        [Route("publishers")]
        public IActionResult Publishers()
        {
            List<Publisher> model = _dataService.GetPublishers();

            return View(model);
        }

        [HttpGet("restore-developer")]
        public IActionResult RestoreDeveloper()
        {
            List<Developer> deletedDevelopers = _dataService.GetDeletedDevelopers();
            return View(deletedDevelopers);
        }

        [HttpGet("restore-developer/{id:int}")]
        public IActionResult RestoreDeveloper(int id)
        {
            _dataService.RestoreDeveloper(id);
            return RedirectToAction(nameof(Developers));
        }

        [HttpGet("restore-publisher")]
        public IActionResult RestorePublisher()
        {
            List<Publisher> deletedPublishers = _dataService.GetDeletedPublishers();
            return View(deletedPublishers);
        }

        [HttpGet("restore-publisher/{id:int}")]
        public IActionResult RestorePublisher(int id)
        {
            _dataService.RestorePublisher(id);
            return RedirectToAction(nameof(Publishers));
        }

        [HttpGet("restore-genre")]
        public IActionResult RestoreGenre()
        {
            List<Genre> deletedGenres = _dataService.GetDeletedGenres();
            return View(deletedGenres);
        }

        [HttpGet("restore-genre/{id:int}")]
        public IActionResult RestoreGenre(int id)
        {
            _dataService.RestoreGenre(id);
            return RedirectToAction(nameof(Genres));
        }

        [Route("ratings")]
        public IActionResult Ratings()
        {
            List<Rating> model = _dataService.GetRatings();

            return View(model);
        }

        [HttpGet("restore-plan")]
        public IActionResult RestorePlan()
        {
            List<Plan> deletedPlans = _dataService.GetDeletedPlans();
            return View(deletedPlans);
        }

        [HttpGet("restore-plan/{id:int}")]
        public IActionResult RestorePlan(int id)
        {
            _dataService.RestorePlan(id);
            return RedirectToAction(nameof(Plans));
        }

        [HttpGet("restore-rating")]
        public IActionResult RestoreRating()
        {
            List<Rating> deletedRatings = _dataService.GetDeletedRatings();
            return View(deletedRatings);
        }

        [HttpGet("restore-rating/{id:int}")]
        public IActionResult RestoreRating(int id)
        {
            _dataService.RestoreRating(id);
            return RedirectToAction(nameof(Ratings));
        }

        [HttpGet("delete-game/{id:int}")]
        public IActionResult DeleteGameConfirm(int id)
        {
            Game game = _dataService.DeleteGame(id);
            return View(game);
        }

        [HttpPost("delete-game/{id:int}")]
        public IActionResult DeleteGame(int id)
        {
            _dataService.DeleteGame(id);

            return RedirectToAction(nameof(Games));
        }

        [HttpGet("restore-game")]
        public IActionResult RestoreGame()
        {
            List<Game> deletedGames = _dataService.GetDeletedGames();
            return View(deletedGames);
        }

        [HttpGet("restore-game/{id:int}")]
        public IActionResult RestoreGame(int id)
        {
            _dataService.RestoreGame(id);
            return RedirectToAction(nameof(Games));
        }

        [Route("Reviews")]
        public IActionResult Reviews()
        {
            return View(_dataService.GetReviews());
        }

        [HttpGet("edit-review/{id:int}")]
        public IActionResult EditReview(int id)
        {
            Review r = _dataService.GetReview(id);
            return View(r);
        }

        [HttpPost("edit-review/{id:int}")]
        public IActionResult EditReview(Review r)
        {
            if (!ModelState.IsValid)
            {
                return View(r);
            }
            _dataService.UpdateReview(r);

            return RedirectToAction(nameof(Reviews));
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

            return RedirectToAction(nameof(Reviews));
        }
    }
}