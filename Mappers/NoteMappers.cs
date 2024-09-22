using NotesBackend.Models;
using NotesBackend.Dtos;
using NotesBackend.Dtos.Note;

namespace NotesBackend.Mappers
{
    public static class NoteMappers
    {
        public static NoteDto ToNoteDto(this Note noteModel)
        {
            return new NoteDto
            {
                Id = noteModel.Id,
                Title = noteModel.Title,
                Content = noteModel.Content,
                UserId = noteModel.UserId,
                CreatedAt = noteModel.CreatedAt,
                UpdatedAt = noteModel.UpdatedAt,
                tags = noteModel.NoteTags.Select(noteTags => noteTags?.ToNoteTagDto()).ToList(),
            };
        }
        public static Note ToNoteFromCreateDto(this CreateNoteDto noteDto)
        {
            return new Note
            {
                Title = noteDto.Title,
                Content = noteDto.Content,
            };
        }
        public static Note ToNoteFromUpdateDto(this UpdateNoteDto updateNoteDto, int id)
        {
            return new Note
            {
                Title = updateNoteDto.Title,
                Content = updateNoteDto.Content,
                Id = id
            };
        }
    }
}