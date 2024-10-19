using EvacuationPlanningMonitoring.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _entity;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity entity)
        {
            return _context.Set<TEntity>().Add(entity).Entity;
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _entity.AsQueryable();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken);
        }

        public void Update<T>(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
