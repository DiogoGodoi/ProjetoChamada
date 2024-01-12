using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthTeste.Models
{
	[Table("Pais")]
	public class MdlPais
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Campo obrigatório.")]
		[StringLength(20, MinimumLength = 4, ErrorMessage = "Tamanho do campo de 4 a 20 caracteres")]
		[Display(Name = "Nome")]
		[DataType(DataType.Text)]
		public string Nome { get; set; }

		[Required(ErrorMessage = "Campo obrigatório.")]
		[StringLength(20, MinimumLength = 4, ErrorMessage = "Tamanho do campo de 4 a 20 caracteres")]
		[Display(Name = "Sobrenome")]
		[DataType(DataType.Text)]
		public string Sobrenome { get; set; }

		[Required(ErrorMessage = "Campo obrigatório.")]
		[StringLength(1, MinimumLength = 1)]
		[Display(Name = "Sexo")]
		public string Sexo { get; set; }

		[StringLength(11, MinimumLength = 0, ErrorMessage = "O campo CPF deve ter 11 caracteres.")]
		[Display(Name = "Cpf")]
		[DataType(DataType.Text)]
		public string Cpf { get; set; }

		[Required(ErrorMessage = "Campo obrigatório")]
		[StringLength(11, MinimumLength = 11, ErrorMessage = "Telefone Inválido")]
		[Display(Name = "Contato")]
		[DataType(DataType.Text)]
		public string Contato { get; set; }

		[StringLength(40, MinimumLength = 4, ErrorMessage = "Tamanho do campo de 4 a 20 caracteres")]
		[Display(Name = "Logradouro")]
		[DataType(DataType.Text)]
		public string Logradouro { get; set; }

		[Range(0, 9999999)]
		[Display(Name = "Numero")]
		public int Numero { get; set; }

		[StringLength(20, MinimumLength = 4, ErrorMessage = "Tamanho do campo de 4 a 20 caracteres")]
		[Display(Name = "Bairro")]
		[DataType(DataType.Text)]
		public string Bairro { get; set; }

		[StringLength(20, MinimumLength = 4, ErrorMessage = "Tamanho do campo de 4 a 20 caracteres")]
		[Display(Name = "Cidade")]
		[DataType(DataType.Text)]
		public string Cidade { get; set; }

		[StringLength(2, MinimumLength = 2)]
		[Display(Name = "Estado")]
		[DataType(DataType.Text)]
		public string Estado { get; set; }
	}
}
