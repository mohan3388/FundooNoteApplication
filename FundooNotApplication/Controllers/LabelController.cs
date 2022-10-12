using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNotApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
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
                    return Ok(new { success = true, message = "Data Added Successfully", data = result });

                }
                else
                {
                    return BadRequest(new { success = false, message = "Data Added failed" });
                }
            }
            catch (Exception)
            {
                throw;
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
                    return Ok(new { success = true, message = "retrieved", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "failed" });
                }
            }
            catch (Exception)
            {
                throw;
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
                    return Ok(new { success = true, message = "Updated", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed" });
                }

            }
            catch (Exception)
            {
                throw;
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
                    return Ok(new { success = true, message = "Deleted", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
