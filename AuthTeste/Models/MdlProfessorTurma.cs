﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthTeste.Models
{
	[Table("Professor_Turma")]
	public class MdlProfessorTurma
	{
		[Key]
		public int Id { get; set; }	

		[ForeignKey("Professor")]
		[Required(ErrorMessage = "Campo obrigatório")]
		public int Fk_Professor_Id { get; set; }

		[ForeignKey("Turma")]
		[Required(ErrorMessage = "Campo obrigatório")]
		[Range(0, 1000, ErrorMessage = "Campo obrigatório")]
		public int Fk_Turma_Id { get; set; }
		public virtual MdlTurma Turma { get; set; }
		public virtual MdlProfessor Professor { get; set; }
	}
}