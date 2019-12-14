using AspNetCore3Base.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace AspNetCore3Base.Data.Contract
{
    public interface IUnitOfWork
    {
        IRepositoryBase<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> Commit();

        void Rollback();
    }
}
