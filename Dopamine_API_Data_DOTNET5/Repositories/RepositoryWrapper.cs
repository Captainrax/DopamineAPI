using Dopamine_API_Data_DOTNET5.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopamine_API_Data_DOTNET5.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DopamineContext _repoContext;

        private IPlayerDataRepository _playerDataRepositoryWrapper;
        //private ICountryRepository _countryRepositoryWrapper;

        public RepositoryWrapper(DopamineContext repositoryContext)
        {
            this._repoContext = repositoryContext;
        }

        public IPlayerDataRepository PlayerDataRepositoryWrapper
        {
            get
            {
                if (null == _playerDataRepositoryWrapper)
                {
                    _playerDataRepositoryWrapper = new PlayerDataRepository(_repoContext);
                }

                return (_playerDataRepositoryWrapper);
            }
        }

        //public ICountryRepository CountryRepositoryWrapper
        //{
        //    get
        //    {
        //        if (null == _countryRepositoryWrapper)
        //        {
        //            _countryRepositoryWrapper = new CountryRepository(_repoContext);
        //        }

        //        return (_countryRepositoryWrapper);
        //    }
        //}

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
