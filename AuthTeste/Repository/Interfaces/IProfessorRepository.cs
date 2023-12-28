using AuthTeste.Models;
using AuthTeste.ViewModels;

namespace AuthTeste.Repository.Interfaces
{
	public interface IProfessorRepository
	{
		ViewModelProfessoresEscolasTurmas GetProfessores();
		void CreateProfessor(ViewModelProfessoresEscolasTurmas professor);
	}
}
