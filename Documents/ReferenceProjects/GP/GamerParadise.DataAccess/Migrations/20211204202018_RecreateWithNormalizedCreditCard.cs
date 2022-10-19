using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamerParadise.DataAccess.Migrations
{
    public partial class RecreateWithNormalizedCreditCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    DeveloperId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.DeveloperId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    PlanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    MaxDownloads = table.Column<byte>(type: "tinyint", nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    HasPersonalLibrary = table.Column<bool>(nullable: false),
                    RecievesNewsletter = table.Column<bool>(nullable: false),
                    HasAccessToAllGames = table.Column<bool>(nullable: false),
                    CanBetaTest = table.Column<bool>(nullable: false),
                    CanPlayOnTwoDevices = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    PublisherId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.PublisherId);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 255, nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Responded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 255, nullable: false),
                    ProfileImage = table.Column<byte[]>(nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsTrending = table.Column<bool>(type: "bit", nullable: false),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    PublisherId = table.Column<int>(nullable: false),
                    DeveloperId = table.Column<int>(nullable: false),
                    RatingId = table.Column<int>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "DeveloperId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    CardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CSC = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    NameOnCard = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CardNum = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExpirationMonth = table.Column<int>(nullable: false),
                    ExpirationYear = table.Column<int>(nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    State = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_CreditCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameGenres",
                columns: table => new
                {
                    GameGenreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenres", x => x.GameGenreId);
                    table.ForeignKey(
                        name: "FK_GameGenres_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameLibraries",
                columns: table => new
                {
                    GameLibraryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLibraries", x => x.GameLibraryId);
                    table.ForeignKey(
                        name: "FK_GameLibraries_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameLibraries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StarCount = table.Column<byte>(type: "tinyint", nullable: false),
                    DateTimeWritten = table.Column<DateTime>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    CardId = table.Column<int>(nullable: false),
                    PlanId = table.Column<int>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Downloads = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.SubscriptionId);
                    table.ForeignKey(
                        name: "FK_Subscriptions_CreditCards_CardId",
                        column: x => x.CardId,
                        principalTable: "CreditCards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "DeveloperId", "IsArchived", "Name" },
                values: new object[,]
                {
                    { 1, false, "Hidden Path Entertainment" },
                    { 24, false, "Rare Ltd." },
                    { 23, false, "Visual Concepts" },
                    { 22, false, "Facepunch Studios" },
                    { 21, false, "Relic Entertainment" },
                    { 20, false, "Bungie" },
                    { 19, false, "Fury Studios" },
                    { 18, false, "Paradox Development Studio" },
                    { 17, false, "Firaxis Games" },
                    { 15, false, "Maxis" },
                    { 14, false, "Playground Games" },
                    { 13, false, "Gearbox Software" },
                    { 16, false, "Colossal Order Ltd." },
                    { 11, false, "Titan Forge Games" },
                    { 2, false, "Nintendo EPD" },
                    { 3, false, "Unknown Developer" },
                    { 12, false, "First Watch Games" },
                    { 5, false, "Rebuilt Games" },
                    { 6, false, "NetherRealm Studios" },
                    { 4, false, "Psyonix LLC" },
                    { 8, false, "Ubisoft" },
                    { 9, false, "Rockstar Games" },
                    { 10, false, "IO Interactive" },
                    { 7, false, "DICE" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "Description", "IsArchived", "Name" },
                values: new object[,]
                {
                    { 10, "A generally story driven game that is often single player and involves choices.", false, "Adventure" },
                    { 16, "A game in which you take over the life of a character and influence their and their world's outcome based on your decisions.", false, "RPG" },
                    { 15, "A game involving one or many different kinds of sports around the world.", false, "Sports" },
                    { 14, "A game featuring vehicles that compete to be the first to navigate some kind of track or obstacle.", false, "Racing" },
                    { 13, "A game with the aspect of building from a set starting point for the purpose of progress.", false, "Building" },
                    { 12, "A game that promotes freedom to roam in a large and expansive map with many opportunities and surpises.", false, "Open World" },
                    { 9, "A game that forces you to get creative with solutions to outplay the enemy.", false, "Strategy" },
                    { 11, "A multiplayer game that promotes and encourages friends to work together to accomplish goals.", false, "Co-op" },
                    { 7, "Emphasizes the use of weaponry and explosives in combat with NPCs or online players.", false, "Shooter" },
                    { 1, "Intense high performance game that requires attention and focus.", false, "Action" },
                    { 2, "A skill based left to right 2D jump game.", false, "Platformer" },
                    { 3, "A game intended to cause mild psychological discomfort and fear.", false, "Horror" },
                    { 8, "Replicates the real life activity displayed in the game with much detail.", false, "Simulation" },
                    { 5, "A fun local or online multiplayer with general fun and laughter.", false, "Party" },
                    { 6, "Online competetive gaming experience in a wide range of environments.", false, "Multiplayer" },
                    { 4, "Intended to challenge the player and force them to get creative to survive.", false, "Survival" }
                });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "PlanId", "CanBetaTest", "CanPlayOnTwoDevices", "Description", "HasAccessToAllGames", "HasPersonalLibrary", "IsArchived", "MaxDownloads", "Name", "Price", "RecievesNewsletter" },
                values: new object[,]
                {
                    { 1, false, false, "Our Classic plan will grant you access to thousands of games instantly and we will provide you a game library for up to 5 downloaded games at a time!", false, true, false, (byte)5, "Classic", 12.99m, true },
                    { 3, true, true, "Our Ultimate plan will grant you access to thousands of games instantly and we will provide you a game library for up to 15 downloaded games at a time across multiple devices! You will also get exclusive beta testing access to newly developing games!", true, true, false, (byte)15, "Ultimate", 25.99m, true },
                    { 2, true, false, "Our Standard plan will grant you access to thousands of games instantly and we will provide you a game library for up to 10 downloaded games at a time! You will also get exclusive beta testing access to newly developing games!", true, true, false, (byte)10, "Standard", 16.99m, true }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PublisherId", "IsArchived", "Name" },
                values: new object[,]
                {
                    { 17, false, "Facepunch Studios" },
                    { 10, false, "Rockstar Games" },
                    { 16, false, "Bungie" },
                    { 15, false, "Raw Fury" },
                    { 14, false, "Paradox Interactive" },
                    { 13, false, "2K" },
                    { 12, false, "Hi-Rez Studios" },
                    { 11, false, "IO Interactive" },
                    { 8, false, "Electronic Arts" },
                    { 7, false, "Warner Bros Games" },
                    { 6, false, "Xbox Game Studios" },
                    { 5, false, "Rebuilt Games" },
                    { 4, false, "Psyonix LLC" },
                    { 3, false, "Unknown Publisher" },
                    { 2, false, "Nintendo" },
                    { 1, false, "Microsoft" },
                    { 9, false, "Ubisoft" }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "RatingId", "Description", "IsArchived", "Name" },
                values: new object[,]
                {
                    { 6, "Content suitable only for adults ages 18 and up. May include prolonged scenes of intense violence, graphic sexual content and/or gambling with real currency.", false, "Adult 18+" },
                    { 5, "Not yet assigned a final ESRB rating. Appears only in advertising, marketing and promotional materials related to a physical (e.g., boxed) video game that is expected to carry an ESRB rating, and should be replaced by a game's rating once it has been assigned.", false, "Rating Pending" },
                    { 4, "Content is generally suitable for ages 13 and up. May contain violence, suggestive themes, crude humor, minimal blood, simulated gambling and/or infrequent use of strong language.", false, "Teen 13+" },
                    { 1, "Content is generally suitable for all ages. May contain minimal cartoon, fantasy or mild violence and/or infrequent use of mild language.", false, "Everyone" },
                    { 2, "Content is generally suitable for ages 17 and up. May contain intense violence, blood and gore, sexual content and/or strong language.", false, "Mature 17+" },
                    { 3, "Content is generally suitable for ages 10 and up. May contain more cartoon, fantasy or mild violence, mild language and/or minimal suggestive themes.", false, "Everyone 10+" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EmailAddress", "FirstName", "IsAdmin", "IsArchived", "LastName", "PasswordHash", "ProfileImage", "Username" },
                values: new object[,]
                {
                    { 2, "bobloblaw123@gmail.com", "Bob", true, false, "Loblaw", "AQAAAAEAACcQAAAAEO8KheuRDzmRvQQln7Wfv1Pc0//4p0m/7dBapgh/vL0eVoIpzzwI5fr9uRrKov1Ddw==", null, "BlahBlahBlah" },
                    { 1, "admin@gamerparadise.com", "Sam", true, false, "Krahn", "AQAAAAEAACcQAAAAEN4G4vIBq6fnCHTRVN3j9/uNgolKJvD2+BDIm229Yzm//CQZc0lB7ZzZWAiySFjnlg==", null, "UndeadWolf222" },
                    { 3, "icvabora@students.ntc.edu", "Isaiah", true, false, "Vobora", "AQAAAAEAACcQAAAAEEYvC2iJLEtrOLCMOJ5VbySKqm/0sgJRRnJCuXVsQ32o//EC4wAzmuVOx8ZYdDF5vA==", null, "godofgames44" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "Description", "DeveloperId", "IsArchived", "IsNew", "IsPopular", "IsTrending", "Name", "PublisherId", "RatingId" },
                values: new object[,]
                {
                    { 1, "Rocket League is a high-powered hybrid of arcade-style soccer and vehicular mayhem with easy-to-understand controls and fluid, physics-driven competition.", 4, false, false, true, true, "Rocket League", 4, 1 },
                    { 23, "One of the most beloved real-time strategy games returns to glory with Age of Empires IV, putting you at the center of epic historical battles that shaped the world.", 21, false, true, true, true, "Age of Empires IV", 6, 4 },
                    { 22, "Destiny 2 is an action MMO with a single evolving world that you and your friends can join anytime, anywhere.", 20, false, false, true, true, "Destiny 2", 16, 4 },
                    { 19, "Paradox Development Studio brings you the sequel to one of the most popular strategy games ever made. Crusader Kings III is the heir to a long legacy of historical grand strategy experiences and arrives with a host of new ways to ensure the success of your royal house.", 18, false, true, false, false, "Crusader Kings III", 14, 4 },
                    { 15, "Play with life and discover the possibilities. Unleash your imagination and create a world of Sims that’s wholly unique. Explore and customize every detail from Sims to homes–and much more.", 15, false, false, true, false, "The Sims 4", 8, 4 },
                    { 10, "Join 35+ million players in SMITE, the Battleground of the Gods! Wield Thor’s hammer, turn your foes to stone as Medusa, or flex your divine power as one of 100+ other mythological icons. Become a God.", 11, false, false, false, false, "SMITE", 12, 4 },
                    { 3, "Age of Empires II: Definitive Edition features “The Last Khans” with 3 new campaigns and 4 new Civilizations. Frequent updates include events, additional content, new game modes, and enhanced features with the recent addition of Co-Op mode!", 1, false, false, false, false, "Age Of Empires II", 6, 4 },
                    { 20, "In Kingdom Two Crowns, players must work in the brand-new solo or co-op campaign mode to build their kingdom and secure it from the threat of the Greed. Experience new technology, units, enemies, mounts, and secrets in the next evolution of the award-winning micro strategy franchise!", 19, false, false, false, false, "Kingdom Two Crowns", 15, 3 },
                    { 18, "Civilization VI offers new ways to interact with your world, expand your empire across the map, advance your culture, and compete against history’s greatest leaders to build a civilization that will stand the test of time. Play as one of 20 historical leaders including Roosevelt (America) and Victoria (England).", 17, false, true, false, true, "Sid Meier's Civilization VI", 13, 3 },
                    { 17, "You've inherited your grandfather's old farm plot in Stardew Valley. Armed with hand-me-down tools and a few coins, you set out to begin your new life. Can you learn to live off the land and turn these overgrown fields into a thriving home?", 3, false, false, true, true, "Stardew Valley", 3, 3 },
                    { 25, "The only aim in Rust is to survive. Everything wants you to die - the island’s wildlife and other inhabitants, the environment, other survivors. Do whatever it takes to last another night.", 22, false, false, true, false, "Rust", 17, 2 },
                    { 24, "Winner of over 175 Game of the Year Awards and recipient of over 250 perfect scores, RDR2 is the epic tale of outlaw Arthur Morgan and the infamous Van der Linde gang, on the run across America at the dawn of the modern age. Also includes access to the shared living world of Red Dead Online.", 9, false, false, true, false, "Red Dead Redemption 2", 10, 2 },
                    { 27, "Sea of Thieves offers the essential pirate experience, from sailing and fighting to exploring and looting – everything you need to live the pirate life and become a legend in your own right. With no set roles, you have complete freedom to approach the world, and other players, however you choose.", 24, false, false, true, false, "Sea of Thieves", 6, 4 },
                    { 13, "The original shooter-looter returns, packing bazillions of guns and a mayhem-fueled adventure! Blast through new worlds & enemies and save your home from the most ruthless cult leaders in the galaxy.", 13, false, true, false, false, "Borderlands 3", 13, 2 },
                    { 9, "Death Awaits. Agent 47 returns in HITMAN 3, the dramatic conclusion to the World of Assassination trilogy.", 10, false, false, true, false, "Hitman 3", 11, 2 },
                    { 8, "When a young street hustler, a retired bank robber and a terrifying psychopath land themselves in trouble, they must pull off a series of dangerous heists to survive in a city in which they can trust nobody, least of all each other.", 9, false, false, true, false, "Grand Theft Auto 5", 10, 2 },
                    { 7, "Dive into the gritty world of a modern-day guerrilla revolution to liberate a nation from its oppressive dictators.", 8, false, true, true, true, "Far Cry 6", 9, 2 },
                    { 6, "Battlefield™ 2042 is a first-person shooter that marks the return to the iconic all-out warfare of the franchise. Adapt and overcome in a near-future world transformed by disorder.", 7, false, true, true, true, "Battlefield 2042", 8, 2 },
                    { 4, "Mortal Kombat is back and better than ever in the next evolution of the iconic franchise.", 6, false, false, false, false, "Mortal Kombat 11", 7, 2 },
                    { 26, "NBA 2K22 puts the entire basketball universe in your hands. Anyone, anywhere can hoop in NBA 2K22.", 23, false, false, false, false, "NBA 2K22", 13, 1 },
                    { 21, "Dynamic seasons change everything at the world’s greatest automotive festival. Go it alone or team up with others to explore beautiful and historic Britain in a shared open world.", 14, false, false, true, false, "Forza Horizon 4", 6, 1 },
                    { 16, "Cities: Skylines is a modern take on the classic city simulation. The game introduces new game play elements to realize the thrill and hardships of creating and maintaining a real city whilst expanding on some well-established tropes of the city building experience.", 16, false, false, false, false, "Cities: Skylines", 14, 1 },
                    { 14, "Your Ultimate Horizon Adventure awaits! Explore the vibrant and ever-evolving open world landscapes of Mexico with limitless, fun driving action in hundreds of the world’s greatest cars.", 14, false, false, false, false, "Forza Horizon 5", 6, 1 },
                    { 5, "In this game you are playing the role of the Mario. You are going through the Mushroom Kingdom, surviving the forces of the antagonist Bowser, and saving Princess Toadstool. Mario should reach the flag pole at the end of each level to win the game.", 2, false, false, true, false, "Super Mario Bros", 2, 1 },
                    { 2, "Pummel Party is a 4-8 player online and local-multiplayer party game. Pummel friends or AI using a wide array of absurd items in the board mode and compete to destroy friendships in the unique collection of minigames.", 5, false, false, false, false, "Pummel Party", 5, 1 },
                    { 11, "You are Ajay Ghale. Traveling to Kyrat, you find yourself caught up in a civil war to overthrow the twisted dictator Pagan Min.", 8, false, false, true, false, "Far Cry 4", 9, 2 },
                    { 12, "Join 20+ million players in Rogue Company, the ultimate third-person tactical action shooter! Become an agent of Rogue Company and wield powerful weapons, high-tech gadgets, and game-changing abilities. Accept the mission and jump into a variety of 4v4 and 6v6 game modes.", 12, false, true, false, true, "Rogue Company", 12, 5 }
                });

            migrationBuilder.InsertData(
                table: "GameGenres",
                columns: new[] { "GameGenreId", "GameId", "GenreId" },
                values: new object[,]
                {
                    { 1, 1, 14 },
                    { 31, 13, 1 },
                    { 32, 13, 7 },
                    { 33, 13, 11 },
                    { 54, 24, 12 },
                    { 55, 24, 10 },
                    { 56, 25, 4 },
                    { 57, 25, 6 },
                    { 40, 17, 16 },
                    { 41, 18, 9 },
                    { 45, 20, 9 },
                    { 46, 20, 10 },
                    { 5, 3, 6 },
                    { 6, 3, 9 },
                    { 21, 10, 1 },
                    { 22, 10, 6 },
                    { 37, 15, 8 },
                    { 42, 19, 9 },
                    { 43, 19, 16 },
                    { 44, 19, 8 },
                    { 50, 22, 12 },
                    { 51, 22, 7 },
                    { 52, 23, 6 },
                    { 53, 23, 9 },
                    { 60, 27, 6 },
                    { 61, 27, 10 },
                    { 62, 27, 12 },
                    { 27, 12, 1 },
                    { 30, 13, 16 },
                    { 26, 11, 10 },
                    { 25, 11, 11 },
                    { 24, 11, 1 },
                    { 2, 1, 15 },
                    { 3, 2, 11 },
                    { 4, 2, 6 },
                    { 9, 5, 2 },
                    { 34, 14, 10 },
                    { 35, 14, 14 },
                    { 36, 14, 12 },
                    { 38, 16, 13 },
                    { 39, 16, 8 },
                    { 47, 21, 14 },
                    { 48, 21, 12 },
                    { 49, 21, 6 },
                    { 58, 26, 15 },
                    { 28, 12, 6 },
                    { 59, 26, 8 },
                    { 8, 4, 6 },
                    { 10, 6, 1 },
                    { 11, 6, 7 },
                    { 12, 6, 6 },
                    { 13, 7, 1 },
                    { 14, 7, 7 },
                    { 15, 7, 12 },
                    { 16, 7, 10 },
                    { 17, 8, 12 },
                    { 18, 8, 1 },
                    { 19, 8, 6 },
                    { 20, 9, 1 },
                    { 23, 11, 12 },
                    { 7, 4, 1 },
                    { 29, 12, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_UserId",
                table: "CreditCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenres_GameId",
                table: "GameGenres",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenres_GenreId",
                table: "GameGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GameLibraries_GameId",
                table: "GameLibraries",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameLibraries_UserId",
                table: "GameLibraries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeveloperId",
                table: "Games",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_RatingId",
                table: "Games",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_GameId",
                table: "Reviews",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CardId",
                table: "Subscriptions",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PlanId",
                table: "Subscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGenres");

            migrationBuilder.DropTable(
                name: "GameLibraries");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Developers");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
