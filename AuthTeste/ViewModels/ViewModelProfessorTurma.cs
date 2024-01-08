using AuthTeste.Models;

namespace AuthTeste.ViewModels
{
	public class ViewModelProfessorTurma
	{
		public MdlProfessor _mdlProfessor { get; set; }
		public MdlProfessorTurma _professorTurma { get; set; }
		public IEnumerable<MdlProfessorTurma> _professorTurmaList { get; set; }
		public IEnumerable<MdlTurma> _mdlTurmaList { get; set; }

	}
}
