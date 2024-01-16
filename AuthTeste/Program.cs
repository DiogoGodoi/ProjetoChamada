using AuthTeste.Contexto;
using AuthTeste.Repository;
using AuthTeste.Repository.Interfaces;
using AuthTeste.Services.EmailService;
using AuthTeste.Services.EmailService.Interfaces;
using AuthTeste.Services.UploadFileService;
using AuthTeste.Services.UserRoleService;
using Microsoft.AspNetCore.Identity;

namespace AuthTeste
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MeuContextoChamada>();    
            builder.Services.AddDbContext<MeuContextoIdentity>();
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
                .AddEntityFrameworkStores<MeuContextoIdentity>()
                .AddDefaultTokenProviders();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddTransient<IEscolasRepository, EscolasRepository>();
            builder.Services.AddTransient<IProfessorRepository, ProfessoresRepository>();
            builder.Services.AddTransient<ITurmaRepository, TurmasRepository>();
            builder.Services.AddTransient<IProfessorTurmaRepository, ProfessorTurmaRepository>();
            builder.Services.AddTransient<IPaisRepository, PaisRepository>();
            builder.Services.AddTransient<IFileManager, FileManager>();
            builder.Services.AddScoped<IUserRoleService, UserRoleService>();

            var app = builder.Build();

            var userService = app.Services.GetRequiredService<IUserRoleService>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            userService.CreateRole();

            userService.CreateUser();

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
