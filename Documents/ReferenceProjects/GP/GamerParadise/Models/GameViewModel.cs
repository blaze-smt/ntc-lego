using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GamerParadise.DataAccess.Models;

namespace GamerParadise.Models
{
    public class GameViewModel
    {
        public Game Game { get; set; }

        public List<Genre> AllGenres { get; set; }

        [Display(Name = "Genres")]
        [Required(ErrorMessage = "You must select at least one genre.")]
        public List<int> SelectedGameGenreIds { get; set; }

        public List<Developer> Developers { get; set; }

        public List<Publisher> Publishers { get; set; }

        public List<Rating> Ratings { get; set; }
    }
}