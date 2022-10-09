using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
