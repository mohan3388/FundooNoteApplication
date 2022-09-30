using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL:IUserRL
    {
        private readonly FundooContext fundooContext;

        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public UserEntity userRegistration(Register registration)
        {
            try
            {
               UserEntity userEntity = new UserEntity();
                userEntity.FirstName=registration.FirstName;
                userEntity.LastName=registration.LastName;
                userEntity.EmailId=registration.EmailId;
                userEntity.Password = registration.Password;
                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if(result>0)
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
    }
}
