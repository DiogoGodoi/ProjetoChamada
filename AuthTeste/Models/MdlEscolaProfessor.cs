using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthTeste.Models
{
	[Table("Escola_Professor")]
	[Keyless]
	public class MdlEscolaProfessor
	{
		[ForeignKey("Professor")]
		public int Fk_Professor_Id { get; set; }
		[ForeignKey("Escola")]
		public int Fk_Escola_Id { get; set; }
		public virtual MdlProfessor? Professor { get; set; }
		public virtual MdlEscola? Escola { get; set; }
	}
}
