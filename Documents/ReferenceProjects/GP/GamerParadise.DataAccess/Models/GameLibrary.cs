using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerParadise.DataAccess.Models
{
    /// <summary>This is a linking table.</summary>
    public class GameLibrary
    {
        [Key]
        [Required]
        public int GameLibraryId { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "You must be signed in.")]
        public User User { get; set; }
    }
}