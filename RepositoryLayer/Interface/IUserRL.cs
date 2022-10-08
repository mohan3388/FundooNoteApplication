using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {

        public UserEntity userRegistration(Register registration);
        public string Login(Login login);
        public string ForgetPassword(string emailId);
        public bool ResetPassword(string email, PasswordResetModel modelPassword);
      
        


    }
}
