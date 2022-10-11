using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public IEnumerable<NoteEntity> GetAllNotes(long NoteId);
        public NoteEntity UpdateNotes(NotePostModel postModel, long UserId, long NoteId);
        public bool DeleteNotes(long UserId, long NoteId);
        public NoteEntity PinNotes(long UserId, long NoteId);
        public NoteEntity ArchieveNote(long UserId, long NoteId);
        public NoteEntity TrashNotes(long UserId, long NoteId);
        public NoteEntity ChangeColor(long NoteId, string color);
        public NoteEntity Uploadimage(IFormFile image, long UserId, long NoteId);
    }
}
