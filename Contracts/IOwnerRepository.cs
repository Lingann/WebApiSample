using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IOwnerRepository : IRepositoryBase<Owner>
    {
        Owner GetOwnerById(int ownerId);
    }
}
