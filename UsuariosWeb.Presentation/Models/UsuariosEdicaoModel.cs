using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace UsuariosWeb.Presentation.Models
{
    public class UsuariosEdicaoModel
    {
        public Guid IdUsuario { get; set; } //campo oculto!

        [Required(ErrorMessage = "Por favor, informe o nome do usuário.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o email do usuário.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, selecione o perfil do usuário.")]
        public string IdPerfil { get; set; }

        #region Campo de seleção do tipo DropDown/ComboBox

        public List<SelectListItem>? ListagemPerfis { get; set; }

        #endregion
    }
}



