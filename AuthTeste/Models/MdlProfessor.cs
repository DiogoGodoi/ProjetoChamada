using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthTeste.Models
{
	[Table("Professor")]
	public class MdlProfessor
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Campo obrigatório")]
		[MaxLength(20, ErrorMessage = "Nome muito longo")]
		[DataType(DataType.Text)]
		public string Nome { get; set; } = "";

		[Required(ErrorMessage = "Campo obrigatório")]
		[MaxLength(20, ErrorMessage = "Sobrenome muito longo")]
		[DataType(DataType.Text)]
		public string Sobrenome { get; set; } = "";

		[Required(ErrorMessage = "Campo obrigatório")]
		public int Cref { get; set; }

		public virtual ICollection<MdlTurma>? Turmas { get; set; }
	}
}
