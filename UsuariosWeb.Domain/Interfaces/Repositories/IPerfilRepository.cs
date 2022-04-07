using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosWeb.Domain.Entities;

namespace UsuariosWeb.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface de repositório para a entidade Perfil
    /// </summary>
    public interface IPerfilRepository : IBaseRepository<Perfil>
    {
        /// <summary>
        /// Método para consultar 1 perfil no banco de dados atraves do nome
        /// </summary>
        Perfil Obter(string nome);
    }
}



