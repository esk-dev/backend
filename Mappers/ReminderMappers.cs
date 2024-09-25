using Newtonsoft.Json.Converters;
using NotesBackend.Dtos;
using NotesBackend.Models;

namespace NotesBackend.Mappers
{
    public static class ReminderMappers
    {
        public static Reminder ToReminder(this CreateReminderDto reminderDto)
        {
            return new Reminder
            {
                NoteId = reminderDto.NoteId,
                ReminderDate = DateTime.ParseExact(reminderDto.ReminderDate, "yyyy-MM-dd HH:mm:ss", null).ToUniversalTime(),
            };
        }
    }
}