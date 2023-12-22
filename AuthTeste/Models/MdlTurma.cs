using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthTeste.Models
{
	[Table("Turma")]
	public class MdlTurma
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(20)]
		public string Nome { get; set; }

		[MaxLength(15)]
		public string Periodo { get; set; }

		[ForeignKey("Escola")]
		public int Fk_Escola_Id { get; set; }

		public virtual MdlEscola Escola { get; set; }

		public virtual ICollection<MdlProfessor> Professores { get; set; }
	}
}
}
