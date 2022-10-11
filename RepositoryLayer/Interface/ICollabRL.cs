using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public CollabEntity AddCollab(long NoteId, string Email);
        public bool DeleteCollab(long CollabId, string Email);
        public IEnumerable<CollabEntity> RetrieveCollab(long NoteId, long UserId);
    }
}
