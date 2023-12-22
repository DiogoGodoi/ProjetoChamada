﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AuthTeste.Models
{
	public class MdlEscolaProfessor
	{
		[ForeignKey("Professor")]
		public int Fk_Professor_Id { get; set; }

		[ForeignKey("Escola")]
		public int Fk_Escola_Id { get; set; }

		public virtual MdlProfessor Professor { get; set; }

		public virtual MdlEscola Escola { get; set; }
	}
}
