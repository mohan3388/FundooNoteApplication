using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL:ILabelBL
    {
        private readonly ILabelRL labelRL;

        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public LabelEntity AddLabel(long NoteId, string LabelName)
        {
            try
            {
                return labelRL.AddLabel(NoteId, LabelName);
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
                return labelRL.GetLabel(NoteId);
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
                return labelRL.UpdateLabel(LabelId, Labelname);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Deletelabel(long LabelId, long NoteId)
        {
            try{
                return labelRL.DeleteLabel(LabelId, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
