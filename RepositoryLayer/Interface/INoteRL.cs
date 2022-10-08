using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity AddNote(long UserId, NotePostModel noteModel);
        public IEnumerable<NoteEntity> GetAllNotes(long UserId);
    }
}
