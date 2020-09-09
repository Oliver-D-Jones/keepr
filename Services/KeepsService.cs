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
            if (foundKeep.IsPrivate)
            {
                throw new Exception ("Invalid Keep Id/Not Public.");
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
                updatedKeep.Name = updatedKeep.Name == null ? foundKeep.Name : updatedKeep.Name;
                updatedKeep.Description = updatedKeep.Description == null ? foundKeep.Description : updatedKeep.Description;
                updatedKeep.Img = updatedKeep.Img == null ? foundKeep.Img : updatedKeep.Img;
                if (foundKeep.IsPrivate && !updatedKeep.IsPrivate)
                {
                    updatedKeep.IsPrivate = false;
                }
                else
                {
                    updatedKeep.IsPrivate = foundKeep.IsPrivate;
                }
            }
            else if (userClaim == "nonAuthor123")
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
            else
            {
                throw new Exception ("Not Owner Of Vault!");
            }

            bool updated = _repo.Update (updatedKeep);
            if (!updated)
            {
                throw new Exception ("Unable To Update.");
            }
            return updatedKeep;
        }
        public string Delete (string userId, int id)
        {
            GetById (id);
            bool delorted = _repo.Delete (userId, id);
            if (!delorted)
            {
                throw new Exception ("Not the author of Keep!");
            }
            return "Keep Deleted!";
        }
    }
}