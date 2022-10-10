using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration config;

        public UserRL(FundooContext fundooContext,IConfiguration config)
        {
            this.config = config;
            this.fundooContext = fundooContext;
        }
        public UserEntity userRegistration(Register registration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = registration.FirstName;
                userEntity.LastName = registration.LastName;
                userEntity.EmailId = registration.EmailId;
                userEntity.Password = registration.Password;
                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Login(Login login)
        {
            var data = fundooContext.UserTable.Where(x => x.EmailId == login.EmailId && x.Password == login.Password).FirstOrDefault();
            if (data == null)
            {
                return null;
            }
            return GenerateJWTToken(login.EmailId, data.UserId);
        }

        private string GenerateJWTToken(string email, long UserId)
        {
            try
            {
                // generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(this.config[("Jwt:key")]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email),
                      
                    new Claim("UserId", UserId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),

                    SigningCredentials =
                    new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 

        public string ForgetPassword(string emailId)
        {
            try
            {
                var emailCheck = fundooContext.UserTable.FirstOrDefault(e => e.EmailId == emailId);
                if (emailCheck != null)
                {
                    var token = GenerateJWTToken(emailCheck.EmailId, emailCheck.UserId);
                    MSMQModel mSMQModel = new MSMQModel();
                    mSMQModel.sendData2Queue(token);
                    return token.ToString();
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            }
        public bool ResetPassword(string email, PasswordResetModel modelPassword)
        {

            try
            {
                var user = this.fundooContext.UserTable.Where(x => x.EmailId == email).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }

                if (modelPassword.Password == modelPassword.ConfirmPassword)
                {
                    user.Password = modelPassword.Password;
                    this.fundooContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    

    }
    }


