using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using RepositoryLayer.Context;
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
        private readonly ILogger<UserController> logger;
    
        private readonly FundooContext fundooContext;
        public UserController(FundooContext fundooContext,IUserBL userBL, ILogger<UserController> logger)
        {
            this.fundooContext = fundooContext; 
            this.userBL = userBL;
            this.logger = logger;
        }

        [HttpPost("Register")]
        public IActionResult Register(Register register)
        {
            try
            {
                var result = userBL.userRegistration(register);
           
                if (result!=null)
                {
                    logger.LogInformation("User Registration Succesfull");
                    return this.Ok(new { success = true, message = "user registarion successfull", data = result });
                }
                else
                {
                    //
                    //throw new Exception("Error occured");
                    logger.LogInformation("User Registration failed");
                    return this.BadRequest(new { success = false, message = "user registarion unsuccessfull" });
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Login Successfilly");
                    return this.Ok(new { success = true, message = "user login successfull", data = user });
                }
                else
                {

                    logger.LogInformation("Login Failed");
                    return this.BadRequest(new { success = false, message = "user login unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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

                    logger.LogInformation("message sent succefully");
                    return this.Ok(new { success = true, message = "message sent succefully" });
                }
                else
                {

                    logger.LogInformation("message sent failed");
                    return this.BadRequest(new { success = false, message = "message not found" });
                }
            } catch(Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("password not matched");
                    return this.BadRequest(new { success = false, message = "New Password and Confirm Password are not equal." });
                }

                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var email = claims.Where(p => p.Type == @"Email").FirstOrDefault()?.Value;
                    this.userBL.ResetPassword(email, modelPassword);
                    logger.LogInformation("Password Reset Done");
                    return this.Ok(new { success = true, message = "Password Reset Sucessfully...", email = $"{email}" });
                }
                else
                {
                    logger.LogInformation("Password Reset Failed");
                    return this.BadRequest(new { success = false, message = "Password Reset Unsuccessful!!!" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}
