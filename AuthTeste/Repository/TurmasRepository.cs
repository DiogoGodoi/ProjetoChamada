using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthTeste.Repository
{
	public class TurmasRepository : ITurmaRepository
	{
		private readonly MeuContextoChamada _context;
		public TurmasRepository(MeuContextoChamada _context)
		{
			this._context = _context;
		}
		public IEnumerable<MdlTurma> GetTurmas()
		{
			try
			{
				var turmas = _context.Turma.Include(i => i.Escola).OrderBy(i => i.Nome).ToList();

				if (turmas != null)
				{
					return turmas;
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
		public MdlTurma GetById(int id)
		{
			try
			{
				var turma = _context.Turma.Include(i => i.Escola).FirstOrDefault(i => i.Id == id);

				if (turma != null)
				{
					return turma;
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}

		}
		public void CreateTurma(MdlTurma turma)
		{
			try
			{
				if (turma != null)
				{
					_context.Turma.Add(turma);
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
		public void UpdateTurma(MdlTurma pmtTurma)
		{
			try
			{
				var turma = _context.Turma.FirstOrDefault(i => i.Id == pmtTurma.Id);

				if (turma != null)
				{
					turma.Nome = pmtTurma.Nome;
					turma.Fk_Escola_Id = pmtTurma.Fk_Escola_Id;

					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
		public void DeleteTurma(int id)
		{
			try
			{
				var turma = _context.Turma.FirstOrDefault(i => i.Id == id);
				 
				if (turma != null)
				{
					_context.Turma.Remove(turma);
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
	}
}
