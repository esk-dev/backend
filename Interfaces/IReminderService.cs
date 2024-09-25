
using NotesBackend.Models;

namespace NotesBackend.Interfaces
{
    public interface IReminderService
    {
        Task<Reminder> CreateReminderAsync(int noteId, Reminder reminder);
        Task<List<Reminder>> GetAllRemindersByUserIdAsync(string userId);
    }
}