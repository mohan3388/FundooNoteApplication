using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity AddCollab(long NoteId, string Email);
        public bool DeleteCollab(long NoteId, string Email);
        public IEnumerable<CollabEntity> RetrieveCollab(long NoteId, long UserId);
    }
}
