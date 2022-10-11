using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public IEnumerable<NoteEntity> GetAllNotes(long NoteId);
        public NoteEntity UpdateNotes(NotePostModel postModel, long UserId, long NoteId);
        public bool DeleteNotes(long UserId, long NoteId);
        public NoteEntity PinNotes(long UserId, long NoteId);
        public NoteEntity ArchieveNote(long UserId, long NoteId);
        public NoteEntity TrashNotes(long UserId, long NoteId);
        public NoteEntity ChangeColor(long UserId, string color);
        public NoteEntity UploadImage(IFormFile image, long UserId, long NoteId);
    }
}
