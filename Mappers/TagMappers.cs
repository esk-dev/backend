
using NotesBackend.Dtos;
using NotesBackend.Models;

namespace NotesBackend.Mappers
{
    public static class TagsMappers
    {
        public static TagDto ToTagDto(this Tag tagModel)
        {
            return new TagDto
            {
                Id = tagModel.Id,
                TagName = tagModel.TagName,
            };
        }
    }
}