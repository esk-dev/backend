
using System.ComponentModel.DataAnnotations;
using NotesBackend.Models;

namespace NotesBackend.Dtos.Note
{
    public class UpdateNoteDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Заголовок должен быть более 5 символов")]
        [MaxLength(50, ErrorMessage = "Заголовок должен быть не более 50 символов")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Содержание должно быть больше 5 символов")]
        public string Content { get; set; } = string.Empty;
        public List<string> tags { get; set; } = new List<string>();
    }

}