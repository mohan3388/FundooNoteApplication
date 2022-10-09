using BusinessLayer.Interface;
using CommonLayer.Model;
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
        public IEnumerable<NoteEntity> GetAllNotes(long UserId)
        {
            try
            {
                return noteRL.GetAllNotes(UserId);
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
    }
}
