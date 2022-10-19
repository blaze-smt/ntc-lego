using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerParadise.DataAccess.Models
{
    public class Genre
    {
        [Key]
        [Required]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "You must include a name.")]
        [MaxLength(255, ErrorMessage = "Name must not be longer than 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must include a description.")]
        [MaxLength(500, ErrorMessage = "Description must not be longer than 500 characters.")]
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        public List<GameGenre> GameGenres { get; set; }

        [Required]
        public bool IsArchived { get; set; }
    }
}