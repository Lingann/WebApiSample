using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Entities.Models;
using Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Utilities;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        private RepositoryContext _repoContext;
        public AccountRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        
        public Account GetAccountById(int id)
        {
            return _repoContext.Accounts.FirstOrDefault(x => x.Id == id);
        }


        public ICollection<Account> GetAccountsByOwnerId(int ownerId)
        {
            return _repoContext.Accounts.Where(x => x.OwnerId == ownerId).AsNoTracking().ToList();
        }
    }
}
