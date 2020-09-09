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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        public VaultsController (VaultsService vs)
        {
            _vs = vs;
        }
        //Get all user vaults
        [HttpGet]
        public ActionResult<IEnumerable<Vault>> Get ()
        {
            try
            {
                Claim user = HttpContext.User.FindFirst (ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception ("Please Make Sure You Are Logged In.");
                }
                return Ok (_vs.Get (user.Value));
            }
            catch (Exception e)
            {
                return BadRequest (e.Message);
            };
        }

        [HttpGet ("{id}")]
        public ActionResult<Vault> GetById (int id)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst (ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception ("Please Make Sure You Are Logged In.");
                }
                return Ok (_vs.GetById (id, user.Value));
            }
            catch (Exception e)
            {
                return BadRequest (e.Message);
            };
        }

        [HttpGet ("{id}/keeps")]
        public ActionResult<IEnumerable<VaultKeepViewModel>> GetKeepsByVaultId (int id)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst (ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception ("You must be logged in.");
                }
                return Ok (_vs.GetKeepsByVaultId (id, user.Value));
            }
            catch (Exception err)
            {
                return BadRequest (err.Message);
            }
        }

        [HttpPost]
        public ActionResult<Vault> Create ([FromBody] Vault newVault)
        {
            try
            {
                Claim userId = HttpContext.User.FindFirst (ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    throw new Exception ("User Id 'userId', is incorrect, make sure you're currently logged in");
                }
                newVault.UserId = userId.Value;
                return Ok (_vs.Create (newVault));
            }
            catch (Exception error)
            {
                return BadRequest (error.Message);
            }
        }

        [Authorize]
        [HttpDelete ("{id}")]
        public ActionResult<Vault> Delete (int id)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst (ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception ("Please Make Sure You Are Logged In.");
                }

                return Ok (_vs.Delete (user.Value, id));
            }
            catch (Exception e)
            {
                return BadRequest (e.Message);
            };
        }

    }
}