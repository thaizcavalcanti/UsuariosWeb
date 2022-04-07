using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosWeb.Presentation.Controllers
{
    [Authorize] //só permite acesso de usuários autenticados!
    public class HomeController : Controller
    {
        //método para abrir a página /Home/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}