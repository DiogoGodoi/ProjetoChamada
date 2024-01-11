using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthTeste.Models
{
	[Table("Escola")]
	public class MdlEscola
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Campo obrigatório")]
		[StringLength(14, MinimumLength = 14, ErrorMessage = "Cnpj Inválido")]
		[Display(Name = "Cnpj")]
		[DataType(DataType.Text)]
		public string Cnpj { get; set; }

		[Required(ErrorMessage = "Campo Obrigatório")]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "Tamanho do campo de 5 a 50 caracteres")]
		[Display(Name = "Nome")]
		[DataType(DataType.Text)]
		public string Nome { get; set; } = "";

		[StringLength(100, ErrorMessage = "Caminho inválido")]
		[Display(Name = "Imagem")]
		[DataType(DataType.ImageUrl)]
		public string UrlImage { get; set; } = "";

		[Required(ErrorMessage ="Campo obrigatorio")]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "Tamanho do campo de 5 a 50 caracteres")]
		[Display(Name = "Logradouro")]
		[DataType(DataType.Text)]
		public string Logradouro { get; set; } = "";

		[Required(ErrorMessage = "Campo obrigatorio")]
		public int Numero { get; set; }

		[Required(ErrorMessage = "Campo obrigatorio")]
		[StringLength(30, MinimumLength = 5, ErrorMessage = "Tamanho do campo de 5 a 30 caracteres")]
		[Display(Name = "Bairro")]
		[DataType(DataType.Text)]
		public string Bairro { get; set; } = "";

		[Required(ErrorMessage = "Campo obrigatorio")]
		[StringLength(30, MinimumLength = 5, ErrorMessage = "Cidade muito longa")]
		[Display(Name = "Cidade")]
		[DataType(DataType.Text)]
		public string Cidade { get; set; } = "";

		[Required(ErrorMessage = "Campo obrigatorio")]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "Tamanho do campo de 5 a 20 caracteres")]
		[Display(Name = "Estado")]
		public string Estado { get; set; } = "";

		public virtual ICollection<MdlTurma> Turmas { get; set; }
	}
}
