using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopamine_API_Data_DOTNET5.Interfaces
{
    public interface IPlayerDataRepository : IRepositoryBase<PlayerData>
    {
        // De 3 metoder herunder er kun med for test formål. For at vise i 
        // CityController.cs hvordan man skal gøre for at få alle relationelle
        // data med, hvis man ikke har enabled lazy loading.
        Task<IEnumerable<PlayerData>> GetAllPlayerData(bool IncludeRelations = false);

        Task<PlayerData> GetPlayerData(int Id, bool IncludeRelations);
        Task<int> GetPointsToAdd(int Id);
    }
}
