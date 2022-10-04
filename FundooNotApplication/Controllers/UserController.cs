using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNotApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public IActionResult Register(Register register)
        {
            try
            {
                var result = userBL.userRegistration(register);
           
                if (result!=null)
                {
                    return this.Ok(new { success = true, message = "user registarion successfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "user registarion unsuccessfull" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpPost("Login")]
        public IActionResult UserLogin(Login tr)
        {
            try
            {
                var user = userBL.Login(tr);
                if (user != null)
                {
                    return this.Ok(new { success = true, message = "user login successfull", data = user });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "user login unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
    }
}
