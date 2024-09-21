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
            return await _context.Notes.FindAsync(noteId);
        }

        public async Task<List<Note>> GetNoteListByUserId(int userId)
        {
            return await _context.Notes.Where(n => n.UserId == userId.ToString()).ToListAsync();
        }

        public async Task<Note> Update(Note note)
        {
            _context.Entry(note).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return note;
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
