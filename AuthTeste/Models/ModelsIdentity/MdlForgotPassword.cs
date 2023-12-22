using System.ComponentModel.DataAnnotations;

namespace AuthTeste.Models.ModelsIdentity
{
    public class MdlForgotPassword
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [DataType(DataType.Text)]
        [Display(Name = "E-mail")]
        public string email { get; set; } = "";

    }
}
