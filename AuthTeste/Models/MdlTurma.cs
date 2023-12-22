using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthTeste.Models
{
	[Table("Turma")]
	public class MdlTurma
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Campo obrigatório")]
		[MaxLength(20, ErrorMessage = "Nome muito longo")]
		[DataType(DataType.Text)]
		public string Nome { get; set; } = "";

		[MaxLength(15, ErrorMessage = "Período muito longo")]
		[DataType(DataType.Text)]
		public string Periodo { get; set; } = "";

		[ForeignKey("Escola")]
		public int Fk_Escola_Id { get; set; }
		public virtual MdlEscola? Escola { get; set; }
		public virtual ICollection<MdlProfessor>? Professores { get; set; }
	}
}
