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
		[StringLength(20, MinimumLength = 4, ErrorMessage = "Tamanho do campo de 4 a 20 caracteres")]
		[Display(Name = "Nome")]
		[DataType(DataType.Text)]
		public string Nome { get; set; } = "";

		[Required(ErrorMessage = "Campo obrigatório")]
		[StringLength(20, MinimumLength = 4, ErrorMessage = "Tamanho do campo de 4 a 20 caracteres")]
		[Display(Name = "Sobrenome")]
		[DataType(DataType.Text)]
		public string Sobrenome { get; set; } = "";

		[Display(Name = "Cpf")]
		[StringLength(11, MinimumLength = 11, ErrorMessage = "O cpf precisa ter 11 digitos")]
		public string Cpf { get; set; } = "";

		[Required(ErrorMessage = "Campo obrigatório")]
		[Display(Name = "Cref")]
		[Range(1, 999999, ErrorMessage = "Campo obrigatório")]
		public int Cref { get; set; }

		[Required(ErrorMessage = "Campo obrigatório")]
		[StringLength(11, MinimumLength = 11, ErrorMessage = "Telefone Inválido")]
		[Display(Name = "Contato")]
		[DataType(DataType.Text)]
		public string Contato { get; set; }

		public virtual ICollection<MdlTurma> Turmas { get; set; }
	}
}
