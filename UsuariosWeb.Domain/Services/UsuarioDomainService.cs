using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosWeb.Domain.Entities;
using UsuariosWeb.Domain.Interfaces.Repositories;
using UsuariosWeb.Domain.Interfaces.Services;

namespace UsuariosWeb.Domain.Services
{
    /// <summary>
    /// Classe para implementação das regras de negócio de usuário
    /// </summary>
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //declarar atributos para utilizarmos os repositorios
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;

        //construtor para fazer a injeção de dependência (inicialização) dos repositorios
        public UsuarioDomainService(IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            //verificar se o email já está cadastrado no banco de dados
            //REGRA: Não permitir o cadastro de usuários com o mesmo email
            if (_usuarioRepository.Obter(usuario.Email) != null)
                //retornar um erro
                throw new Exception($"O email '{usuario.Email}' já está cadastrado na aplicação.");

            //REGRA: Todo usuário cadastrado na aplicação deverá
            //ter o perfil 'default' como padrão
            var perfil = _perfilRepository.Obter("default");
            usuario.IdPerfil = perfil.IdPerfil;

            //cadastrando o usuário
            _usuarioRepository.Inserir(usuario);
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            //consultar o usuário atraves do email
            var item = _usuarioRepository.Obter(usuario.Email);
            //verificar se o email já está cadastrado para outro usuário
            if (item != null && item.IdUsuario != usuario.IdUsuario)
                //retornar um erro
                throw new Exception($"O email '{usuario.Email}' já está cadastrado para outro usuário do sistema.");

            //atualizando os dados do usuário
            _usuarioRepository.Alterar(usuario);
        }

        public void ExcluirUsuario(Usuario usuario)
        {
            //excluindo o usuário
            _usuarioRepository.Excluir(usuario);
        }

        public Usuario AutenticarUsuario(string email, string senha)
        {
            //buscar o usuario no banco de dados atraves do email e da senha
            Usuario usuario = _usuarioRepository.Obter(email, senha);

            //verificar se o usuario foi encontrado
            if (usuario != null)
                return usuario;
            else
                //retornar erro
                throw new Exception("Acesso negado. Usuário inválido.");
        }

        public Usuario ObterUsuario(string email)
        {
            //consultar o usuário no banco de dados atraves do email
            var usuario = _usuarioRepository.Obter(email);

            //verificar se o usuário foi encontrado
            if (usuario != null)
            {
                //buscar os dados do perfil do usuário
                usuario.Perfil = _perfilRepository.ObterPorId(usuario.IdPerfil);

                //retornar o objeto usuario
                return usuario;
            }
            else
                throw new Exception("Usuário não encontrado.");
        }

        public Usuario ObterUsuario(Guid idUsuario)
        {
            //retornar 1 usuário atraves do id
            return _usuarioRepository.ObterPorId(idUsuario);
        }

        public List<Usuario> ConsultarUsuarios()
        {
            //retornar todos os usuários cadastrados no banco
            return _usuarioRepository.Consultar();
        }

        public List<Perfil> ConsultarPerfis()
        {
            //retornar todos os perfis cadastrados no banco
            return _perfilRepository.Consultar();
        }

        public Perfil ObterPerfil(Guid idPerfil)
        {
            //retornar 1 perfil atraves do id
            return _perfilRepository.ObterPorId(idPerfil);
        }
    }
}



