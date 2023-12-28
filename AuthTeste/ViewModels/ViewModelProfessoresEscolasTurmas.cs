using AuthTeste.Models;

namespace AuthTeste.ViewModels
{
	public class ViewModelProfessoresEscolasTurmas
	{
		public MdlProfessor? _professor { get; set; }	
		public MdlEscolaProfessor? _mdlEscolaProfessor { get; set; }
		public MdlProfessorTurma? _mdlProfessorTurma { get; set; }
		public IEnumerable<MdlEscolaProfessor>? _mdlEscolaProfessorList { get; set; }
		public IEnumerable<MdlProfessorTurma>? _mdlProfessorTurmaList { get; set; }

	}
}
