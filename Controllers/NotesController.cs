using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotesBackend.Models;
using NotesBackend.Dtos.Note;
using NotesBackend.Interfaces;
using NotesBackend.Mappers;
using NotesBackend.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NotesBackend.Dtos;

namespace NotesBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly ITagService _tagService;
        private readonly UserManager<User> _userManager;
        public NotesController(INoteService noteService, ITagService tagService, UserManager<User> userManager)
        {
            _noteService = noteService;
            _tagService = tagService;
            _userManager = userManager;
        }

        private async Task<User> GetLoggedInUser()
        {
            var userName = User.GetLoggedInUserName();
            return await _userManager.FindByNameAsync(userName);
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetNotesWithTags()
        {
            var user = await GetLoggedInUser();
            var notes = await _noteService.GetNoteWithTagsListByUserId(user.Id);
            var noteDtoe = notes.Select(n => n.ToNoteDto()).ToList();
            return Ok(noteDtoe);
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var note = await _noteService.GetNoteById(id);
            if (note == null)
            {
                return NotFound("Note not found");
            }

            return Ok(note.ToNoteDto());
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote([FromBody] CreateNoteDto createNoteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await GetLoggedInUser();
            var noteModel = createNoteDto.ToNoteFromCreateDto();
            noteModel.UserId = user.Id;
            var note = await _noteService.Create(noteModel);
            var tagNames = createNoteDto.tagNames;
            if (tagNames.LongCount() != 0)
            {
                await _tagService.AddTagsToNoteAsync(note.Id, tagNames);
            }

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note.ToNoteDto());
        }

        // PUT: api/Notes/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNote([FromRoute] int id, [FromBody] UpdateNoteDto updateNoteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var noteModel = await _noteService.UpdateAsync(id, updateNoteDto.ToNoteFromUpdateDto(id));

            if (noteModel == null)
            {
                return NotFound("Note not found");
            }

            var note = await _noteService.GetNoteById(id);
            return Ok(note.ToNoteDto());
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            await _noteService.Delete(id);

            return Ok();
        }



        // POST: api/Notes/{noteId}/tags/{tagName}
        [HttpPost("{noteId}/tags/{tagName}")]
        public async Task<IActionResult> AddTagToNote(int noteId, string tagName)
        {
            var note = await _noteService.GetNoteById(noteId);

            if (note == null)
            {
                return NotFound("Note not found");
            }

            var tag = await _tagService.GetTagByNameAsync(tagName);

            if (tag == null)
            {
                tag = await _tagService.CreateTagAsync(tagName);
            }

            await _tagService.AddTagToNoteAsync(noteId, tag.Id);

            return Ok(note.ToNoteDto());
        }

        // DELETE: api/Notes/{noteId}/tags/{tagName}
        [HttpDelete("{noteId}/tags/{tagName}")]
        public async Task<IActionResult> RemoveTagFromNote(int noteId, string tagName)
        {
            var note = await _noteService.GetNoteById(noteId);
            if (note == null)
            {
                return NotFound("Note not found");
            }

            var tag = await _tagService.GetTagByNameAsync(tagName);
            if (tag == null)
            {
                return NotFound("Tag not found");
            }

            var state = await _tagService.RemoveTagFromNoteAsync(note, tag);

            if (state == null)
            {
                return NotFound("Not found realation between Note and TAg");
            }

            return Ok(note.ToNoteDto());
        }

    }
}
