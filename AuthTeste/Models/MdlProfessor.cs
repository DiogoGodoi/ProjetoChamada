using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthTeste.Models
{
	[Table("Professor")]
	public class MdlProfessor
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(20)]
		public string Nome { get; set; }

		[Required]
		[MaxLength(20)]
		public string Sobrenome { get; set; }

		public int Cref { get; set; }

		public virtual ICollection<MdlTurma> Turmas { get; set; }
	}
}
