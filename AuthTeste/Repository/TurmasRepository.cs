﻿using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthTeste.Repository
{
	public class TurmasRepository: ITurmaRepository
	{
		private readonly MeuContexto _context;

		public TurmasRepository(MeuContexto _context)
		{
			this._context = _context;
		}
		public IEnumerable<MdlTurma> Turmas()
		{
			var turmas = _context.Turma.Include(i => i.Escola).ToList();

			return turmas;
		}
		public MdlTurma GetById(int id)
		{

			var turma = _context.Turma.Include(i => i.Escola).FirstOrDefault(i => i.Id == id);

			if(turma != null)
			{
				return turma;
			}
			else
			{
				return null;
			}
		}
		public void CreateTurma(MdlTurma turma) {

			if(turma != null)
			{
				_context.Add(turma);
				_context.SaveChanges();
			}

		}
		public void UpdateTurma(MdlTurma pmtTurma)
		{
			var turma = _context.Turma.FirstOrDefault(i => i.Id == pmtTurma.Id);

			if(turma != null)
			{
				turma.Nome = pmtTurma.Nome;
				turma.Periodo = pmtTurma.Periodo;	
				turma.Fk_Escola_Id = pmtTurma.Fk_Escola_Id;

				_context.SaveChanges();
			}

		}
		public void DeleteTurma(int id)
		{
			var turma = _context.Turma.FirstOrDefault(i => i.Id == id);
			if(turma != null)
			{
				_context.Turma.Remove(turma);
				_context.SaveChanges();
			}
		}
	}
}
