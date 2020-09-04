using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Contracts.Service;
using Contracts.Services;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IAuthService _authService;
        private IOwnerRepository _owner;
        private IAccountRepository _account;
        private IUserRepository _user;
        public IOwnerRepository Owner
        {
            get { return _owner != null ? _owner : new OwnerRepository(_repoContext); }
        }

        public IAccountRepository Account
        {
            get { return _account != null ? _account : new AccountRepository(_repoContext); }
        }

        public IUserRepository User
        {
            get { return _user != null ? _user : new UserRepository(_repoContext, _authService); }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext, IAuthService authService)
        {
            _repoContext = repositoryContext;
            _authService = authService;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
