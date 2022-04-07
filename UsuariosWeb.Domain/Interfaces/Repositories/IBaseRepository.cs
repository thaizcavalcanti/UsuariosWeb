using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosWeb.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface genérica para definir os métodos do repositorio
    /// </summary>
    /// <typeparam name="TEntity">Tipo de dados genérico</typeparam>
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        void Inserir(TEntity entity);
        void Alterar(TEntity entity);
        void Excluir(TEntity entity);

        List<TEntity> Consultar();
        TEntity ObterPorId(Guid id);
    }
}



