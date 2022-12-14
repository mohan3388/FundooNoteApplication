using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {

        public LabelEntity AddLabel(long UserId, long NoteId, string LabelName);
        public IEnumerable<LabelEntity> GetLabel(long NoteId);
        public LabelEntity UpdateLabel(long LabelId, string Labelname);
        public bool DeleteLabel(long LabelId, long NoteId);
       
    }
}
