using AuthTeste.Components;
using AuthTeste.Contexto;
using AuthTeste.Models.ModelsIdentity;
using AuthTeste.Repository;
using AuthTeste.Repository.Interfaces;
using AuthTeste.Services.EmailService;
using AuthTeste.Services.EmailService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthTeste
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MeuContexto>();
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {  
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;

            })
                .AddEntityFrameworkStores<MeuContexto>()
                .AddDefaultTokenProviders();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.SlidingExpiration = true;
            });
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddTransient<IEscolasRepository, EscolasRepository>();
			builder.Services.AddTransient<IProfessorRepository, ProfessoresRepository>();
			builder.Services.AddTransient<ITurmaRepository, TurmasRepository>();
			builder.Services.AddTransient<IEscolaProfessorRepository, EscolaProfessorRepository>();
			builder.Services.AddTransient<IProfessorTurmaRepository, ProfessorTurmaRepository>();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
