using System.ComponentModel.DataAnnotations;

namespace AuthTeste.Models
{
	public class MdlResetPassword
	{
		[Key]
		public int id { get; set; }

		[Required(ErrorMessage = "Campo obrigatorio")]
		[DataType(DataType.Text)]
		[Display(Name = "E-mail")]
		public string email { get; set; }

		[Required(ErrorMessage = "Campo obrigatorio")]
		[DataType(DataType.Password)]
		[Display(Name = "Senha")]
		public string password { get; set; }
		public string token { get; set; }
	}
}
