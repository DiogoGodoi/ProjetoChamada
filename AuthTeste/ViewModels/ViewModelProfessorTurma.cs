using AuthTeste.Models;

namespace AuthTeste.ViewModels
{
	public class ViewModelProfessorTurma
	{
		public MdlProfessor? _mdlProfessor { get; set; }
		public IEnumerable<MdlProfessorTurma>? _mdlProfessorTurmaList { get; set; }

	}
}
