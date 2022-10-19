using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerParadise.DataAccess.Models
{
    public class Review
    {
        [Required]
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int GameId { get; set; }

        public Game Game { get; set; }

        [MaxLength(1000, ErrorMessage = "The comment cannot be more than 1000 characters")]
        [Column(TypeName = "nvarchar(1000)")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "You must include a rating between 1 and 5 stars.")]
        [Column(TypeName = "tinyint")]
        [Range(1, 5, ErrorMessage = "Rating must only be 1 to 5 stars.")]
        [Display(Name = "Star Rating")]
        public int StarCount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeWritten { get; set; }

        [Required]
        public bool IsArchived { get; set; }
    }
}