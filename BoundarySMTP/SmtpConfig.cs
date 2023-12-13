using System;
using System.Dynamic;
using System.Net;
using System.Net.Mail;

namespace BoundarySMTP
{
    public class SmtpConfig
    {

        private string remetente { get; set; } = "diogogoddoi@gmail.com";
        private string assunto { get; set; } = "Redefinicao de senha";
        public string corpo { get; set; } = "";

		public void EnviarEmail(string destinatario)
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

            }catch(SmtpException ex)
            {
                Console.WriteLine(ex.ToString());   
            }

        }

    }
}
