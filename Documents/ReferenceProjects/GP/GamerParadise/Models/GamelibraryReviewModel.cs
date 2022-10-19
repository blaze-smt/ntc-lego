using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GamerParadise.DataAccess.Models;

namespace GamerParadise.Models
{
    public class GamelibraryReviewModel
    {
        public Game Game { get; set; }

        public List<Review> Reviews { get; set; }

        public User user { get; set; }

        public List<GameLibrary> gameLibraries { get; set; }
    }
}