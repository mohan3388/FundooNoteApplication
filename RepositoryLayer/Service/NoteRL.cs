using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class NoteRL:INoteRL
    {
        private readonly FundooContext fundonoteContext;
        

        public NoteRL(FundooContext fundonoteContext)
        {
            this.fundonoteContext = fundonoteContext;
           
        }

        public NoteEntity AddNote(long UserId, NotePostModel noteModel)
        {
            try
            {
                NoteEntity note = new NoteEntity();
                note.UserId = UserId;
                note.Title = noteModel.Title;
                note.Description = noteModel.Description;
                note.Reminder=noteModel.Reminder;
                note.Color = noteModel.Color;
                note.Image = noteModel.Image;
                note.Archieve = noteModel.Archieve;
                note.IsPinned = noteModel.IsPinned;
                note.Trash = noteModel.Trash;
                note.Created = DateTime.Now;
                note.Edited = DateTime.Now;
                fundonoteContext.NoteTable.Add(note);
                int result=fundonoteContext.SaveChanges();
                if (result > 0)
                {
                    return note;
                }
                else
                {
                    return null;
                }
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
                var result=fundonoteContext.NoteTable.Where(x=>x.UserId==UserId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity UpdateNotes(NotePostModel postModel,long UserId, long NoteId)
        {
            try
            {
                var result = fundonoteContext.NoteTable.Where(x => x.UserId == UserId && x.NoteId==NoteId).FirstOrDefault();
                if(result != null)
                {
                    result.Title = postModel.Title;
                    result.Description = postModel.Description;
                    result.Color = postModel.Color;
                    result.Reminder = postModel.Reminder;
                    result.Image = postModel.Image;
                    result.Edited = DateTime.Now;
                    fundonoteContext.SaveChanges();
                    return result;
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
        public bool DeleteNotes(long UserId, long NoteId)
        {
            try
            {
                var result = fundonoteContext.NoteTable.Where(x => x.UserId == UserId && x.NoteId == NoteId).FirstOrDefault();
                if (result != null)
                {
                    fundonoteContext.NoteTable.Remove(result);
                    fundonoteContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
