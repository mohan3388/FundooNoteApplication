using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        public NoteEntity AddNote(long UserId, NotePostModel notePostModel);
        public IEnumerable<NoteEntity> GetAllNotes(long UserId);
        public NoteEntity UpdateNotes(NotePostModel postModel, long UserId, long NoteId);
        public bool DeleteNotes(long UserId, long NoteId);

    }
}
