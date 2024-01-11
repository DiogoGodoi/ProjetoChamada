using AuthTeste.Models;

namespace AuthTeste.Repository.Interfaces
{
	public interface IPaisRepository
	{
		IEnumerable<MdlPais> ListPais();
		MdlPais GetPaisId(int id);
		void CreatePais(MdlPais pais);	
		void UpdatePais(MdlPais pais);
		void DeletePais(int id);	
	}
}
