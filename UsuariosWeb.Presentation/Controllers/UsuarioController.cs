using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UsuariosWeb.Domain.Entities;
using UsuariosWeb.Domain.Interfaces.Services;
using UsuariosWeb.Presentation.Models;
using UsuariosWeb.Presentation.Reports;

namespace UsuariosWeb.Presentation.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        //atributo
        private readonly IUsuarioDomainService _usuarioDomainService;

        //construtor para inicialização (injeção de dependência)
        public UsuarioController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(UsuariosCadastroModel model)
        {
            //verificar se todos os campos
            //passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //criando um objeto Usuario para cadastro no domain service
                    var usuario = new Usuario()
                    {
                        IdUsuario = Guid.NewGuid(),
                        Nome = model.Nome,
                        Email = model.Email,
                        Senha = model.Senha,
                        DataCadastro = DateTime.Now
                    };

                    //cadastrando o usuário
                    _usuarioDomainService.CadastrarUsuario(usuario);

                    ModelState.Clear(); //limpar o formulário
                    TempData["MensagemSucesso"] = $"Usuário {usuario.Nome}, cadastrado com sucesso";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        public IActionResult Consulta()
        {
            //criando uma lista da classe model -> UsuariosConsultaModel
            var lista = new List<UsuariosConsultaModel>();

            try
            {
                //consultando e percorrer os usuários obtidos do banco de dados
                foreach (var item in _usuarioDomainService.ConsultarUsuarios())
                {
                    lista.Add(new UsuariosConsultaModel()
                    {
                        IdUsuario = item.IdUsuario,
                        Nome = item.Nome,
                        Email = item.Email,
                        DataCadastro = item.DataCadastro
                    });
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            //enviando a lista para a página
            return View(lista);
        }

        public IActionResult Relatorio()
        {
            try
            {
                //consultar os usuários
                var usuarios = _usuarioDomainService.ConsultarUsuarios();

                //gerando o relatorio PDF
                var pdf = UsuarioReport.GerarPdf(usuarios);

                //download do relatório..
                return File(pdf, "application/pdf", "relatorio.pdf");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
                return RedirectToAction("Consulta");
            }
        }

        public IActionResult MinhaConta()
        {
            var model = new UsuariosMinhaContaModel();

            try
            {
                //capturar o email do usuário autenticado no sistema
                var email = User.Identity.Name;

                //consultar os dados do usuário atraves do email
                var usuario = _usuarioDomainService.ObterUsuario(email);

                //transferir os dados do usuário para a classe MODEL
                model.Nome = usuario.Nome;
                model.Email = usuario.Email;
                model.Perfil = usuario.Perfil.Nome.ToUpper();
                model.DataCadastro = usuario.DataCadastro;
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            //enviando os dados para a página
            return View(model);
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                var usuario = new Usuario()
                {
                    IdUsuario = id
                };

                _usuarioDomainService.ExcluirUsuario(usuario);
                TempData["MensagemSucesso"] = "Usuário excluído com sucesso!";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            //redirecionamento
            return RedirectToAction("Consulta");
        }

        //método para abrir a página de edição
        public IActionResult Edicao(Guid id)
        {
            var model = new UsuariosEdicaoModel();
            model.ListagemPerfis = new List<SelectListItem>();

            try
            {
                //buscar o usuário atraves do ID..
                var usuario = _usuarioDomainService.ObterUsuario(id);

                //verificar se o usuário foi encontrado
                if (usuario != null)
                {
                    model.IdUsuario = usuario.IdUsuario;
                    model.Nome = usuario.Nome;
                    model.Email = usuario.Email;
                    model.IdPerfil = usuario.IdPerfil.ToString();
                    model.ListagemPerfis = ObterListagemPerfis();
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }

        [HttpPost] //método para receber a requisição POST do formulário
        public IActionResult Edicao(UsuariosEdicaoModel model)
        {
            //verificar se todos os campos passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario()
                    {
                        IdUsuario = model.IdUsuario,
                        Nome = model.Nome,
                        Email = model.Email,
                        IdPerfil = Guid.Parse(model.IdPerfil)
                    };

                    _usuarioDomainService.AtualizarUsuario(usuario);

                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            model.ListagemPerfis = ObterListagemPerfis();
            return View(model);
        }

        //método para gerara listagem de perfis do DropDownList
        private List<SelectListItem> ObterListagemPerfis()
        {
            var lista = new List<SelectListItem>();

            //popular a lista de perfis para o campo de seleção
            foreach (var item in _usuarioDomainService.ConsultarPerfis())
            {
                lista.Add(new SelectListItem { Value = item.IdPerfil.ToString(), Text = item.Nome.ToUpper() });
            }

            return lista;
        }
    }
}



