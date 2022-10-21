using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using GamerParadise.DataAccess.Data;
using GamerParadise.DataAccess.Models;
using GamerParadise.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GamerParadise.Services
{
    public class DataService
    {
        private readonly DataContext _dataContext;

        public DataService(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public CreditCard AddCreditCard(CreditCard card)
        {
            _dataContext.CreditCards.Add(card);
            _dataContext.SaveChanges();
            return card;
        }

        public Developer AddDeveloper(Developer developer)
        {
            _dataContext.Developers.Add(developer);
            _dataContext.SaveChanges();
            return developer;
        }

        public Game AddGame(Game game)
        {
            _dataContext.Games.Add(game);
            _dataContext.SaveChanges();
            return game;
        }

        public Genre AddGenre(Genre genre)
        {
            _dataContext.Genres.Add(genre);
            _dataContext.SaveChanges();
            return genre;
        }

        public Plan AddPlan(Plan plan)
        {
            _dataContext.Plans.Add(plan);
            _dataContext.SaveChanges();
            return plan;
        }

        public Publisher AddPublisher(Publisher publisher)
        {
            _dataContext.Publishers.Add(publisher);
            _dataContext.SaveChanges();
            return publisher;
        }

        public Rating AddRating(Rating rating)
        {
            _dataContext.Ratings.Add(rating);
            _dataContext.SaveChanges();
            return rating;
        }

        public Subscription AddSubscription(Subscription subscription)
        {
            _dataContext.Subscriptions.Add(subscription);
            _dataContext.SaveChanges();
            return subscription;
        }

        public User AddUser(User user)
        {
            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
            return user;
        }

        public Subscription CancelSubscription(Subscription subscription)
        {
            subscription.IsActive = false;
            UpdateSubscription(subscription);
            return subscription;
        }

        public CreditCard DeleteCreditCard(int id)
        {
            CreditCard creditCard = _dataContext.CreditCards.Where(x => x.CardId == id).FirstOrDefault();
            _dataContext.CreditCards.Remove(creditCard);
            _dataContext.SaveChanges();
            return creditCard;
        }

        public Developer DeleteDeveloper(int id)
        {
            Developer developer = _dataContext.Developers.Where(x => x.DeveloperId == id).FirstOrDefault();
            developer.IsArchived = true;
            UpdateDeveloper(developer);
            return developer;
        }

        public Genre DeleteGenre(int id)
        {
            Genre genre = _dataContext.Genres.Where(x => x.GenreId == id).FirstOrDefault();
            genre.IsArchived = true;
            UpdateGenre(genre);
            return genre;
        }

        public Plan DeletePlan(int id)
        {
            Plan plan = _dataContext.Plans.Where(x => x.PlanId == id).FirstOrDefault();
            plan.IsArchived = true;
            UpdatePlan(plan);
            return plan;
        }

        public Publisher DeletePublisher(int id)
        {
            Publisher publisher = _dataContext.Publishers.Where(x => x.PublisherId == id).FirstOrDefault();
            publisher.IsArchived = true;
            UpdatePublisher(publisher);
            return publisher;
        }

        public Rating DeleteRating(int id)
        {
            Rating rating = _dataContext.Ratings.Where(x => x.RatingId == id).FirstOrDefault();
            rating.IsArchived = true;
            UpdateRating(rating);
            return rating;
        }

        public CreditCard GetCreditCard(int id)
        {
            return _dataContext.CreditCards.FirstOrDefault(x => x.CardId == id);
        }

        public List<CreditCard> GetCreditCards()
        {
            return _dataContext.CreditCards.ToList();
        }

        public List<Developer> GetDeletedDevelopers()
        {
            return _dataContext.Developers.Where(d => d.IsArchived).ToList();
        }

        public List<Genre> GetDeletedGenres()
        {
            return _dataContext.Genres.Where(g => g.IsArchived).ToList();
        }

        public List<Plan> GetDeletedPlans()
        {
            return _dataContext.Plans.Where(p => p.IsArchived).ToList();
        }

        public List<Publisher> GetDeletedPublishers()
        {
            return _dataContext.Publishers.Where(p => p.IsArchived).ToList();
        }

        public List<Rating> GetDeletedRatings()
        {
            return _dataContext.Ratings.Where(r => r.IsArchived).ToList();
        }

        public Developer GetDeveloper(int id)
        {
            return _dataContext.Developers.FirstOrDefault(x => x.DeveloperId == id);
        }

        public List<Developer> GetDevelopers()
        {
            return _dataContext.Developers.Where(x => x.IsArchived == false).ToList();
        }

        public Game GetGame(int id)
        {
            return _dataContext.Games.Include(g => g.GameGenres).ThenInclude(gg => gg.Genre)
            .Include(g => g.Developer).Include(g => g.Publisher).Include(g => g.Rating).FirstOrDefault(g => g.GameId == id);
        }

        public List<Game> GetGames()
        {
            return _dataContext.Games.Include(x => x.GameGenres).ThenInclude(x => x.Genre).Include(x => x.Reviews)
            .Include(x => x.Developer).Include(x => x.Publisher).Include(x => x.Rating).Where(x => x.IsArchived == false).ToList();
        }

        public List<Genre> GetGenres()
        {
            return _dataContext.Genres.Where(x => x.IsArchived == false).ToList();
        }

        public Genre GetGenre(int id)
        {
            return _dataContext.Genres.FirstOrDefault(x => x.GenreId == id);
        }

        public Publisher GetPublisher(int id)
        {
            return _dataContext.Publishers.FirstOrDefault(x => x.PublisherId == id);
        }

        public List<Publisher> GetPublishers()
        {
            return _dataContext.Publishers.Where(x => x.IsArchived == false).ToList();
        }

        public Plan GetPlan(int id)
        {
            return _dataContext.Plans.FirstOrDefault(p => p.PlanId == id);
        }

        public List<Plan> GetPlans()
        {
            return _dataContext.Plans.Where(p => p.IsArchived == false).ToList();
        }

        public Rating GetRating(int id)
        {
            return _dataContext.Ratings.FirstOrDefault(x => x.RatingId == id);
        }

        public List<Rating> GetRatings()
        {
            return _dataContext.Ratings.Where(x => x.IsArchived == false).ToList();
        }

        public Subscription GetSubscription(int id)
        {
            return _dataContext.Subscriptions.FirstOrDefault(x => x.SubscriptionId == id);
        }

        public List<Subscription> GetSubscriptions()
        {
            return _dataContext.Subscriptions.Where(x => x.IsActive == true).ToList();
        }

        public User GetUser(string email)
        {
            return _dataContext.Users.FirstOrDefault(e => e.EmailAddress.ToLower() == email.ToLower());
        }

        public User GetUser(int id)
        {
            return _dataContext.Users.Include(x => x.Cards).Include(x => x.Subscription)
                .ThenInclude(x => x.Plan).Include(x => x.Reviews)
                .FirstOrDefault(x => x.Id == id);
        }

        public void RestoreDeveloper(int id)
        {
            Developer d = _dataContext.Developers.FirstOrDefault(p => p.DeveloperId == id);
            d.IsArchived = false;
            this.UpdateDeveloper(d);
        }

        public void RestoreGenre(int id)
        {
            Genre g = _dataContext.Genres.FirstOrDefault(g => g.GenreId == id);
            g.IsArchived = false;
            this.UpdateGenre(g);
        }

        public void RestorePlan(int id)
        {
            Plan p = _dataContext.Plans.FirstOrDefault(p => p.PlanId == id);
            p.IsArchived = false;
            this.UpdatePlan(p);
        }

        public void RestorePublisher(int id)
        {
            Publisher p = _dataContext.Publishers.FirstOrDefault(p => p.PublisherId == id);
            p.IsArchived = false;
            this.UpdatePublisher(p);
        }

        public void RestoreRating(int id)
        {
            Rating r = _dataContext.Ratings.FirstOrDefault(r => r.RatingId == id);
            r.IsArchived = false;
            this.UpdateRating(r);
        }

        public CreditCard UpdateCreditCard(CreditCard creditCard)
        {
            _dataContext.CreditCards.Update(creditCard);
            _dataContext.SaveChanges();
            return creditCard;
        }

        public Developer UpdateDeveloper(Developer developer)
        {
            _dataContext.Developers.Update(developer);
            _dataContext.SaveChanges();
            return developer;
        }

        public void UpdateGame(Game game)
        {
            _dataContext.Games.Update(game);
            _dataContext.SaveChanges();
        }

        public Genre UpdateGenre(Genre genre)
        {
            _dataContext.Genres.Update(genre);
            _dataContext.SaveChanges();
            return genre;
        }

        public Plan UpdatePlan(Plan plan)
        {
            _dataContext.Plans.Update(plan);
            _dataContext.SaveChanges();
            return plan;
        }

        public Publisher UpdatePublisher(Publisher publisher)
        {
            _dataContext.Publishers.Update(publisher);
            _dataContext.SaveChanges();
            return publisher;
        }

        public Rating UpdateRating(Rating rating)
        {
            _dataContext.Ratings.Update(rating);
            _dataContext.SaveChanges();
            return rating;
        }

        public void UpdateSubscription(Subscription subscription)
        {
            _dataContext.Subscriptions.Update(subscription);
            _dataContext.SaveChanges();
        }

        public List<Review> GetReviews()
        {
            return _dataContext.Reviews.Where(x => x.IsArchived == false).Include(x => x.Game)
                .Include(x => x.User).Include(x => x.Game).ToList();
        }

        public List<Review> GetGameReviews(int id)
        {
            return _dataContext.Reviews.Include(x => x.User).Where(x => x.GameId == id).ToList();
        }

        public Review GetReview(int reviewId)
        {
            return _dataContext.Reviews.Where(x => x.ReviewId == reviewId)
                .Include(x => x.Game).Include(x => x.User).FirstOrDefault();
        }

        public Review GetUserGameReview(int userId, int gameId)
        {
            return _dataContext.Reviews.Where(x => x.UserId == userId).Where(x => x.GameId == gameId)
                .Include(x => x.User).Include(x => x.Game).FirstOrDefault();
        }

        public bool GetUserReviewTruth(int userId, int gameId)
        {
            if(_dataContext.Reviews.Where(x => x.UserId == userId).Where(x => x.GameId == gameId) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Review DeleteReview(int id)
        {
            Review r = _dataContext.Reviews.Where(x => x.ReviewId == id).FirstOrDefault();
            _dataContext.Reviews.Remove(r);
            _dataContext.SaveChanges();
            return r;
        }

        public Review UpdateReview(Review review)
        {
            _dataContext.Reviews.Update(review);
            _dataContext.SaveChanges();
            return review;
        }

        public Review AddReview(Review review)
        {
            _dataContext.Reviews.Add(review);
            _dataContext.SaveChanges();
            return review;
        }

        public List<Review> GetUserReviews(int userId)
        {
            return _dataContext.Reviews.Where(x => x.UserId == userId).Include(x => x.Game)
                        .Include(x => x.User).ToList();
        }

        public bool AddGameToLibrary(User user, int gameId)
        {
            if (user.Subscription != null) // TODO specific subscription logic
            {
                Game g = GetGame(gameId);
                GameLibrary gl = new GameLibrary();
                gl.GameId = gameId;
                gl.UserId = user.Id;
                _dataContext.GameLibraries.Add(gl);
                _dataContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<GameLibrary> GetGamesInLibrary(int userId)
        {
            List<GameLibrary> gl =
             _dataContext.GameLibraries
                .Where(x => x.UserId == userId)
                .Include(x => x.Game)
                .Include(x => x.Game.Rating)
                .ToList();
            return gl;
        }


        public GameLibrary GetGameInLibrary(int id)
        {
            return _dataContext.GameLibraries.Include(x => x.Game).FirstOrDefault(x => x.GameLibraryId == id);
        }

        public GameLibrary GetGameFromLibrary(int id, int userId)
        {
            return _dataContext.GameLibraries.Where(x => x.GameId == id).Where(x => x.UserId == userId).Include(x => x.Game).FirstOrDefault();
        }

        public void DeleteGameFromLibrary(int id)
        {
            GameLibrary gl = _dataContext.GameLibraries.Where(x => x.GameLibraryId == id).FirstOrDefault();
            _dataContext.GameLibraries.Remove(gl);
            _dataContext.SaveChanges();
        }

        public Ticket SubmitTicket(Ticket ticket)
        {
            _dataContext.Tickets.Add(ticket);
            _dataContext.SaveChanges();
            return ticket;
        }

        public List<Ticket> GetTickets()
        {
            return _dataContext.Tickets.Where(x => x.Responded == false).ToList();
        }

        public Ticket GetTicket(int ticketId)
        {
            return _dataContext.Tickets.FirstOrDefault(x => x.TicketId == ticketId);
        }

        public void RespondToTicket(Ticket ticket)
        {
            _dataContext.Tickets.Update(ticket);
            _dataContext.SaveChanges();
        }

        public Game DeleteGame(int gameId)
        {
            Game game = _dataContext.Games.Where(x => x.GameId == gameId).FirstOrDefault();
            game.IsArchived = true;
            UpdateGame(game);
            return game;
        }

        public void RestoreGame(int gameId)
        {
            Game g = _dataContext.Games.FirstOrDefault(g => g.GameId == gameId);
            g.IsArchived = false;
            UpdateGame(g);
        }

        public List<Game> GetDeletedGames()
        {
            return _dataContext.Games.Where(g => g.IsArchived).ToList();
        }

        public void UpdateUser(User user)
        {
            _dataContext.Users.Update(user);
            _dataContext.SaveChanges();
        }
    }
}