using System.ComponentModel.DataAnnotations;

namespace AuthTeste.Models
{
	public class MdlUser
	{
		[Required(ErrorMessage = "Campo obrigatorio")]
		[DataType(DataType.Text)]
		[Display(Name = "Usuário")]
		public string userName {  get; set; }

		[Required(ErrorMessage = "Campo obrigatorio")]
		[DataType(DataType.Password)]
		[Display(Name = "Senha")]
		public string password { get; set; }
	}
}
