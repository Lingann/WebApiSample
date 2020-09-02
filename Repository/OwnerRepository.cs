using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Entities.Models;
namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        private RepositoryContext _repoContext;
        public OwnerRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        /// <summary>
        /// 通过id索引查找单个Owner
        /// </summary>
        /// <param name="ownerId">id索引</param>
        /// <returns></returns>
        public Owner GetOwnerById(int id)
        {
            return _repoContext.Owners.FirstOrDefault(x => x.Id == id);
        }
    }
}
