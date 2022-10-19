using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerParadise.DataAccess.Models
{
    public class Publisher
    {
        [Key]
        [Required]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "You must include a name.")]
        [MaxLength(255, ErrorMessage = "Name can not be greater than 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        public List<Game> Games { get; set; }

        [Required]
        public bool IsArchived { get; set; }
    }
}