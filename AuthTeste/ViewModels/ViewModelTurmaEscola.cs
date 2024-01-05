using AuthTeste.Models;

namespace AuthTeste.ViewModels
{
	public class ViewModelTurmaEscola
	{
		public MdlTurma _turma { get; set; }
		public IEnumerable<MdlEscola> _escolas { get; set; }	
	}
}
