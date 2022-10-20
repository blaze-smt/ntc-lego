using GamerParadise.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using GamerParadise.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using System.IO;

namespace GamerParadise.DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }
        public DbSet<Game> Games { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<GameLibrary> GameLibraries { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Developer> Developers { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<GameGenre> GameGenres { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Each subscription has one user, and each user has 0 or 1 subscriptions.
            modelBuilder.Entity<User>().HasOne(u => u.Subscription).WithOne(s => s.User).HasForeignKey<Subscription>(u => u.UserId).OnDelete(DeleteBehavior.NoAction);

            // Each card has one user, and each user has many cards.
            modelBuilder.Entity<CreditCard>().HasOne(c => c.User).WithMany(u => u.Cards).HasForeignKey(c => c.UserId).IsRequired();

            // Each subscription has one card, and each card has many subscriptions.
            modelBuilder.Entity<Subscription>().HasOne(s => s.Card).WithMany(c => c.Subscriptions).HasForeignKey(s => s.CardId).IsRequired();

            // Each subscription has one plan, and each plan has many subscriptions.
            modelBuilder.Entity<Subscription>().HasOne(s => s.Plan).WithMany(p => p.Subscriptions).HasForeignKey(p => p.PlanId).IsRequired();

            // Each game library has one user, and each user has many game libraries.
            modelBuilder.Entity<GameLibrary>().HasOne(gl => gl.User).WithMany(u => u.GameLibraries).HasForeignKey(gl => gl.UserId).IsRequired();

            // Each game library has one game, and each game has many game libraries.
            modelBuilder.Entity<GameLibrary>().HasOne(gl => gl.Game).WithMany(g => g.GameLibraries).HasForeignKey(gl => gl.GameId).IsRequired();

            // Each gamegenre record has one game, and each game has many genres.
            modelBuilder.Entity<GameGenre>().HasOne(gg => gg.Game).WithMany(g => g.GameGenres).HasForeignKey(gg => gg.GameId).IsRequired();

            // Each gamegenre has one genre, and each genre has many gamegenres.
            modelBuilder.Entity<GameGenre>().HasOne(gg => gg.Genre).WithMany(ge => ge.GameGenres).HasForeignKey(gg => gg.GenreId).IsRequired();

            // Each review has one user, and each user has many reviews.
            modelBuilder.Entity<Review>().HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId).IsRequired();

            // Each review has one game, and each game has many reviews.
            modelBuilder.Entity<Review>().HasOne(r => r.Game).WithMany(g => g.Reviews).HasForeignKey(r => r.GameId).IsRequired();

            // Each game has one rating, and each rating has many games.
            modelBuilder.Entity<Game>().HasOne(g => g.Rating).WithMany(r => r.Games).HasForeignKey(g => g.RatingId).IsRequired();

            // Each game has one publisher, and each publisher has many games.
            modelBuilder.Entity<Game>().HasOne(g => g.Publisher).WithMany(p => p.Games).HasForeignKey(g => g.PublisherId).IsRequired();

            // Each game has one developer, and each developer has many games.
            modelBuilder.Entity<Game>().HasOne(g => g.Developer).WithMany(d => d.Games).HasForeignKey(g => g.DeveloperId).IsRequired();

            // Sample seed data for testing
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 1, Name = "Action", Description = "Intense high performance game that requires attention and focus.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 2, Name = "Platformer", Description = "A skill based left to right 2D jump game.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 3, Name = "Horror", Description = "A game intended to cause mild psychological discomfort and fear.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 4, Name = "Survival", Description = "Intended to challenge the player and force them to get creative to survive.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 5, Name = "Party", Description = "A fun local or online multiplayer with general fun and laughter.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 6, Name = "Multiplayer", Description = "Online competetive gaming experience in a wide range of environments.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 7, Name = "Shooter", Description = "Emphasizes the use of weaponry and explosives in combat with NPCs or online players.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 8, Name = "Simulation", Description = "Replicates the real life activity displayed in the game with much detail.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 9, Name = "Strategy", Description = "A game that forces you to get creative with solutions to outplay the enemy.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 10, Name = "Adventure", Description = "A generally story driven game that is often single player and involves choices.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 11, Name = "Co-op", Description = "A multiplayer game that promotes and encourages friends to work together to accomplish goals.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 12, Name = "Open World", Description = "A game that promotes freedom to roam in a large and expansive map with many opportunities and surpises.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 13, Name = "Building", Description = "A game with the aspect of building from a set starting point for the purpose of progress.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 14, Name = "Racing", Description = "A game featuring vehicles that compete to be the first to navigate some kind of track or obstacle.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 15, Name = "Sports", Description = "A game involving one or many different kinds of sports around the world.", IsArchived = false });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 16, Name = "RPG", Description = "A game in which you take over the life of a character and influence their and their world's outcome based on your decisions.", IsArchived = false });

            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 1, Name = "Hidden Path Entertainment", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 2, Name = "Nintendo EPD", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 3, Name = "Unknown Developer", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 4, Name = "Psyonix LLC", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 5, Name = "Rebuilt Games", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 6, Name = "NetherRealm Studios", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 7, Name = "DICE", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 8, Name = "Ubisoft", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 9, Name = "Rockstar Games", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 10, Name = "IO Interactive", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 11, Name = "Titan Forge Games", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 12, Name = "First Watch Games", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 13, Name = "Gearbox Software", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 14, Name = "Playground Games", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 15, Name = "Maxis", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 16, Name = "Colossal Order Ltd.", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 17, Name = "Firaxis Games", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 18, Name = "Paradox Development Studio", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 19, Name = "Fury Studios", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 20, Name = "Bungie", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 21, Name = "Relic Entertainment", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 22, Name = "Facepunch Studios", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 23, Name = "Visual Concepts", IsArchived = false });
            modelBuilder.Entity<Developer>().HasData(new Developer { DeveloperId = 24, Name = "Rare Ltd.", IsArchived = false });

            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 1, Name = "Microsoft", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 2, Name = "Nintendo", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 3, Name = "Unknown Publisher", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 4, Name = "Psyonix LLC", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 5, Name = "Rebuilt Games", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 6, Name = "Xbox Game Studios", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 7, Name = "Warner Bros Games", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 8, Name = "Electronic Arts", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 9, Name = "Ubisoft", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 10, Name = "Rockstar Games", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 11, Name = "IO Interactive", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 12, Name = "Hi-Rez Studios", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 13, Name = "2K", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 14, Name = "Paradox Interactive", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 15, Name = "Raw Fury", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 16, Name = "Bungie", IsArchived = false });
            modelBuilder.Entity<Publisher>().HasData(new Publisher { PublisherId = 17, Name = "Facepunch Studios", IsArchived = false });

            modelBuilder.Entity<Rating>().HasData(new Rating { RatingId = 1, Name = "Everyone", Description = "Content is generally suitable for all ages. May contain minimal cartoon, fantasy or mild violence and/or infrequent use of mild language.", IsArchived = false });
            modelBuilder.Entity<Rating>().HasData(new Rating { RatingId = 2, Name = "Mature 17+", Description = "Content is generally suitable for ages 17 and up. May contain intense violence, blood and gore, sexual content and/or strong language.", IsArchived = false });
            modelBuilder.Entity<Rating>().HasData(new Rating { RatingId = 3, Name = "Everyone 10+", Description = "Content is generally suitable for ages 10 and up. May contain more cartoon, fantasy or mild violence, mild language and/or minimal suggestive themes.", IsArchived = false });
            modelBuilder.Entity<Rating>().HasData(new Rating { RatingId = 4, Name = "Teen 13+", Description = "Content is generally suitable for ages 13 and up. May contain violence, suggestive themes, crude humor, minimal blood, simulated gambling and/or infrequent use of strong language.", IsArchived = false });
            modelBuilder.Entity<Rating>().HasData(new Rating { RatingId = 5, Name = "Rating Pending", Description = "Not yet assigned a final ESRB rating. Appears only in advertising, marketing and promotional materials related to a physical (e.g., boxed) video game that is expected to carry an ESRB rating, and should be replaced by a game's rating once it has been assigned.", IsArchived = false });
            modelBuilder.Entity<Rating>().HasData(new Rating { RatingId = 6, Name = "Adult 18+", Description = "Content suitable only for adults ages 18 and up. May include prolonged scenes of intense violence, graphic sexual content and/or gambling with real currency.", IsArchived = false });

            modelBuilder.Entity<Game>().HasData(new Game { GameId = 1, Name = "Rocket League", Description = "Rocket League is a high-powered hybrid of arcade-style soccer and vehicular mayhem with easy-to-understand controls and fluid, physics-driven competition.", IsTrending = true, IsPopular = true, IsNew = false, IsArchived = false, PublisherId = 4, DeveloperId = 4, RatingId = 1 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 2, Name = "Pummel Party", Description = "Pummel Party is a 4-8 player online and local-multiplayer party game. Pummel friends or AI using a wide array of absurd items in the board mode and compete to destroy friendships in the unique collection of minigames.", IsTrending = false, IsPopular = false, IsNew = false, IsArchived = false, PublisherId = 5, DeveloperId = 5, RatingId = 1 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 3, Name = "Age Of Empires II", Description = "Age of Empires II: Definitive Edition features “The Last Khans” with 3 new campaigns and 4 new Civilizations. Frequent updates include events, additional content, new game modes, and enhanced features with the recent addition of Co-Op mode!", IsTrending = false, IsPopular = false, IsNew = false, IsArchived = false, PublisherId = 6, DeveloperId = 1, RatingId = 4 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 4, Name = "Mortal Kombat 11", Description = "Mortal Kombat is back and better than ever in the next evolution of the iconic franchise.", IsTrending = false, IsPopular = false, IsNew = false, IsArchived = false, PublisherId = 7, DeveloperId = 6, RatingId = 2 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 5, Name = "Super Mario Bros", Description = "In this game you are playing the role of the Mario. You are going through the Mushroom Kingdom, surviving the forces of the antagonist Bowser, and saving Princess Toadstool. Mario should reach the flag pole at the end of each level to win the game.", IsTrending = false, IsPopular = true, IsNew = false, IsArchived = false, PublisherId = 2, DeveloperId = 2, RatingId = 1 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 6, Name = "Battlefield 2042", Description = "Battlefield™ 2042 is a first-person shooter that marks the return to the iconic all-out warfare of the franchise. Adapt and overcome in a near-future world transformed by disorder.", IsTrending = true, IsPopular = true, IsNew = true, IsArchived = false, PublisherId = 8, DeveloperId = 7, RatingId = 2 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 7, Name = "Far Cry 6", Description = "Dive into the gritty world of a modern-day guerrilla revolution to liberate a nation from its oppressive dictators.", IsTrending = true, IsPopular = true, IsNew = true, IsArchived = false, PublisherId = 9, DeveloperId = 8, RatingId = 2 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 8, Name = "Grand Theft Auto 5", Description = "When a young street hustler, a retired bank robber and a terrifying psychopath land themselves in trouble, they must pull off a series of dangerous heists to survive in a city in which they can trust nobody, least of all each other.", IsTrending = false, IsPopular = true, IsNew = false, IsArchived = false, PublisherId = 10, DeveloperId = 9, RatingId = 2 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 9, Name = "Hitman 3", Description = "Death Awaits. Agent 47 returns in HITMAN 3, the dramatic conclusion to the World of Assassination trilogy.", IsTrending = false, IsPopular = true, IsNew = false, IsArchived = false, PublisherId = 11, DeveloperId = 10, RatingId = 2 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 10, Name = "SMITE", Description = "Join 35+ million players in SMITE, the Battleground of the Gods! Wield Thor’s hammer, turn your foes to stone as Medusa, or flex your divine power as one of 100+ other mythological icons. Become a God.", IsTrending = false, IsPopular = false, IsNew = false, IsArchived = false, PublisherId = 12, DeveloperId = 11, RatingId = 4 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 11, Name = "Far Cry 4", Description = "You are Ajay Ghale. Traveling to Kyrat, you find yourself caught up in a civil war to overthrow the twisted dictator Pagan Min.", IsTrending = false, IsPopular = true, IsNew = false, IsArchived = false, PublisherId = 9, DeveloperId = 8, RatingId = 2 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 12, Name = "Rogue Company", Description = "Join 20+ million players in Rogue Company, the ultimate third-person tactical action shooter! Become an agent of Rogue Company and wield powerful weapons, high-tech gadgets, and game-changing abilities. Accept the mission and jump into a variety of 4v4 and 6v6 game modes.", IsTrending = true, IsPopular = false, IsNew = true, IsArchived = false, PublisherId = 12, DeveloperId = 12, RatingId = 5 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 13, Name = "Borderlands 3", Description = "The original shooter-looter returns, packing bazillions of guns and a mayhem-fueled adventure! Blast through new worlds & enemies and save your home from the most ruthless cult leaders in the galaxy.", IsTrending = false, IsPopular = false, IsNew = true, IsArchived = false, PublisherId = 13, DeveloperId = 13, RatingId = 2 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 14, Name = "Forza Horizon 5", Description = "Your Ultimate Horizon Adventure awaits! Explore the vibrant and ever-evolving open world landscapes of Mexico with limitless, fun driving action in hundreds of the world’s greatest cars.", IsTrending = false, IsPopular = false, IsNew = false, IsArchived = false, PublisherId = 6, DeveloperId = 14, RatingId = 1 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 15, Name = "The Sims 4", Description = "Play with life and discover the possibilities. Unleash your imagination and create a world of Sims that’s wholly unique. Explore and customize every detail from Sims to homes–and much more.", IsTrending = false, IsPopular = true, IsNew = false, IsArchived = false, PublisherId = 8, DeveloperId = 15, RatingId = 4 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 16, Name = "Cities: Skylines", Description = "Cities: Skylines is a modern take on the classic city simulation. The game introduces new game play elements to realize the thrill and hardships of creating and maintaining a real city whilst expanding on some well-established tropes of the city building experience.", IsTrending = false, IsPopular = false, IsNew = false, IsArchived = false, PublisherId = 14, DeveloperId = 16, RatingId = 1 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 17, Name = "Stardew Valley", Description = "You've inherited your grandfather's old farm plot in Stardew Valley. Armed with hand-me-down tools and a few coins, you set out to begin your new life. Can you learn to live off the land and turn these overgrown fields into a thriving home?", IsTrending = true, IsPopular = true, IsNew = false, IsArchived = false, PublisherId = 3, DeveloperId = 3, RatingId = 3 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 18, Name = "Sid Meier's Civilization VI", Description = "Civilization VI offers new ways to interact with your world, expand your empire across the map, advance your culture, and compete against history’s greatest leaders to build a civilization that will stand the test of time. Play as one of 20 historical leaders including Roosevelt (America) and Victoria (England).", IsTrending = true, IsPopular = false, IsNew = true, IsArchived = false, PublisherId = 13, DeveloperId = 17, RatingId = 3 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 19, Name = "Crusader Kings III", Description = "Paradox Development Studio brings you the sequel to one of the most popular strategy games ever made. Crusader Kings III is the heir to a long legacy of historical grand strategy experiences and arrives with a host of new ways to ensure the success of your royal house.", IsTrending = false, IsPopular = false, IsNew = true, IsArchived = false, PublisherId = 14, DeveloperId = 18, RatingId = 4 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 20, Name = "Kingdom Two Crowns", Description = "In Kingdom Two Crowns, players must work in the brand-new solo or co-op campaign mode to build their kingdom and secure it from the threat of the Greed. Experience new technology, units, enemies, mounts, and secrets in the next evolution of the award-winning micro strategy franchise!", IsTrending = false, IsPopular = false, IsNew = false, IsArchived = false, PublisherId = 15, DeveloperId = 19, RatingId = 3 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 21, Name = "Forza Horizon 4", Description = "Dynamic seasons change everything at the world’s greatest automotive festival. Go it alone or team up with others to explore beautiful and historic Britain in a shared open world.", IsTrending = false, IsPopular = true, IsNew = false, IsArchived = false, PublisherId = 6, DeveloperId = 14, RatingId = 1 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 22, Name = "Destiny 2", Description = "Destiny 2 is an action MMO with a single evolving world that you and your friends can join anytime, anywhere.", IsTrending = true, IsPopular = true, IsNew = false, IsArchived = false, PublisherId = 16, DeveloperId = 20, RatingId = 4 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 23, Name = "Age of Empires IV", Description = "One of the most beloved real-time strategy games returns to glory with Age of Empires IV, putting you at the center of epic historical battles that shaped the world.", IsTrending = true, IsPopular = true, IsNew = true, IsArchived = false, PublisherId = 6, DeveloperId = 21, RatingId = 4 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 24, Name = "Red Dead Redemption 2", Description = "Winner of over 175 Game of the Year Awards and recipient of over 250 perfect scores, RDR2 is the epic tale of outlaw Arthur Morgan and the infamous Van der Linde gang, on the run across America at the dawn of the modern age. Also includes access to the shared living world of Red Dead Online.", IsTrending = false, IsPopular = true, IsNew = false, IsArchived = false, PublisherId = 10, DeveloperId = 9, RatingId = 2 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 25, Name = "Rust", Description = "The only aim in Rust is to survive. Everything wants you to die - the island’s wildlife and other inhabitants, the environment, other survivors. Do whatever it takes to last another night.", IsTrending = false, IsPopular = true, IsNew = false, IsArchived = false, PublisherId = 17, DeveloperId = 22, RatingId = 2 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 26, Name = "NBA 2K22", Description = "NBA 2K22 puts the entire basketball universe in your hands. Anyone, anywhere can hoop in NBA 2K22.", IsTrending = false, IsPopular = false, IsNew = false, IsArchived = false, PublisherId = 13, DeveloperId = 23, RatingId = 1 });
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 27, Name = "Sea of Thieves", Description = "Sea of Thieves offers the essential pirate experience, from sailing and fighting to exploring and looting – everything you need to live the pirate life and become a legend in your own right. With no set roles, you have complete freedom to approach the world, and other players, however you choose.", IsTrending = false, IsPopular = true, IsNew = false, IsArchived = false, PublisherId = 6, DeveloperId = 24, RatingId = 4 });

            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 1, GenreId = 14, GameId = 1 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 2, GenreId = 15, GameId = 1 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 3, GenreId = 11, GameId = 2 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 4, GenreId = 6, GameId = 2 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 5, GenreId = 6, GameId = 3 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 6, GenreId = 9, GameId = 3 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 7, GenreId = 1, GameId = 4 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 8, GenreId = 6, GameId = 4 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 9, GenreId = 2, GameId = 5 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 10, GenreId = 1, GameId = 6 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 11, GenreId = 7, GameId = 6 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 12, GenreId = 6, GameId = 6 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 13, GenreId = 1, GameId = 7 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 14, GenreId = 7, GameId = 7 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 15, GenreId = 12, GameId = 7 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 16, GenreId = 10, GameId = 7 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 17, GenreId = 12, GameId = 8 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 18, GenreId = 1, GameId = 8 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 19, GenreId = 6, GameId = 8 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 20, GenreId = 1, GameId = 9 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 21, GenreId = 1, GameId = 10 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 22, GenreId = 6, GameId = 10 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 23, GenreId = 12, GameId = 11 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 24, GenreId = 1, GameId = 11 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 25, GenreId = 11, GameId = 11 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 26, GenreId = 10, GameId = 11 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 27, GenreId = 1, GameId = 12 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 28, GenreId = 6, GameId = 12 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 29, GenreId = 7, GameId = 12 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 30, GenreId = 16, GameId = 13 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 31, GenreId = 1, GameId = 13 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 32, GenreId = 7, GameId = 13 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 33, GenreId = 11, GameId = 13 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 34, GenreId = 10, GameId = 14 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 35, GenreId = 14, GameId = 14 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 36, GenreId = 12, GameId = 14 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 37, GenreId = 8, GameId = 15 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 38, GenreId = 13, GameId = 16 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 39, GenreId = 8, GameId = 16 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 40, GenreId = 16, GameId = 17 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 41, GenreId = 9, GameId = 18 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 42, GenreId = 9, GameId = 19 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 43, GenreId = 16, GameId = 19 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 44, GenreId = 8, GameId = 19 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 45, GenreId = 9, GameId = 20 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 46, GenreId = 10, GameId = 20 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 47, GenreId = 14, GameId = 21 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 48, GenreId = 12, GameId = 21 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 49, GenreId = 6, GameId = 21 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 50, GenreId = 12, GameId = 22 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 51, GenreId = 7, GameId = 22 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 52, GenreId = 6, GameId = 23 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 53, GenreId = 9, GameId = 23 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 54, GenreId = 12, GameId = 24 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 55, GenreId = 10, GameId = 24 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 56, GenreId = 4, GameId = 25 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 57, GenreId = 6, GameId = 25 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 58, GenreId = 15, GameId = 26 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 59, GenreId = 8, GameId = 26 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 60, GenreId = 6, GameId = 27 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 61, GenreId = 10, GameId = 27 });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre { GameGenreId = 62, GenreId = 12, GameId = 27 });

            modelBuilder.Entity<Plan>().HasData(new Plan { PlanId = 1, Name = "Classic", Description = "Our Classic plan will grant you access to thousands of games instantly and we will provide you a game library for up to 5 downloaded games at a time!", Price = 12.99m, MaxDownloads = 5, IsArchived = false, HasPersonalLibrary = true, RecievesNewsletter = true, HasAccessToAllGames = false, CanBetaTest = false, CanPlayOnTwoDevices = false });
            modelBuilder.Entity<Plan>().HasData(new Plan { PlanId = 2, Name = "Standard", Description = "Our Standard plan will grant you access to thousands of games instantly and we will provide you a game library for up to 10 downloaded games at a time! You will also get exclusive beta testing access to newly developing games!", Price = 16.99m, MaxDownloads = 10, IsArchived = false, HasPersonalLibrary = true, RecievesNewsletter = true, HasAccessToAllGames = true, CanBetaTest = true, CanPlayOnTwoDevices = false });
            modelBuilder.Entity<Plan>().HasData(new Plan { PlanId = 3, Name = "Ultimate", Description = "Our Ultimate plan will grant you access to thousands of games instantly and we will provide you a game library for up to 15 downloaded games at a time across multiple devices! You will also get exclusive beta testing access to newly developing games!", Price = 25.99m, MaxDownloads = 15, IsArchived = false, HasPersonalLibrary = true, RecievesNewsletter = true, HasAccessToAllGames = true, CanBetaTest = true, CanPlayOnTwoDevices = true });

            User sam = new User
            {
                Id = 1,
                Username = "UndeadWolf222",
                EmailAddress = "admin@gamerparadise.com",
                FirstName = "Sam",
                LastName = "Krahn",
                ProfileImage = null,
                IsAdmin = true,
                IsArchived = false
            };

            PasswordHasher<string> ph = new PasswordHasher<string>();
            sam.PasswordHash = ph.HashPassword(null, "12345");

            modelBuilder.Entity<User>().HasData(sam);

            User adam = new User
            {
                Id = 2,
                Username = "BlahBlahBlah",
                EmailAddress = "bobloblaw123@gmail.com",
                FirstName = "Bob",
                LastName = "Loblaw",
                ProfileImage = null,
                IsAdmin = true,
                IsArchived = false
            };

            adam.PasswordHash = ph.HashPassword(null, "blah123");

            modelBuilder.Entity<User>().HasData(adam);

            User isaiah = new User
            {
                Id = 3,
                Username = "godofgames44",
                EmailAddress = "icvabora@students.ntc.edu",
                FirstName = "Isaiah",
                LastName = "Vobora",
                ProfileImage = null,
                IsAdmin = true,
                IsArchived = false
            };

            isaiah.PasswordHash = ph.HashPassword(null, "12345");

            modelBuilder.Entity<User>().HasData(isaiah);

            base.OnModelCreating(modelBuilder);
        }
    }
}