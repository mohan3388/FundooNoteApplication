using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<NoteController> logger;
        public NoteController(INoteBL noteBL, IMemoryCache memoryCache, FundooContext fundooContext, IDistributedCache distributedCache, ILogger<NoteController> logger)
        {
            this.noteBL = noteBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.logger = logger;
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
                    logger.LogInformation("Note Added Successfully");
                    return Ok(new { success = true, message = "Note Added Successfully", data = result });
                }
                else
                {
                    logger.LogInformation("Note Added Failed");
                    return BadRequest(new { success = false, message = "Note Added Failed" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
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
                    logger.LogInformation("data retrieved");
                    return Ok(new { success = true, message = "successfully data retrieved", data = result });
                }
                else
                {
                    logger.LogInformation("failed to retrieve data");
                    return BadRequest(new { success = false, message = "failed to retrieve data" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
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
                    logger.LogInformation("Note Updated successfully");
                    return Ok(new { success = true, message = "Data Updated successfully", data = result });
                }
                else
                {
                    logger.LogInformation("Note Updated failed");
                    return BadRequest(new { success = true, message = "Data Updated failed" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Note deleted");
                    return Ok(new { success = true, message = "note successfully deleted", data = result });
                }
                else
                {
                    logger.LogInformation("note not deleted");
                    return BadRequest(new { success = true, message = "note not deleted" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
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
                    logger.LogInformation("Pinned Notes");
                    return Ok(new { success = true, message = "Pinned Notes", data = result });
                } else if (result != result)
                {
                    logger.LogInformation("Could not Pined Notes");
                    return Ok(new { success = true, message = "Could not Pined Notes" });
                }
                else
                {
                    logger.LogInformation("Failed to Pined Notes");
                    return BadRequest(new { success = true, message = "Failed to Pined Notes" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
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
                    logger.LogInformation("Archieved Notes");
                    return Ok(new { success = true, message = "Archieved Notes", data = result });
                } else if (result != result)
                {
                    logger.LogInformation("Could not Archieved Notes");
                    return Ok(new { success = true, message = "Could not Archieved Notes" });
                }
                else
                {
                    logger.LogInformation("Failed to Archieve");
                    return BadRequest(new { success = false, message = "Failed to Archieved Notes" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
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
                    logger.LogInformation("Trash Notes");
                    return Ok(new { success = true, message = "Trash Notes", data = result });
                }
                else if (result != result)
                {
                    logger.LogInformation("Could not Trash Notes");
                    return Ok(new { success = true, message = "Could not Trash Notes" });
                }
                else
                {
                    logger.LogInformation("Failed to Trash");
                    return BadRequest(new { success = false, message = "Failed to Trash Notes" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
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
                    logger.LogInformation("Color Changed");
                    return Ok(new { success = true, message = "Color Changed" , data=result});
                }
                else
                {
                    logger.LogInformation("Color not Changed");
                    return BadRequest(new { success = false, message = "Color not Changed"});
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Image Uploaded");
                    return Ok(new { success = true, message = "Image Uploaded", data = result });
                }
                else
                {
                    logger.LogInformation("Image not Changed");
                    return BadRequest(new { success = false, message = "Image not Changed" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
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
