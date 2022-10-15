using BusinessLayer.Interface;
using BusinessLayer.Service;
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
    public class CollabController : ControllerBase
    {
      
        private readonly ICollabBL collabBL;
        private readonly IMemoryCache memoryCache;
        private readonly FundooContext fundooContext;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<CollabController> logger;
        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, FundooContext fundooContext, IDistributedCache distributedCache, ILogger<CollabController> logger)
        {
            
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.fundooContext=fundooContext;
            this.distributedCache = distributedCache;
            this.logger = logger;
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
                    logger.LogInformation("Added successfully");
                    return Ok(new { success = true, message = "Added successfully", data = result });
                }
                else
                {
                    logger.LogInformation("failed to Add");
                    return BadRequest(new { success = false, message = "failed to Add" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
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
                    logger.LogInformation("Data retrieved");
                    return Ok(new { success = true, message = "Data retrieved", data=result});
                }
                else
                {
                    logger.LogInformation("Data retrieved Failed");
                    return BadRequest(new { success = false, message = "Data retrieved Failed" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
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
                    logger.LogInformation("Deleted successfully");
                    return Ok(new { success = true, message = "Deleted successfully", data = result });
                }
                else
                {
                    logger.LogInformation("Not Deleted");
                    return BadRequest(new { success = false, message = "Not Deleted" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            var cacheKey = "CollabList";
            string serializedCollabList;
            var CollabList = new List<CollabEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                CollabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
            }
            else
            {
                CollabList = await fundooContext.CollabTable.ToListAsync();
                serializedCollabList = JsonConvert.SerializeObject(CollabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(CollabList);
        }
    }
}
