using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;
using WebAPI.Data;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext context;
        public GenericRepository(DataContext context) => this.context = context;

        public void Add(TEntity entity) => context.Add(entity);
        public void AddRange(IEnumerable<TEntity> entities) => context.AddRange(entities);
        public IQueryable<TEntity> GetAll() => context.Set<TEntity>();
        public TEntity GetById(string id) => context.Set<TEntity>().Find(id);
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression) => context.Set<TEntity>().Where(expression);
        public void Remove(TEntity entity) => context.Set<TEntity>().Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities) => context.Set<TEntity>().RemoveRange(entities);

    }
}
