﻿using EvacuationPlanningMonitoring.Models.DbModels;
using System.Linq.Expressions;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetQueryable();
        TEntity Add<T>(TEntity entity);
        void Update<T>(TEntity entity);
        Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task SaveChangesAsync();
    }
}
