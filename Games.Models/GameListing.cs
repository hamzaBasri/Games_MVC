using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Games.Models
{
    public class GameListing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GameId { get; set; }

        [ForeignKey("GameId")]
        [ValidateNever]
        public Game Game { get; set; }

        [Required]
        public int PlatformId { get; set; }

        [ForeignKey("PlatformId")]
        [ValidateNever]
        public Platform Platform { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }

        public double PriceEBGames { get; set; }
        public double PriceAmazon { get; set; }
        public double PriceWalmart { get; set; }
    }
}