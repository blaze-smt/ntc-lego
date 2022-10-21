using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerParadise.DataAccess.Models
{
    public class Plan
    {
        [Key]
        [Required]
        public int PlanId { get; set; }

        [Required(ErrorMessage = "You must include a name.")]
        [MaxLength(255, ErrorMessage = "The name can not be more than 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must include a description.")]
        [MaxLength(500, ErrorMessage = "The description can not be more than 500 characters.")]
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must include a price.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "You must include a max number of downloads.")]
        [Range(1, 50, ErrorMessage = "You must specify between 1 and 50 downloads.")]
        [Column(TypeName = "tinyint")]
        [Display(Name = "Maximum Downloads")]
        public int MaxDownloads { get; set; }

        [Required]
        public bool IsArchived { get; set; }

        [Required]
        public bool HasPersonalLibrary { get; set; }

        [Required]
        public bool RecievesNewsletter { get; set; }

        [Required]
        public bool HasAccessToAllGames { get; set; }

        [Required]
        public bool CanBetaTest { get; set; }

        [Required]
        public bool CanPlayOnTwoDevices { get; set; }

        public List<Subscription> Subscriptions { get; set; }

    }
}