using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopamine_API_Data_DOTNET5
{
    public class Upgrades
    {
        public virtual int Id { get; set; }
        public virtual int BuildingOne { get; set; } = 0;
        public virtual int BuildingTwo { get; set; } = 0;
        public virtual PlayerData PlayerData { get; set; }
    }
}
