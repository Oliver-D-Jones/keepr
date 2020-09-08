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

        internal RefVaultKeep Create (RefVaultKeep newVaultKeep)
        {
            Keep foundKeep = _keepsService.GetById (newVaultKeep.VaultId);
            foundKeep.Keeps = foundKeep.Keeps + 1;
            string nonAuthor = "nonAuthor";
            _keepsService.Update (foundKeep,nonAuthor);
            return _repo.Create (newVaultKeep);
        }
    }
}