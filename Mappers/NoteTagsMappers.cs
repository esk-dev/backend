
using NotesBackend.Models;
using NotesBackend.Dtos;
namespace NotesBackend.Mappers
{
    public static class NoteTagsMappers
    {
        public static NoteTagDto ToNoteTagDto(this NoteTag noteTag)
        {
            return new NoteTagDto
            {
                Id = noteTag.Id,
                tag = noteTag.Tag.ToTagDto(),
            };
        }
    }
}