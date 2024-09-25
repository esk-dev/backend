
using System;
using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public string UserId { get; set; }
        public DateTime ReminderDate { get; set; }
        public Note Note { get; set; }
    }
}
