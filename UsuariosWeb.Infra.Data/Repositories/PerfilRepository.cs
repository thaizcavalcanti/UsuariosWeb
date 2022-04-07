using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosWeb.Domain.Entities;
using UsuariosWeb.Domain.Interfaces.Repositories;

namespace UsuariosWeb.Infra.Data.Repositories
{
    /// <summary>
    /// Classe para implementar o repositorio da entidade Perfil
    /// </summary>
    public class PerfilRepository : IPerfilRepository
    {
        //atributo para armazenar a connectionstring do banco de dados
        private string _connectionString;

        //método construtor para receber a string de conexão do banco de dados
        public PerfilRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Inserir(Perfil entity)
        {
            var query = @"
                    INSERT INTO PERFIL(IDPERFIL, NOME)
                    VALUES(@IdPerfil, @Nome)
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Alterar(Perfil entity)
        {
            var query = @"
                    UPDATE PERFIL SET
                        NOME = @Nome
                    WHERE
                        IDPERFIL = @IdPerfil
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Excluir(Perfil entity)
        {
            var query = @"
                    DELETE FROM PERFIL
                    WHERE IDPERFIL = @IdPerfil
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Perfil> Consultar()
        {
            var query = @"
                    SELECT * FROM PERFIL
                    ORDER BY NOME
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Perfil>(query)
                    .ToList();
            }
        }

        public Perfil ObterPorId(Guid id)
        {
            var query = @"
                    SELECT * FROM PERFIL
                    WHERE IDPERFIL = @id
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Perfil>(query, new { id })
                    .FirstOrDefault();
            }
        }

        public Perfil Obter(string nome)
        {
            var query = @"
                    SELECT * FROM PERFIL
                    WHERE NOME = @nome
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Perfil>(query, new { nome })
                    .FirstOrDefault();
            }
        }
    }
}



