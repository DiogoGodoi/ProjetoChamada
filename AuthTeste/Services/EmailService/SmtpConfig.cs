using System.Net;
using System.Net.Mail;

namespace AuthTeste.Services.EmailService
{
    public class SmtpConfig : ISmtpConfig
    {
        public string remetente { get; set; } = "diogogoddoi@gmail.com";
        public string assunto { get; set; } = "";
        public string corpo { get; set; } = "";

        public Task EnviarEmail(string destinatario, string assunto)
        {
            var server = "smtp.gmail.com";
            var porta = 587;
            var usuario = "diogogoddoi@gmail.com";
            var senha = "apmm vjju cdvu jhmh";

            SmtpClient smtpClient = new SmtpClient(server)
            {
                Port = porta,
                Credentials = new NetworkCredential(usuario, senha),
                EnableSsl = true

            };

            MailMessage mensagem = new MailMessage(remetente, destinatario, assunto, corpo);

            try
            {
                smtpClient.Send(mensagem);

            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Task.CompletedTask;

        }

    }
}
