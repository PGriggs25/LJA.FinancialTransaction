using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJA.FinancialTransaction.Persistence.Repositories
{
    // Generic repository implementation; EF Core’s DbContext effectively acts as a unit of work
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _set;

        public Repository(DbContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _set.AsQueryable();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _set.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _set.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _set.Remove(entity);
        }

        public void MarkAsModified(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
