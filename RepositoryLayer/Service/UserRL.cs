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
                userEntity.Password = Encrypt_Password(registration.Password);
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
            var data = fundooContext.UserTable.SingleOrDefault(x => x.EmailId == login.EmailId) ;
            bool passValid = (data.EmailId == login.EmailId && Decrypt_Password(data.Password) == login.Password);
            if (!data.Equals(null)&&passValid)
            {
                return GenerateJWTToken(login.EmailId, data.UserId);
               
            }
            return null;
        }

        //[NotMapped]
     
        private string Decrypt_Password(string encryptpassword)
        {
            string pswstr = string.Empty;
            System.Text.UTF8Encoding encode_psw = new System.Text.UTF8Encoding();
            System.Text.Decoder Decode = encode_psw.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpassword);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            pswstr = new String(decoded_char);
            return pswstr;
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

        private string Encrypt_Password(string password)
        {
            string pswstr = string.Empty;
            byte[] psw_encode = new byte[password.Length];
            psw_encode = System.Text.Encoding.UTF8.GetBytes(password);
            pswstr = Convert.ToBase64String(psw_encode);
            return pswstr;
        }

    }
    }


