using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Games.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage ="Le Nom Ne doit Pas Deppaser 30 Caracteres")]
        [DisplayName("Categories")]
        public string Name { get; set; }
        [DisplayName("Ordre d'affichage")]
        [Range(1,100, ErrorMessage ="Ordre d'affichage doit etre entre 1 et 100")]
        public int DisplayOrder { get; set; }

    }
}
