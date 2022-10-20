using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerParadise.DataAccess.Models
{
    public class Rating
    {
        [Key]
        [Required]
        public int RatingId { get; set; }

        [Required(ErrorMessage = "You must include a name.")]
        [MaxLength(255, ErrorMessage = "Name can not be greater than 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must include a description.")]
        [MaxLength(255, ErrorMessage = "The description cannot be more than 500 characters.")]
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        public List<Game> Games { get; set; }

        [Required]
        public bool IsArchived { get; set; }
    }
}