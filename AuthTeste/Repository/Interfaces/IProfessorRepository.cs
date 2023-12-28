﻿using AuthTeste.ViewModels;

namespace AuthTeste.Repository.Interfaces
{
	public interface IProfessorRepository
	{
		ViewModelProfessoresEscolasTurmas GetProfessores();
		ViewModelProfessoresEscolasTurmas GetProfessorId(int id);
		void CreateProfessor(ViewModelProfessoresEscolasTurmas professor);
		void UpdateProfessor(ViewModelProfessoresEscolasTurmas pmtProfessor);
		void RemoveProfessor(int id);

	}
}
