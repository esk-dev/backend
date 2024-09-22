using NotesBackend.Models;

namespace NotesBackend.Interfaces
{
    public interface ITagService
    {
        Task<Tag> GetTagByIdAsync(int tagId);
        Task<Tag> CreateTagAsync(string tagName);
        Task<Tag> GetTagByNameAsync(string tagName);
        Task AddTagToNoteAsync(int noteId, int tagId);
        Task AddTagsToNoteAsync(int noteId, List<string> tagNames);
        Task<Tag> RemoveTagFromNoteAsync(Note note, Tag tag);
        Task DeleteTagAsync(int tagId);
        Task<List<Tag>> GetTagsAsync();
        Task<Tag> UpdateTagAsync(int tagId, Tag tag);
    }
}