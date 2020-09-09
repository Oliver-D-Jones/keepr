using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;
        public VaultsService (VaultsRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Vault> Get (string userId)
        {
            return _repo.Get (userId);
        }
        public IEnumerable<VaultKeepViewModel> GetKeepsByVaultId (int id, string userId)
        {
            GetById (id, userId);
            return _repo.GetKeepsByVaultId (id, userId);
        }
        public Vault GetById (int id, string userId)
        {
            Vault foundVault = _repo.GetById (id);
            if (foundVault.UserId != userId)
            {
                throw new Exception ("Not owner of Vault.");
            }
            return foundVault;
        }
        public Vault Create (Vault newVault)
        {
            return _repo.Create (newVault);
        }
        public string Delete (string userId, int id)
        {
            GetById (id, userId);
            bool delorted = _repo.Delete (userId, id);
            if (!delorted)
            {
                throw new Exception ("Not the author of Keep!");
            }
            return "Vault Deleted!";
        }
    }
}