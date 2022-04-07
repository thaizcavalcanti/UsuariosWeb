using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosWeb.Domain.Entities;

namespace UsuariosWeb.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface de repositório para a entidade Usuário
    /// </summary>
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        /// <summary>
        /// Método para consultar 1 usuário no banco de dados atraves do email
        /// </summary>
        Usuario Obter(string email);

        /// <summary>
        /// Método para consultar 1 usuário no banco de dados atraves do email e da senha
        /// </summary>
        Usuario Obter(string email, string senha);
    }
}



