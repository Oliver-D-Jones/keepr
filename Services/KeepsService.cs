using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;
        public KeepsService (KeepsRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Keep> Get ()
        {
            return _repo.Get ();
        }
        internal IEnumerable<VaultKeepViewModel> Get (string userId)
        {
            return _repo.Get (userId);
        }
        public Keep Create (Keep newKeep)
        {
            return _repo.Create (newKeep);
        }
    }
}