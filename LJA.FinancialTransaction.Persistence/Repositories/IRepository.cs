using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJA.FinancialTransaction.Persistence.Repositories
{
    //Defines a generic repository for basic CRUD operations.
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(int id);

        IQueryable<TEntity> GetAll();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        Task DeleteAsync(int id);

        void MarkAsModified(TEntity entity);
    }
}
