using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL:ICollabBL
    {
        public readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public CollabEntity AddCollab(long NoteId, string Email)
        {
            try
            {
                return collabRL.AddCollab(NoteId, Email);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<CollabEntity> RetrieveCollab(long NoteId, long UserId)
        {
            try {
                return collabRL.RetrieveCollab(NoteId, UserId);
            }
            catch (Exception)
            {
                throw;
            }
            }
        public bool DeleteCollab(long NoteId, string Email)
        {
            try
            {
                return collabRL.DeleteCollab(NoteId,Email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
