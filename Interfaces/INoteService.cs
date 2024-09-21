using NotesBackend.Models;

namespace NotesBackend.Interfaces
{
    public interface INoteService
    {
        Task<Note> Create(Note note);
        Task<Note> GetNoteById(int noteId);
        Task<List<Note>> GetNoteListByUserId(int userId);
        Task<Note> Update(Note note);
        Task Delete(int noteId);

    }
}