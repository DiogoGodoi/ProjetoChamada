using AuthTeste.Models;

namespace AuthTeste.Repository.Interfaces
{
	public interface IEscolasRepository
	{
		IEnumerable<MdlEscola> Escolas { get;}
		void InsertEscola(MdlEscola escola);
		MdlEscola GetEscolaId(int id);
		void UpdateEscola(MdlEscola pmtEscola);
		void RemoveEscola(int id);
	}
}
