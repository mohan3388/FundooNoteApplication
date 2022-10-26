using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool Archieve { get; set; }
        public bool IsPinned { get; set; }
        public bool Trash { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual UserEntity User{ get; set; }

        [ForeignKey("Note")]
        public long NoteId { get; set; }
        public virtual NoteEntity Note { get; set; }

    }
}
