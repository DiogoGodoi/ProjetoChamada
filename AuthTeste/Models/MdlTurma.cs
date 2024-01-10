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
		[StringLength(20, MinimumLength = 5, ErrorMessage = "Nome muito longo")]
		[DataType(DataType.Text)]
		[Display(Name = "Nome")]
		public string Nome { get; set; } = "";

		[Required(ErrorMessage = "Campo obrigatório")]
		[ForeignKey("Escola")]
		[Display(Name = "Escola")]
		public int Fk_Escola_Id { get; set; }
		public virtual MdlEscola Escola { get; set; }
		public virtual ICollection<MdlProfessor> Professores { get; set; }
	}
}
