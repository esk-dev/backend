using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.Models;
using NotesBackend.Interfaces;

namespace NotesBackend.Services
{
    public class NoteService : INoteService
    {
        private readonly NotesBackendDbContext _context;

        public NoteService(NotesBackendDbContext context)
        {
            _context = context;
        }

        public async Task<Note> Create(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<Note> GetNoteById(int noteId)
        {
            return await _context.Notes
            .Include(n => n.NoteTags)
            .ThenInclude(nt => nt.Tag)
            .FirstOrDefaultAsync(n => n.Id == noteId);
        }

        public async Task<List<Note>> GetNoteListByUserId(string userId)
        {
            return await _context.Notes.Where(n => n.UserId == userId).ToListAsync();
        }

        public async Task<List<Note>> GetNoteWithTagsListByUserId(string userId)
        {
            return await _context.Notes
            .Where(n => n.UserId == userId)
            .Include(n => n.NoteTags)
            .ThenInclude(nt => nt.Tag)
            .ToListAsync();
        }

        public async Task<Note> UpdateAsync(int id, Note note)
        {
            var existingNote = await _context.Notes.FindAsync(id);

            if (existingNote == null)
            {
                return null;
            }

            existingNote.Title = note.Title;
            existingNote.Content = note.Content;

            await _context.SaveChangesAsync();
            return existingNote;
        }

        public async Task Delete(int noteId)
        {
            var note = await _context.Notes.FindAsync(noteId);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }
    }
}
