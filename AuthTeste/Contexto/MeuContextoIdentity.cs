using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthTeste.Contexto
{
    public class MeuContextoIdentity: IdentityDbContext<IdentityUser>
    {
        public MeuContextoIdentity(DbContextOptions<MeuContextoIdentity> options): base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			IConfiguration configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var defaultConnection = configuration.GetConnectionString("connectionStringIdentity");

			optionsBuilder.UseSqlServer(defaultConnection);

		}

	}
}
