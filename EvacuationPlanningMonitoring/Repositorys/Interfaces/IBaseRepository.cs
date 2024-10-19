using EvacuationPlanningMonitoring.Models.DbModels;
using System.Linq.Expressions;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetQueryable();
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task SaveChangesAsync();
    }
}
