using NotesBackend.Models;

namespace NotesBackend.Interfaces
{
    public interface ITagService
    {
        Task<Tag> CreateTag(string tagName);
        Task<Tag> GetTagByName(string tagName);
        Task AddTagToNote(int noteId, int tagId);
        Task AddTagsToNote(int noteId, List<string> tagNames);
        Task RemoveTagFromNote(int noteId, int tagId);
    }
}