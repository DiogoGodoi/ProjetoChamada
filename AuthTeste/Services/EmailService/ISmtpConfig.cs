namespace AuthTeste.Services.EmailService
{
    public interface ISmtpConfig
    {
        string remetente { get; set; }
        string assunto { get; set; }
        string corpo { get; set; }

        Task EnviarEmail(string desinatario, string assunto);
    }
}
