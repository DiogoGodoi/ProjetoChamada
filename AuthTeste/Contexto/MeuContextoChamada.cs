using AuthTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthTeste.Contexto
{
	public class MeuContextoChamada: DbContext
	{
		public DbSet<MdlEscola> Escola { get; set; }
		public DbSet<MdlProfessor> Professor { get; set; }
		public DbSet<MdlTurma> Turma { get; set; }
		public DbSet<MdlEscolaProfessor> Escola_Professor { get; set; }
		public DbSet<MdlProfessorTurma> Professor_Turma { get; set; }
		public DbSet<MdlPais> Pais { get; set; }
		public MeuContextoChamada(DbContextOptions<MeuContextoChamada> options) : base(options) { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			IConfiguration configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var defaultConnection = configuration.GetConnectionString("connectionStringChamada");

			optionsBuilder.UseSqlServer(defaultConnection);

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<MdlEscola>(i =>
			{
				i.HasIndex(i => i.UrlImage).IsUnique();
				i.HasIndex(i => i.Cnpj).IsUnique();
				i.HasIndex(i => i.Nome).IsUnique();
			});

			builder.Entity<MdlProfessor>(i =>
			{
				i.HasIndex(i => i.Cref).IsUnique();
				i.HasIndex(i => i.Cpf).IsUnique();	
			});

			builder.Entity<MdlPais>(i =>
			{
				i.HasIndex(I => I.Cpf).IsUnique();
			});

		}
	}
}
