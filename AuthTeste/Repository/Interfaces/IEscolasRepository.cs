using AuthTeste.Models;

namespace AuthTeste.Repository.Interfaces
{
	public interface IEscolasRepository
	{
		IEnumerable<MdlEscola> Escolas { get;}
	}
}
