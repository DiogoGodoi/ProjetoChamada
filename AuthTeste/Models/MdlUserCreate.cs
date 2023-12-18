using System.ComponentModel.DataAnnotations;

namespace AuthTeste.Models
{
	public class MdlUserCreate
	{
		[Key]
		public int id { get; set; }

		[Required(ErrorMessage = "Campo obrigatorio")]
		[DataType(DataType.Text)]
		[Display(Name = "Nome de usuário")]
		public string userName { get; set; }

		[Required(ErrorMessage = "Campo obrigatorio")]
		[DataType(DataType.Text)]
		[Display(Name = "E-mail")]
		public string email { get; set; }

		[Required(ErrorMessage = "Campo obrigatorio")]
		[DataType(DataType.Password)]
		[Display(Name = "Senha")]
		public string password { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        public string confirmPassword { get; set; } = "";

        [Required(ErrorMessage = "Campo obrigatorio")]
		[DataType(DataType.Text)]
		[Display(Name = "Nível de permissão")]
		public string permissao { get; set; }

    }
}
