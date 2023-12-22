namespace AuthTeste.Services.EmailService.Interfaces
{
    public interface IEmailService
    {
        string remetente { get; set; }
        string assunto { get; set; }
        string corpo { get; set; }

        Task EnviarEmail(string desinatario, string assunto);
    }
}
