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
        public Keep GetById (int id)
        {
            Keep foundKeep = _repo.GetById (id);
            if (foundKeep == null)
            {
                throw new Exception ("Invalid Keep Id");
            }
            return foundKeep;
        }
        public IEnumerable<VaultKeepViewModel> Get (string userId)
        {
            return _repo.Get (userId);
        }
        public Keep Create (Keep newKeep)
        {
            return _repo.Create (newKeep);
        }
        public Keep Update (Keep updatedKeep, string userClaim)
        {
            Keep foundKeep = GetById (updatedKeep.Id);
            updatedKeep.Id = foundKeep.Id;
            updatedKeep.UserId = foundKeep.UserId;
            if (userClaim == foundKeep.UserId)
            {
                updatedKeep.Views = foundKeep.Views;
                updatedKeep.Keeps = foundKeep.Keeps;
                updatedKeep.Shares = foundKeep.Shares;
                // only allow user to change these
                updatedKeep.Name = updatedKeep.Name == null ? updatedKeep.Name : foundKeep.Name;
                updatedKeep.Description = updatedKeep.Description == null ? updatedKeep.Description : foundKeep.Description;
                updatedKeep.Img = updatedKeep.Img == null ? updatedKeep.Img : foundKeep.Img;
                updatedKeep.IsPrivate = updatedKeep.IsPrivate != foundKeep.IsPrivate ? updatedKeep.IsPrivate : foundKeep.IsPrivate;
            }
            else
            {
                updatedKeep.Name = foundKeep.Name;
                updatedKeep.Description = foundKeep.Description;
                updatedKeep.Img = foundKeep.Img;
                updatedKeep.IsPrivate = foundKeep.IsPrivate;
                // only allow likes,keeps,shares to be changed by nonAuthors
                updatedKeep.Views = updatedKeep.Views > foundKeep.Views ? foundKeep.Views + 1 : foundKeep.Views;
                updatedKeep.Keeps = updatedKeep.Keeps > foundKeep.Keeps ? foundKeep.Keeps + 1 : foundKeep.Keeps;
                updatedKeep.Shares = updatedKeep.Shares > foundKeep.Shares ? foundKeep.Shares + 1 : foundKeep.Shares;
            }

            bool updated = _repo.Update (updatedKeep);
            if (!updated)
            {
                throw new Exception ("Unable To Update.");
            }
            return updatedKeep;
        }
    }
}