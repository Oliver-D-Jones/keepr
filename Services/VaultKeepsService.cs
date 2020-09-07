using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        public VaultKeepsService (VaultKeepsRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<VaultKeepViewModel> Get (int vaultId, string userId)
        {
            return _repo.Get (vaultId, userId);
        }
        internal IEnumerable<VaultKeepViewModel> Create (int vaultId,int keepId, string userId)
        {
            return _repo.Create (vaultId, keepId,userId);
        }
    }
}