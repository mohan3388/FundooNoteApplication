using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity AddLabel(long UserId, long NoteId, string LabelName);
        public IEnumerable<LabelEntity> GetLabel(long NoteId);
        public LabelEntity UpdateLabel(long LabelId, string Labelname);
        public bool Deletelabel(long LabelId, long NoteId);
      
    }
}
