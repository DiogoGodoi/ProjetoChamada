using AuthTeste.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthTeste.Contexto
{
    public class MeuContexto: IdentityDbContext<IdentityUser>
    {

        public MeuContexto(DbContextOptions<MeuContexto> options): base(options) { }

    }
}
