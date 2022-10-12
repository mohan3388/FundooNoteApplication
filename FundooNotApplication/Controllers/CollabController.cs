using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundooNotApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
      
        private readonly ICollabBL collabBL;
        public CollabController(ICollabBL collabBL)
        {
            
            this.collabBL = collabBL;
        }

        [HttpPost("CollabAdd")]
        public IActionResult AddCollab(long NoteId, string Email)
        {
            try
            {
               // long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId"));
                var result = collabBL.AddCollab(NoteId, Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Added successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "failed to Add" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("RetrieveCollab")]
        public IActionResult RetrieveCollab(long NoteId, long UserId)
        {
            try
            {
              
                var result = collabBL.RetrieveCollab(NoteId,UserId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Data retrieved", data=result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Data retrieved Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete("DeleteCollab")]
        public IActionResult DeleteCollab(long NoteId,string Email)
        {
            try
            {
                var result = collabBL.DeleteCollab(NoteId, Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Deleted successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note Deleted" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
