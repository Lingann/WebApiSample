using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IOwnerRepository _owner;
        private IAccountRepository _account;

        public IOwnerRepository Owner
        {
            get { return _owner != null ? _owner : new OwnerRepository(_repoContext); }
        }

        public IAccountRepository Account
        {
            get { return _account != null ? _account : new AccountRepository(_repoContext); }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
