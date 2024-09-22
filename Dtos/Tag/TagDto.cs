
using NotesBackend.Models;

namespace NotesBackend.Dtos
{
    public class TagDto
    {
        public required int Id { get; set; }
        public required string TagName { get; set; }
    }
}