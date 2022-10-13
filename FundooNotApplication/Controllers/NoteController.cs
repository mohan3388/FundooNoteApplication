using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;
        private readonly IMemoryCache memoryCache;
        private readonly FundooContext fundooContext;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBL noteBL, IMemoryCache memoryCache, FundooContext fundooContext, IDistributedCache distributedCache)
        {
            this.noteBL = noteBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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
        public IActionResult GetAllNotes(long NoteId)
        {
            try {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.GetAllNotes(NoteId);
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
        [HttpPut("UploadImage")]
        public IActionResult UploadImage(IFormFile image, long NoteId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.UploadImage(image, UserId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Image Uploaded", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Color not Changed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNoteUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedNoteList;
            var NoteList = new List<NoteEntity>();
            var redisNoteList = await distributedCache.GetAsync(cacheKey);
            if (redisNoteList != null)
            {
                serializedNoteList = Encoding.UTF8.GetString(redisNoteList);
                NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNoteList);
            }
            else
            {
                NoteList = await fundooContext.NoteTable.ToListAsync();
                serializedNoteList = JsonConvert.SerializeObject(NoteList);
                redisNoteList = Encoding.UTF8.GetBytes(serializedNoteList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNoteList, options);
            }
            return Ok(NoteList);
        }
    }
}
