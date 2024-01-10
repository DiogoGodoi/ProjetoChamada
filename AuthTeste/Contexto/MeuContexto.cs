﻿using AuthTeste.Models;
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
        public DbSet<MdlTurma> Turma { get; set; }  
        public DbSet<MdlEscolaProfessor> Escola_Professor { get; set; }
        public DbSet<MdlProfessorTurma> Professor_Turma { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var defaultConnection = configuration.GetConnectionString("defaultConnection");

			optionsBuilder.UseSqlServer(defaultConnection);

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
            builder.Entity<MdlEscola>(i =>
            {
                i.HasIndex(i => i.UrlImage).IsUnique();
            });
		}
	}
}
