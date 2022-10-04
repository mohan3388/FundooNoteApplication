using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserEntity userRegistration(Register registration)
        {
            try
            {
                return userRL.userRegistration(registration);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public string Login(Login userLoginModel)
        {
            try
            {
                return userRL.Login(userLoginModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ForgetPassword(string emailId)
        {
            try
            {
                return this.userRL.ForgetPassword(emailId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
