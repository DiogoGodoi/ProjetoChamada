using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundarySMTP
{
	public interface ISmtpConfig
	{
		 string remetente { get; set; } 
		 string assunto { get; set; } 
		 string corpo { get; set; }

		Task EnviarEmail(string desinatario, string assunto);
	}
}
