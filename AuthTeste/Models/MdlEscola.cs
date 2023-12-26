using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthTeste.Models
{
	[Table("Escola")]
	public class MdlEscola
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Campo Obrigatório")]
		[MaxLength(50, ErrorMessage = "Nome muito longo")]
		[DataType(DataType.Text)]
		public string Nome { get; set; } = "";
		[MaxLength(100, ErrorMessage = "Caminho inválido ou muito longa")]
		[DataType(DataType.ImageUrl)]
		public string UrlImage { get; set; } = "";
		[MaxLength(50, ErrorMessage = "Endereço muito longo")]
		[DataType(DataType.Text)]
		public string Logradouro { get; set; } = "";
		[DataType(DataType.Text)]
		public int Numero { get; set; }
		[MaxLength(30, ErrorMessage = "Bairro muito longo")]
		[DataType(DataType.Text)]
		public string Bairro { get; set; } = "";
		[MaxLength(30, ErrorMessage = "Cidade muito longa")]
		[DataType(DataType.Text)]
		public string Cidade { get; set; } = "";
		[MaxLength(20)]
		[DataType(DataType.Text)]
		public string Estado { get; set; } = "";
		public virtual ICollection<MdlTurma>? Turmas { get; set; }
	}
}
