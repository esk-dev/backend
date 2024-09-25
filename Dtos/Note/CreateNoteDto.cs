using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Dtos.Note
{
    public class CreateNoteDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "Заголовок должен быть более 4 символов")]
        [MaxLength(50, ErrorMessage = "Заголовок должен быть не более 50 символов")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Содержание должно быть больше 5 символов")]
        public string Content { get; set; } = string.Empty;
        public List<String> Tags { get; set; } = new List<string>();
    }

}