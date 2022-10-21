using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerParadise.DataAccess.Models
{
    public class Game
    {
        [Key]
        [Required]
        public int GameId { get; set; }

        [Required(ErrorMessage = "You must have a name.")]
        [MaxLength(255, ErrorMessage = "Name can not be more than 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must have a description.")]
        [MaxLength(500, ErrorMessage = "Description can not be more than 500 characters.")]
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsTrending { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsPopular { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsNew { get; set; }

        [Required(ErrorMessage = "A publisher is required.")]
        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        [Required(ErrorMessage = "A developer is required.")]
        public int DeveloperId { get; set; }

        public Developer Developer { get; set; }

        [Required(ErrorMessage = "A rating is required.")]
        public int RatingId { get; set; }

        public Rating Rating { get; set; }

        public List<GameLibrary> GameLibraries { get; set; }

        public List<GameGenre> GameGenres { get; set; }

        public List<Review> Reviews { get; set; }

        [Required]
        public bool IsArchived { get; set; }
    }
}