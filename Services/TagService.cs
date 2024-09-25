using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.Models;
using NotesBackend.Interfaces;
using NotesBackend.Mappers;

namespace NotesBackend.Services
{
    public class TagService : ITagService
    {
        private readonly NotesBackendDbContext _context;

        public TagService(NotesBackendDbContext context)
        {
            _context = context;
        }

        public async Task<Tag> CreateTagAsync(string tagName)
        {
            var tag = new Tag { TagName = tagName };
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task<List<Tag>> GetTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetTagByIdAsync(int tagId)
        {
            return await _context.Tags.FindAsync(tagId);
        }

        public async Task<Tag> GetTagByNameAsync(string tagName)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.TagName == tagName);
        }

        public async Task<List<Tag>> GetAllTagsByQueryAsync(string query)
        {
            return await _context.Tags.Where(tag => tag.TagName.Contains(query)).ToListAsync();
        }

        public async Task DeleteTagAsync(int tagId)
        {
            var tag = await _context.Tags.FindAsync(tagId);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Tag> UpdateTagAsync(int tagId, Tag tag)
        {
            var existingTag = await _context.Tags.FindAsync(tagId);

            if (existingTag == null)
            {
                return null;
            }

            existingTag.TagName = tag.TagName;

            await _context.SaveChangesAsync();
            return existingTag;
        }


        public async Task RemoveTagsFromNoteAsync(int noteId, List<string> tagNames)
        {
            List<Tag> tagModels = new List<Tag>();
            foreach (var tagName in tagNames)
            {
                var model = await GetTagByNameAsync(tagName);
                if (model != null)
                {
                    tagModels.Add(model);
                }
            }

            _context.Tags.RemoveRange(tagModels);
            await _context.SaveChangesAsync();
        }

        public async Task AddTagsToNoteAsync(int noteId, List<string> tagNames)
        {
            List<Tag> tagModels = new List<Tag>();
            foreach (var tagName in tagNames)
            {
                var model = await GetTagByNameAsync(tagName);
                if (model != null)
                {
                    tagModels.Add(model);
                }
                else
                {
                    var tagModel = await CreateTagAsync(tagName);
                    tagModels.Add(tagModel);
                };
            }

            var noteTags = tagModels.Select(t => new NoteTag { NoteId = noteId, TagId = t.Id });

            await _context.NoteTags.AddRangeAsync(noteTags);
            await _context.SaveChangesAsync();
        }

    }
}
