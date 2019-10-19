using AspNetCore3._0Base.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace AspNetCore3._0Base.Data.Contract
{
    public interface IUnitOfWork
    {
        IRepositoryBase<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> Commit();

        void Rollback();
    }
}
