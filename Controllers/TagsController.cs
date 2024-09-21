using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotesBackend.Models;
using NotesBackend.Services;

namespace NotesBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagsController(TagService tagService)
        {
            _tagService = tagService;
        }

        // POST /api/Tags
        [HttpPost]
        public async Task<ActionResult<Tag>> CreateTag(string tagName)
        {
            return await _tagService.CreateTag(tagName);
        }

        // DELETE /api/Tags/1
        [HttpDelete("{tagId}")]
        public async Task<ActionResult> DeleteTag(int tagId)
        {
            await _tagService.DeleteTag(tagId);

            return NoContent();
        }
    }
}
