using BusinessLayer.Interface;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly IMemoryCache memoryCache;
        private readonly FundooContext fundooContext;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<LabelController> logger;
        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, FundooContext fundooContext, IDistributedCache distributedCache, ILogger<LabelController> logger)
        {

            this.labelBL = labelBL;
            this.memoryCache=memoryCache;
            this.fundooContext = fundooContext;
            this.distributedCache=distributedCache;
            this.logger=logger;
        }
        [HttpPost("AddLabel")]
        public IActionResult AddLabel(long UserId, long NoteId, string Labelname)
        {
            try
            {
                //long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.AddLabel(UserId, NoteId, Labelname);
                if (result != null)
                {
                    logger.LogInformation("Label Added Successfully");
                    return Ok(new { success = true, message = "Label Added Successfully", data = result });

                }
                else
                {
                    logger.LogInformation("Label Added failed");
                    return BadRequest(new { success = false, message = "Label Added failed" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpGet("GetLabel")]
        public IActionResult GetLabel(long NoteId)
        {
            try
            {
                var result = labelBL.GetLabel(NoteId);
                if (result != null)
                {
                    logger.LogInformation("Label retrieved");
                    return Ok(new { success = true, message = "retrieved", data = result });
                }
                else
                {
                    logger.LogInformation("Label retrieve failed");
                    return BadRequest(new { success = false, message = "failed" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpPut("Updatelabel")]
        public IActionResult UpdateLabel(long LabelId, string Labelname)
        {
            try
            {
                // long Labelid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "LabelId").Value);
                var result = labelBL.UpdateLabel(LabelId, Labelname);
                if (result != null)
                {
                    logger.LogInformation("Updated");
                    return Ok(new { success = true, message = "Updated", data = result });
                }
                else
                {
                    logger.LogInformation("Failed");
                    return BadRequest(new { success = false, message = "Failed" });
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpDelete("DeleteLabel")]
        public IActionResult DeleteLabel(long LabelId, long NoteId)
        {
            try
            {
               // long noteId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "NoteId").Value);
                var result =labelBL.Deletelabel(LabelId, NoteId);
                if(result != null)
                {
                    logger.LogInformation("Deleted");
                    return Ok(new { success = true, message = "Deleted", data = result });
                }
                else
                {
                    logger.LogInformation("failed");
                    return BadRequest(new { success = false, message = "failed" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedlabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedlabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedlabelList);
            }
            else
            {
                LabelList = await fundooContext.LabelTable.ToListAsync();
                serializedlabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedlabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)) // SetAbsoluteExpiration expieration date for cache memory
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));//SetSlidingExpiration it set how long the cache entry can be active
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }
    }
}
