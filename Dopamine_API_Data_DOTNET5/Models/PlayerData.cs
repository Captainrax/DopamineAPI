// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Dopamine_API_Data_DOTNET5;
using System.Collections.Generic;

namespace Dopamine_API_Data_DOTNET5
{
    public class PlayerData
    {
        public int Id { get; set; }
        public long points { get; set; }
        public int pointsToBeAdded { get; set; }
        public virtual Upgrades Upgrades { get; set; }
    }
}
