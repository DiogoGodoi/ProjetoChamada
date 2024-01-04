using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthTeste.Models
{
	[Table("Escola_Professor")]
	public class MdlEscolaProfessor
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("Professor")]
		public int Fk_Professor_Id { get; set; }
		[ForeignKey("Escola")]
		public int Fk_Escola_Id { get; set; }
		public virtual MdlProfessor Professor { get; set; }
		public virtual MdlEscola Escola { get; set; }

	}
}
