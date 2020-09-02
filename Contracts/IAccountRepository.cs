using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        ICollection<Account> GetAccountsByOwnerId(int ownerId);

        Account GetAccountById(int Id);
    }
}
