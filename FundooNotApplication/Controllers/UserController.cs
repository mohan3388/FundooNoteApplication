using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLogger.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FundooNotApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly INLogger logger;

        public UserController(IUserBL userBL,INLogger logger)
        {
            this.userBL = userBL;
            this.logger=logger;
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
                    //
                    //throw new Exception("Error occured");
                    this.logger.LogInfo($"User Regestration Email : {register.EmailId}");
                    return this.BadRequest(new { success = false, message = "user registarion unsuccessfull" });
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"User Regestration Fail: {register.EmailId}");
                throw ex;
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
                    this.logger.LogInfo($"User login Email : {tr.EmailId}");
                    return this.Ok(new { success = true, message = "user login successfull", data = user });
                }
                else
                {
                    logger.LogError($"User login Fail: {tr.EmailId}");
                    return this.BadRequest(new { success = false, message = "user login unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPost("Forgetpassword")]
        public IActionResult ForgetPassword(string emailId)
        {
            try
            {
                var result = userBL.ForgetPassword(emailId);
                if(result!=null)
                {
                    return this.Ok(new { success = true, message = "message sent succefully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "message not found" });
                }
            } catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(PasswordResetModel modelPassword)
        {
            try
            {
                if (modelPassword.Password != modelPassword.ConfirmPassword)
                {
                   
                    return this.BadRequest(new { success = false, message = "New Password and Confirm Password are not equal." });
                }

                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var email = claims.Where(p => p.Type == @"Email").FirstOrDefault()?.Value;
                    this.userBL.ResetPassword(email, modelPassword);
                   
                    return this.Ok(new { success = true, message = "Password Reset Sucessfully...", email = $"{email}" });
                }
                else
                {
                   
                    return this.BadRequest(new { success = false, message = "Password Reset Unsuccessful!!!" });
                }
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
    }
}
