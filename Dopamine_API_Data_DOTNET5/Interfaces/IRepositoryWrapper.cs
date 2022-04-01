using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopamine_API_Data_DOTNET5.Interfaces
{
    public interface IRepositoryWrapper
    {
        IPlayerDataRepository PlayerDataRepositoryWrapper { get; }

        //ICountryRepository CountryRepositoryWrapper { get; }

        void Save();

    }
}
