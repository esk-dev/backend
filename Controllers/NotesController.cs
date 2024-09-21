using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NotesBackend.Models;
using NotesBackend.Services;
using NotesBackend.Dtos.Note;
using NotesBackend.Interfaces;
using NotesBackend.Mappers;
using Microsoft.AspNetCore.Identity;
using NotesBackend.Extensions;

namespace NotesBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly ITagService _tagService;
        private readonly UserService _userService;
        public NotesController(INoteService noteService, ITagService tagService, UserService userService)
        {
            _noteService = noteService;
            _tagService = tagService;
            _userService = userService;
        }


        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetNotes()
        {
            var user_name = User.GetUsername();
            var user = _userService.getUserContext(user_name);
            var notes = await _noteService.GetNoteListByUserId(user.Id);
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
                return NotFound();
            }

            return Ok(note.ToNoteDto());
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote([FromBody] CreateNoteDto createNoteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var noteModel = createNoteDto.ToNoteFromCreateDto();
            var note = await _noteService.Create(noteModel);
            var tagNames = createNoteDto.tagNames;
            if (tagNames.LongCount() != 0)
            {
                await _tagService.AddTagsToNote(note.Id, tagNames);
            }

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note.ToNoteDto());
        }

        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            await _noteService.Update(note);

            return NoContent();
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            await _noteService.Delete(id);

            return NoContent();
        }



        // POST: api/Notes/{noteId}/tags/{tagName}
        [HttpPost("{noteId}/tags/{tagName}")]
        public async Task<IActionResult> AddTagToNote(int noteId, string tagName)
        {
            var note = await _noteService.GetNoteById(noteId);

            if (note == null)
            {
                return NotFound();
            }

            var tag = await _tagService.GetTagByName(tagName);

            if (tag == null)
            {
                tag = await _tagService.CreateTag(tagName);
            }

            await _tagService.AddTagToNote(noteId, tag.Id);

            return NoContent();
        }

        // DELETE: api/Notes/{noteId}/tags/{tagName}
        [HttpDelete("{noteId}/tags/{tagName}")]
        public async Task<IActionResult> RemoveTagFromNote(int noteId, string tagName)
        {
            var note = await _noteService.GetNoteById(noteId);
            if (note == null)
            {
                return NotFound();
            }

            var tag = await _tagService.GetTagByName(tagName);
            if (tag == null)
            {
                return NotFound();
            }

            await _tagService.RemoveTagFromNote(noteId, tag.Id);

            return NoContent();
        }

    }
}
