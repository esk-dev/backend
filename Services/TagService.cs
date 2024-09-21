using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.Models;
using NotesBackend.Interfaces;

namespace NotesBackend.Services
{
    public class TagService : ITagService
    {
        private readonly NotesBackendDbContext _context;

        public TagService(NotesBackendDbContext context)
        {
            _context = context;
        }

        public async Task<Tag> CreateTag(string tagName)
        {
            var tag = new Tag { TagName = tagName };
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task DeleteTag(int tagId)
        {
            var tag = await _context.Tags.FindAsync(tagId);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Tag> GetTagByName(string tagName)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.TagName == tagName);
        }

        public async Task AddTagToNote(int noteId, int tagId)
        {
            var noteTag = new NoteTag { NoteId = noteId, TagId = tagId };
            _context.NoteTags.Add(noteTag);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTagFromNote(int noteId, int tagId)
        {
            var noteTag = await _context.NoteTags.FindAsync(noteId, tagId);
            if (noteTag != null)
            {
                _context.NoteTags.Remove(noteTag);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddTagsToNote(int noteId, List<string> tagNames)
        {
            List<Tag> tagModels = new List<Tag>();
            foreach (var tagName in tagNames)
            {
                var model = await GetTagByName(tagName);
                if (model != null)
                {
                    tagModels.Add(model);
                }
                else
                {
                    var tagModel = await CreateTag(tagName);
                    tagModels.Add(tagModel);
                };
            }

            var noteTags = tagModels.Select(t => new NoteTag { NoteId = noteId, TagId = t.Id });

            _context.NoteTags.AddRange(noteTags);
        }
    }
}
