namespace UsuariosWeb.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para a página de consulta de usuários
    /// </summary>
    public class UsuariosConsultaModel
    {
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
