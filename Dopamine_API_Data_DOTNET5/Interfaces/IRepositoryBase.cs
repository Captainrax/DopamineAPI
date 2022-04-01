﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dopamine_API_Data_DOTNET5.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindAll();
        Task<T> FindOne(int id);
        // i know this isnt an entity but ive spent too long trying to make it work.
        Task<PlayerData> FindPlayerDataWithRelations(int id);
        //Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Save();

        // LTPE funktionalitet adderet herunder !!!
        //void EnableLazyLoading();

        //void DisableLazyLoading();
    }
}