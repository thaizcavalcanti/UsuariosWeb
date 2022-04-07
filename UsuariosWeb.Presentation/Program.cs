using Microsoft.AspNetCore.Authentication.Cookies;
using UsuariosWeb.Domain.Interfaces.Repositories;
using UsuariosWeb.Domain.Interfaces.Services;
using UsuariosWeb.Domain.Services;
using UsuariosWeb.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurando para MVC
builder.Services.AddControllersWithViews();

#region Configura��o de inje��o de depend�ncia

//ler a connectionstring do arquivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("UsuariosWeb");

//inje��o da connectionstring para as classes do repositorio
//e tambem j� inicializa-las no projeto AspNet
builder.Services.AddTransient<IPerfilRepository>
    (map => new PerfilRepository(connectionString));

builder.Services.AddTransient<IUsuarioRepository>
    (map => new UsuarioRepository(connectionString));

//inje��o de dependencia para as classes do dominio
//e tambem ja inicializa-las no projeto AspNet
builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();

#endregion

//habilitando o projeto para usar cookies e autentica��o de acesso
builder.Services.Configure<CookiePolicyOptions>
    (options => { options.MinimumSameSitePolicy = SameSiteMode.None; });
builder.Services.AddAuthentication
    (CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

//autentica��o e autoriza��o
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

//Definir a p�gina inicial do projeto (/Account/Login)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}"
);

app.Run();



