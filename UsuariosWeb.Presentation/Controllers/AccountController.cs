using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UsuariosWeb.Domain.Entities;
using UsuariosWeb.Domain.Interfaces.Services;
using UsuariosWeb.Presentation.Models;

namespace UsuariosWeb.Presentation.Controllers
{
    public class AccountController : Controller
    {
        //atributo
        private readonly IUsuarioDomainService _usuarioDomainService;

        //construtor para inicialização dos atributos
        public AccountController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        //abrir a página /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost] //recebe o submit do formulário da página /Account/Login
        public IActionResult Login(AccountLoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //autenticando o usuário
                    var usuario = _usuarioDomainService.AutenticarUsuario(model.Email, model.Senha);

                    #region Criar a permissão de acesso do usuário

                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, usuario.Email) },
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    #endregion

                    //redirecionar para a página /Home/Index
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        //abrir a página /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost] //recebe o submit do formulário da página /Account/Register
        public IActionResult Register(AccountRegisterModel model)
        {
            //verificando se todos os campos passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //resgatar os dados do usuário
                    var usuario = new Usuario()
                    {
                        IdUsuario = Guid.NewGuid(),
                        Nome = model.Nome,
                        Email = model.Email,
                        Senha = model.Senha,
                        DataCadastro = DateTime.Now
                    };

                    //realizando o cadastro
                    _usuarioDomainService.CadastrarUsuario(usuario);

                    TempData["MensagemSucesso"] = $"Parabéns {usuario.Nome}, sua conta foi criada com sucesso.";
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    //gerando mensagem de erro
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        //método de ação para fazer o logout do usuário:
        public IActionResult Logout()
        {
            #region Remover a permissão de acesso do usuário

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            #endregion

            //redirecionando para a página /Account/Login
            return RedirectToAction("Login", "Account");
        }
    }
}



