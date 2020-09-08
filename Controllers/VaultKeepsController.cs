using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
    [Authorize]
    [ApiController]
    [Route ("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;
        public VaultKeepsController (VaultKeepsService vks)
        {
            _vks = vks;
        }

       [HttpGet("{vaultId}")]
        public ActionResult<IEnumerable<VaultKeepViewModel>> Get(int vaultId)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("You must be logged in.");
                }
                return Ok(_vks.Get(vaultId,user.Value));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        
        [HttpPost]
        public ActionResult<RefVaultKeep> Create([FromBody] RefVaultKeep newVaultKeep)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("You must be logged in.");
                }
                newVaultKeep.UserId = user.Value;
                return Ok(_vks.Create(newVaultKeep));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        // [HttpPost]
        // public ActionResult<VaultKeepViewModel> Create([FromBody] VaultKeepViewModel newVaultKeep)
        // {
        //     try
        //     {
        //         Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        //         if (user == null)
        //         {
        //             throw new Exception("You must be logged in.");
        //         }
        //         newVaultKeep.UserId = user.Value;
        //         return Ok(_vks.Create(newVaultKeep));
        //     }
        //     catch (Exception err)
        //     {
        //         return BadRequest(err.Message);
        //     }
        // }

    }
}