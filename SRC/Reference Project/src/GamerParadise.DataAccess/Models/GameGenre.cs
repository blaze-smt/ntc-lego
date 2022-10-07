using System.ComponentModel.DataAnnotations;

namespace GamerParadise.DataAccess.Models
{
    public class GameGenre
    {
        [Key]
        [Required]
        public int GameGenreId { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int GenreId { get; set; }

        [Required(ErrorMessage = "You must specify a genre.")]
        public Genre Genre { get; set; }
    }
}