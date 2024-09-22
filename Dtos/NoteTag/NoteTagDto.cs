
using NotesBackend.Dtos;
using NotesBackend.Models;

namespace NotesBackend.Dtos
{
    public class NoteTagDto
    {
        public required int Id { get; set; }
        public required TagDto tag { get; set; }
    }
}