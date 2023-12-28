using AuthTeste.Models;

namespace AuthTeste.Repository.Interfaces
{
	public interface ITurmaRepository
	{
		IEnumerable<MdlTurma> Turmas();
		void CreateTurma(MdlTurma turma);
		MdlTurma GetById(int id);
		void UpdateTurma(MdlTurma pmtTurma);
		void DeleteTurma(int id);
	}
}
