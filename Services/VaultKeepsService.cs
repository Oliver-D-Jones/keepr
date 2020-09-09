using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Http;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly KeepsService _keepsService;

        public VaultKeepsService (VaultKeepsRepository repo, KeepsService keepsService)
        {
            _repo = repo;
            _keepsService = keepsService;
        }

        internal IEnumerable<VaultKeepViewModel> Get (int vaultId, string userId)
        {
            return _repo.Get (vaultId, userId);
        }
        public RefVaultKeep GetById (int id)
        {
            RefVaultKeep foundVK = _repo.GetById (id);
            if (foundVK == null)
            {
                throw new Exception ("Invalid RefVaultKeep Id");
            }
            return foundVK;
        }
        internal RefVaultKeep Create (RefVaultKeep newVaultKeep)
        {
            Keep foundKeep = _keepsService.GetById (newVaultKeep.KeepId);
            foundKeep.Keeps = foundKeep.Keeps + 1;
            string nonAuthor = "nonAuthor123";
            _keepsService.Update (foundKeep, nonAuthor);
            return _repo.Create (newVaultKeep);
        }
        public string Delete (string userId, int id)
        {
            GetById (id);
            bool delorted = _repo.Delete (userId, id);
            if (!delorted)
            {
                throw new Exception ("Invalid Id or not authorized.");
            }
            return "Removed From Vault.";
        }
    }
}