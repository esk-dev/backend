using System;
using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User? User { get; set; }
        public int ReminderId { get; set; }
        public Reminder? Reminder { get; set; }
        public List<NoteTag>? NoteTags { get; set; }
    }
}
