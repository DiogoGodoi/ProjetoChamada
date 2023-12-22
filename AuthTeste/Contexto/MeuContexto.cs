using AuthTeste.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthTeste.Contexto
{
    public class MeuContexto: IdentityDbContext<IdentityUser>
    {

        public MeuContexto(DbContextOptions<MeuContexto> options): base(options) { }

        public DbSet<MdlEscola> Escola { get; set; }    
        public DbSet<MdlProfessor> Professor { get; set;}
        public DbSet<MdlEscolaProfessor> Escola_Professor { get; set; }
        public DbSet<MdlTurma> Turma { get; set; }  
        public DbSet<MdlProfessorTurma> Professor_Turma { get; set; }   

    }
}
