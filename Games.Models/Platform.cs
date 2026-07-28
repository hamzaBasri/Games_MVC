using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Games.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Le Nom Ne doit Pas Deppaser 30 Caracteres")]
        [DisplayName("Plateforme")]
        public string Name { get; set; }

        [ValidateNever]
        public string? LogoUrl { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
