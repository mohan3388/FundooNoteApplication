using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;
using System;
using System.Linq;

namespace FundooNotApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;
        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }

        [HttpPost("AddNote")]
        public IActionResult AddNotes(NotePostModel note)
        {
            try
            {

                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.AddNote(UserId, note);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Added Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note Added Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetNotes")]
        public ActionResult GetAllNotes(long UserId)
        {
            try {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.GetAllNotes(userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "successfully data retrieved", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "failed to retrieve data" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("Updatenotes")]
        public IActionResult UpdateNotes(NotePostModel notePost, long NoteId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.UpdateNotes(notePost, UserId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Data Updated successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = true, message = "Data Updated failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("DeleteNote")]
        public IActionResult DeletNote(long NoteId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.DeleteNotes(UserId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "note successfully deleted", data = result });
                }
                else
                {
                    return BadRequest(new { success = true, message = "note successfully deleted" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("PinNotes")]
        public IActionResult PinNotes(long NoteId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.PinNotes(UserId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Pinned Notes", data = result });
                } else if (result != result)
                {
                    return Ok(new { success = true, message = "Could not Pined Notes" });
                }
                else
                {
                    return BadRequest(new { success = true, message = "Failed to Pined Notes" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("ArchieveNote")]
        public IActionResult ArchieveNote(long NoteId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.ArchieveNote(UserId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Archieved Notes", data = result });
                } else if (result != result)
                {
                    return Ok(new { success = true, message = "Could not Archieved Notes" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to Archieved Notes" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    
        [HttpPut("TrashNotes")]
        public IActionResult TrashNotes(long NoteId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.TrashNotes(UserId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Trash Notes", data = result });
                }
                else if (result != result)
                {
                    return Ok(new { success = true, message = "Could not Trash Notes" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to Trash Notes" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("ChangeColor")]
        public IActionResult ChangeColor(long NoteId, string Color)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.ChangeColor(NoteId, Color);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Color Changed" , data=result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Color not Changed"});
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
