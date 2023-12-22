using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthTeste.Models
{
	[Table("Escola")]
	public class MdlEscola
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Nome { get; set; }

		[MaxLength(100)]
		public string UrlImage { get; set; }

		[MaxLength(50)]
		public string Logradouro { get; set; }

		public int Numero { get; set; }

		[MaxLength(30)]
		public string Bairro { get; set; }

		[MaxLength(30)]
		public string Cidade { get; set; }

		[MaxLength(20)]
		public string Estado { get; set; }

		public virtual ICollection<MdlTurma> Turmas { get; set; }
	}
}
