using NotesBackend.Models;

namespace NotesBackend.Dtos
{
    public class NoteDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; } = string.Empty;
        public required string Content { get; set; } = string.Empty;
        public required string UserId { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
        public required List<Tag> tags { get; set; } = new List<Tag>();
    }
}