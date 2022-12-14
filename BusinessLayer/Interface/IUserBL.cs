using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity userRegistration(Register registration);
        public string Login(Login userLoginModel);
        public string ForgetPassword(string emailId);
        public bool ResetPassword(string email, PasswordResetModel modelPassword);
    }
}
