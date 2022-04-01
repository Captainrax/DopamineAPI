using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dopamine_API_Data_DOTNET5.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dopamine_API_Data_DOTNET5.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DopamineContext RepositoryContext { get; set; }

        public RepositoryBase(DopamineContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public virtual async Task<IEnumerable<T>> FindAll()
        {
            return await this.RepositoryContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<PlayerData> FindPlayerDataWithRelations(int id)
        {
            var query = await this.RepositoryContext.Set<PlayerData>().Where(player => player.Id == id).Include(playerdata => playerdata.Upgrades).FirstOrDefaultAsync();
            //var query = this.RepositoryContext.Set<T>().AsNoTracking().AsQueryable();
            this.RepositoryContext.Entry(query).State = EntityState.Detached;
            return query;

        }

        public virtual async Task<T> FindOne(int id)
        {
            var entity = await this.RepositoryContext.Set<T>().FindAsync(id);
            this.RepositoryContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }


//        public virtual async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
//        {
//            //ParameterExpression s = Expression.Parameter(typeof(T));
////#if (ENABLED_FOR_LAZY_LOADING_USAGE)
////            return await this.RepositoryContext.Set<T>().Where(expression).ToListAsync();
////#else
//            return await this.RepositoryContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
////#endif
//        }

        public virtual async Task Create(T entity)
        {
            await this.RepositoryContext.Set<T>().AddAsync(entity);
            await this.Save();
        }

        public virtual async Task Update(T entity)
        {
            // Skal laves asynkron i linjen herunder. Men UpdateAsync findes ikke !!!
            try
            {
                this.RepositoryContext.Set<T>().Update(entity);
                await this.Save();
            }
            catch (Exception Error)
            {
                string ErrorString = Error.ToString();
            }
        }

        public virtual async Task Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
            await this.Save();
        }

        public virtual async Task Save()
        {
            int NumberOfObjectsSaved = -1;
            NumberOfObjectsSaved = await this.RepositoryContext.SaveChangesAsync();
        }

        //public virtual void EnableLazyLoading()
        //{
        //    this.RepositoryContext.ChangeTracker.LazyLoadingEnabled = true;
        //}

        //public virtual void DisableLazyLoading()
        //{
        //    this.RepositoryContext.ChangeTracker.LazyLoadingEnabled = false;
        //}
    }
}
