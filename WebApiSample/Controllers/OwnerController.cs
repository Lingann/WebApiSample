using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Contracts;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Authorization;

namespace WebApiSample.Controllers
{
    [Route("api/owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;

        public OwnerController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        [HttpGet]
        [Authorize]
        public Owner GetOwner(int ownerId)
        {
            var owner = _repoWrapper.Owner.GetOwnerById(ownerId);

            var accounts = _repoWrapper.Account.GetAccountsByOwnerId(ownerId);

             owner.Accounts = accounts;

            return owner;
        }
    }
}
