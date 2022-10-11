using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class NoteBL:INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public NoteEntity AddNote(long UserId, NotePostModel notePostModel)
        {
            try
            {
               return noteRL.AddNote(UserId, notePostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<NoteEntity> GetAllNotes(long NoteId)
        {
            try
            {
                return noteRL.GetAllNotes(NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity UpdateNotes(NotePostModel postModel, long UserId, long NoteId)
        {
            try
            {
               return noteRL.UpdateNotes(postModel, UserId, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteNotes(long UserId, long NoteId)
        {
            try
            {
               return noteRL.DeleteNotes(UserId, NoteId);
               
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity PinNotes(long UserId, long NoteId)
        {
            try
            {
                return noteRL.PinNotes(UserId, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
            }
        public NoteEntity ArchieveNote(long UserId, long NoteId)
        {
            try
            {
                return noteRL.ArchieveNote(UserId, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity TrashNotes(long UserId, long NoteId)
        {
            try
            {
                return noteRL.TrashNotes(UserId, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity ChangeColor(long UserId, string color)
        {
            try
            {
                return noteRL.ChangeColor(UserId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity UploadImage(IFormFile image,long UserId,long NoteId)
        {
            try
            {
                return noteRL.Uploadimage(image, UserId, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
