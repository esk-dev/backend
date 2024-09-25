using NotesBackend.Models;

namespace NotesBackend.Interfaces
{
    public interface INoteService
    {
        Task<Note> Create(Note note);
        Task<Note> GetNoteById(int noteId);
        Task<List<Note>> GetNoteListByUserId(string userId);
        Task<List<Note>> GetNoteWithRelationListByUserId(string userId);
        Task<Note> UpdateAsync(int Id, Note note);
        Task UpdateNoteTagsAsync(int noteId, List<string> tagNames);
        Task Delete(int noteId);

    }
}