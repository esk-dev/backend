using NotesBackend.Models;

namespace NotesBackend.Interfaces
{
    public interface ITagService
    {
        Task<Tag> GetTagByIdAsync(int tagId);
        Task<Tag> CreateTagAsync(string tagName);
        Task<Tag> GetTagByNameAsync(string tagName);
        Task<List<Tag>> GetAllTagsByQueryAsync(string query);
        Task AddTagsToNoteAsync(int noteId, List<string> tagNames);
        Task RemoveTagsFromNoteAsync(int noteId, List<string> tagNames);
        Task DeleteTagAsync(int tagId);
        Task<List<Tag>> GetTagsAsync();
        Task<Tag> UpdateTagAsync(int tagId, Tag tag);
    }
}