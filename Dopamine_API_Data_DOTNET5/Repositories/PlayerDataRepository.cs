using Dopamine_API_Data_DOTNET5.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopamine_API_Data_DOTNET5.Repositories
{
    public class PlayerDataRepository : RepositoryBase<PlayerData>, IPlayerDataRepository
    {
        public PlayerDataRepository(DopamineContext context) : base(context)
        {
            if (null == context)
            {
                throw new ArgumentNullException(nameof(context));
            }
            this.RepositoryContext.ChangeTracker.LazyLoadingEnabled = false;
        }

        public async Task<IEnumerable<PlayerData>> GetAllPlayerData(bool IncludeRelations = false)
        {
            if (false == IncludeRelations)
            {
                var collection = await base.FindAll();
                return (collection);
            }
            else
            {
                var collection = await base.RepositoryContext.PlayerData.ToListAsync();

                var collection1 = collection.OrderByDescending(c => c.Id);
                return collection1;
            }
        }

        public async Task<PlayerData> GetPlayerData(int TodoItemID, bool IncludeRelations)
        {
            if (false == IncludeRelations)
            {
                var TodoItem_Object = await base.FindOne(TodoItemID);
                return TodoItem_Object;
            }
            else
            {
                var TodoItem_Object = await base.FindPlayerDataWithRelations(TodoItemID);
                return (TodoItem_Object);
            }
        }

        public async Task<int> GetPointsToAdd(int Id)
        {
            var playerData = await base.FindOne(Id);
            if(playerData == null)
            {
                return (0);
            }

            int tempPoints = playerData.pointsToBeAdded;
            playerData.pointsToBeAdded = 0;

            base.RepositoryContext.Update(playerData);
            base.RepositoryContext.SaveChanges();

            return (tempPoints);
        }

    }
}