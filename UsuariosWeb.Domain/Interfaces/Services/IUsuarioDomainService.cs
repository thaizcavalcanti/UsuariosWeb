using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosWeb.Domain.Entities;

namespace UsuariosWeb.Domain.Interfaces.Services
{
    /// <summary>
    /// Interface para regras de negócio da aplicação
    /// </summary>
    public interface IUsuarioDomainService
    {
        /// <summary>
        /// Método para realização do cadastro do usuário
        /// </summary>
        void CadastrarUsuario(Usuario usuario);

        /// <summary>
        /// Método para atualizar um usuário no sistema
        /// </summary>
        void AtualizarUsuario(Usuario usuario);

        /// <summary>
        /// Método para excluir um usuário no sistema
        /// </summary>
        void ExcluirUsuario(Usuario usuario);

        /// <summary>
        /// Método para fazer a verificação de autenticação do usuário
        /// </summary>
        Usuario AutenticarUsuario(string email, string senha);

        /// <summary>
        /// Método para consultar 1 usuário através do email informado
        /// </summary>
        Usuario ObterUsuario(string email);

        /// <summary>
        /// Método para consultar 1 usuário através do id informado
        /// </summary>
        /// <returns></returns>
        Usuario ObterUsuario(Guid idUsuario);

        /// <summary>
        /// Método para consultar todos os usuários cadastrados
        /// </summary>
        List<Usuario> ConsultarUsuarios();

        /// <summary>
        /// Método para consultar todos os perfis cadastrados
        /// </summary>
        List<Perfil> ConsultarPerfis();

        /// <summary>
        /// Método para consultar 1 perfil através do id informado
        /// </summary>
        Perfil ObterPerfil(Guid idPerfil);
    }
}



