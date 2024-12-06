using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Producer { get; set; }
        [Required]
        public double ListPrice { get; set; }
        [Required]
        [Display(Name = "Prix Walmart")]
        [Range(1,1000)]
        public double PriceWalmart { get; set; }
        [Required]
        [Range(1, 1000)]
        [Display(Name = "Prix Amazon")]
        public double PriceAmazon { get; set; }
        [Required]
        [Range(1, 1000)]
        [Display(Name = "Prix ABGames")]
        public double PriceABGames { get; set; }

    }
}
