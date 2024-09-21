using System;
using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public List<NoteTag>? NoteTags { get; set; }
    }
}
