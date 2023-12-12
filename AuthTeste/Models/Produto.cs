using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthTeste.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [MinLength(5, ErrorMessage = "Nome muito curto")]
        [MaxLength(45, ErrorMessage = "Nome muito longo")]
        public string? nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        public int? quantidade { get; set; }

    }
}
