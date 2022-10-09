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
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;
        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }
        [Authorize]
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
        [Authorize]
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
                    return BadRequest(new { success = false, message = "failed to retrieve data"});
                }
            }
            catch (Exception)
            {
                throw;
            }
    }
        [Authorize]
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
        [Authorize]
        [HttpDelete("DeleteNote")]
        public IActionResult DeletNote(long NoteId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e=>e.Type=="UserId").Value);
                var result=noteBL.DeleteNotes(UserId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message="note successfully deleted", data=result });
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
    }
}
