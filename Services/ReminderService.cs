
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.Interfaces;
using NotesBackend.Models;

namespace NotesBackend.Services
{
    public class ReminderService : IReminderService
    {
        private readonly NotesBackendDbContext _context;
        private readonly INoteService _noteService;

        public ReminderService(NotesBackendDbContext context, INoteService noteService)
        {
            _context = context;
            _noteService = noteService;
        }

        public async Task<Reminder> CreateReminderAsync(int noteId, Reminder reminder)
        {
            var noteModel = await _noteService.GetNoteById(noteId);
            if (noteModel == null)
            {
                return null;
            }
            await _context.AddAsync(reminder);
            await _context.SaveChangesAsync();
            return reminder;
        }

        public async Task<List<Reminder>> GetAllRemindersByUserIdAsync(string userId)
        {
            return await _context.Reminders
            .Include(v => v.Note)
            .Where(n => n.UserId == userId)
            .ToListAsync();
        }
    }
}