using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Models.Interfaces;
using ProvaPub.Repository;
using System.Reflection;

namespace ProvaPub.Services
{
    public abstract class BaseService<T> where T : class, IEntity
    {
        protected readonly TestDbContext _ctx;
        protected DbSet<T> _dbSet;
        protected readonly int pageSize = 10;

        public BaseService(TestDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<T>();
        }

        public async virtual Task<Items<T>> ListItems(int page) 
        { 
            if(page < 1)
                page = 1;
            int totalSize = _dbSet.Count();
            int skip = (page - 1) * pageSize;
            bool hasNext = (skip + pageSize) < totalSize;

            List<T> listItems = await _dbSet.OrderBy(p => p.Id).Skip(skip).Take(pageSize).ToListAsync();

            return new Items<T>() { HasNext = hasNext, TotalCount = 10, Item = listItems };
        }
    }
}
