using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public LabelEntity AddLabel(long NoteId, string LabelName)
        {
            try
            {


                LabelEntity entity = new LabelEntity();
                //entity.UserId = UserId;
                entity.NoteId = NoteId;
                entity.LabelName = LabelName;
                fundooContext.Add(entity);
                fundooContext.SaveChanges();
                return entity;



            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<LabelEntity> GetLabel(long NoteId)
        {
            try
            {
                var noteId = fundooContext.LabelTable.Where(x => x.NoteId == NoteId);

                return noteId;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public LabelEntity UpdateLabel(long LabelId, string Labelname)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(x => x.LabelId == LabelId).FirstOrDefault();
                if (result != null)
                {
                    result.LabelName = Labelname;
                    fundooContext.SaveChanges();
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
        public bool DeleteLabel(long LabelId, long NoteId)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(x => x.LabelId == LabelId && x.NoteId == NoteId).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.Remove(result);
                    fundooContext.SaveChanges();
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
