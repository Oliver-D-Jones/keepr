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
    [ApiController]
    [Route ("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;
        public KeepsController (KeepsService ks)
        {
            _ks = ks;
        }
        //Get all publically avaible keeps
        [HttpGet]
        public ActionResult<IEnumerable<Keep>> Get ()
        {
            try
            {
                return Ok (_ks.Get ());
            }
            catch (Exception e)
            {
                return BadRequest (e.Message);
            };
        }
        //NOTE GET USER KEEPS
        [Authorize]
        [HttpGet ("user")]
        public ActionResult<IEnumerable<Keep>> GetByUser ()
        {
            try
            {
                Claim user = HttpContext.User.FindFirst (ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception ("Please Log In To Get Your Keeps.");
                }
                return Ok (_ks.Get (user.Value));
            }
            catch (Exception err)
            {
                return BadRequest (err.Message);
            }
        }

        [HttpGet ("{id}")]
        public ActionResult<Keep> GetById (int id)
        {
            try
            {
                return Ok (_ks.GetById (id));
            }
            catch (Exception e)
            {
                return BadRequest (e.Message);
            };
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Keep> Create ([FromBody] Keep newKeep)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst (ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception ("User Id 'userId', is incorrect, make sure you're currently logged in");
                }
                newKeep.UserId = user.Value;
                return Ok (_ks.Create (newKeep));
            }
            catch (Exception error)
            {
                return BadRequest (error.Message);
            }
        }

        [Authorize]
        [HttpPut ("{id}")]
        public ActionResult<Keep> Update (int id, [FromBody] Keep updatedKeep)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst (ClaimTypes.NameIdentifier);

                if (user == null)
                {
                    throw new Exception ("Please Log In.");
                }
                updatedKeep.Id = id;
                return Ok (_ks.Update (updatedKeep, user.Value));
            }
            catch (System.Exception err)
            {
                return BadRequest (err.Message);
            }
        }

        [Authorize]
        [HttpDelete ("{id}")]
        public ActionResult<Keep> Delete (int id)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst (ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception ("Please Make Sure You Are Logged In.");
                }
                return Ok (_ks.Delete (user.Value, id));
            }
            catch (Exception e)
            {
                return BadRequest (e.Message);
            };
        }

    }
}