using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRL : ICollabRL
    {
        public readonly FundooContext fundooContext;
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public CollabEntity AddCollab(long NoteId, string Email)
        {
            try
            {
                var NoteModel = fundooContext.NoteTable.Where(x => x.NoteId == NoteId).FirstOrDefault();
                var UserModel = fundooContext.UserTable.Where(x => x.EmailId == Email).FirstOrDefault();
                if (NoteModel != null && UserModel != null)
                {
                    CollabEntity collabentity = new CollabEntity();
                    collabentity.UserId = UserModel.UserId;
                    collabentity.NoteId = NoteModel.NoteId;
                    collabentity.CollabEmail = UserModel.EmailId;
                    fundooContext.Add(collabentity);
                    fundooContext.SaveChanges();
                    return collabentity;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<CollabEntity> RetrieveCollab(long NoteId,long UserId)
        {
            try {
                var noteId = fundooContext.CollabTable.Where(x => x.NoteId == NoteId && x.UserId==UserId);
               
                return noteId;
                
            }
            catch (Exception)
            {
                throw;
            }
            }
        public bool DeleteCollab(long CollabId, string Email)
        {
            try {
                var CollaborateId = fundooContext.CollabTable.Where(x => x.CollabId == CollabId && x.CollabEmail == Email).FirstOrDefault();
                if (CollaborateId != null)
                {
                    fundooContext.CollabTable.Remove(CollaborateId);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
    }
    }
}