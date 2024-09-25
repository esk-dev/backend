using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.Models;
using NotesBackend.Interfaces;
using System.Security.Cryptography;

namespace NotesBackend.Services
{
    public class NoteService : INoteService
    {
        private readonly NotesBackendDbContext _context;
        private readonly ITagService _tagService;

        public NoteService(NotesBackendDbContext context, ITagService tagService)
        {
            _context = context;
            _tagService = tagService;
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
            .Include(n => n.Reminder)
            .Include(n => n.NoteTags)
            .ThenInclude(nt => nt.Tag)
            .FirstOrDefaultAsync(n => n.Id == noteId);
        }

        public async Task<List<Note>> GetNoteListByUserId(string userId)
        {
            return await _context.Notes.Where(n => n.UserId == userId).ToListAsync();
        }

        public async Task<List<Note>> GetNoteWithRelationListByUserId(string userId)
        {
            return await _context.Notes
            .Where(n => n.UserId == userId)
            .Include(n => n.Reminder)
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

        public async Task UpdateNoteTagsAsync(int noteId, List<string> tags)
        {
            var note = await _context.Notes.FirstAsync(note => note.Id == noteId);
            var currentTags = await _context.NoteTags
            .Where(nt => nt.NoteId == note.Id)
            .Include(nt => nt.Tag)
            .Select(nt => nt.Tag)
            .ToListAsync();

            var tagsToAdd = new List<string>();

            var tagsToRemove = new List<string>();
            foreach (var tagName in tags)
            {
                if (!currentTags.Exists(v => v.TagName == tagName))
                {
                    tagsToAdd.Add(tagName);
                }
            }

            foreach (var currTag in currentTags)
            {
                if (!tags.Exists(updatedTagName => updatedTagName == currTag.TagName))
                {
                    tagsToRemove.Add(currTag.TagName);
                }
            }

            await _tagService.AddTagsToNoteAsync(noteId, tagsToAdd);
            await _tagService.RemoveTagsFromNoteAsync(noteId, tagsToRemove);
        }
    }
}
