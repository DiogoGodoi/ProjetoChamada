using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthTeste.Models
{
	[Table("Professor_Turma")]
	[Keyless]
	public class MdlProfessorTurma
	{
		[ForeignKey("Professor")]
		public int Fk_Professor_Id { get; set; }
		[ForeignKey("Turma")]
		public int Fk_Turma_Id { get; set; }
		public virtual MdlTurma? Turma { get; set; }
		public virtual MdlProfessor? Professor { get; set; }
	}
}