using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesBackend.Dtos;
using NotesBackend.Extensions;
using NotesBackend.Interfaces;
using NotesBackend.Mappers;
using NotesBackend.Models;
using NotesBackend.Services;

namespace NotesBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly UserManager<User> _userManager;

        public TagsController(ITagService tagService, UserManager<User> userManager)
        {
            _tagService = tagService;
            _userManager = userManager;
        }

        private async Task<User> GetLoggedInUser()
        {
            var userName = User.GetLoggedInUserName();
            return await _userManager.FindByNameAsync(userName);
        }

        [HttpGet("search")]
        public async Task<ActionResult> GetAllTagsByQuery([FromQuery] string query)
        {
            var tags = await _tagService.GetAllTagsByQueryAsync(query);
            var tagsDto = tags.Select(t => t.ToTagDto()).ToList();
            return Ok(tagsDto);
        }
        // POST /api/Tags
        [HttpPost]
        public async Task<ActionResult<Tag>> CreateTag([FromBody] CreateTagDto createTagDto)
        {
            var tagModel = await _tagService.CreateTagAsync(createTagDto.TagName);
            return CreatedAtAction(nameof(GetTag), new { tagId = tagModel.Id }, tagModel.ToTagDto());
        }


        [HttpGet("{tagId}")]
        public async Task<ActionResult<Tag>> GetTag([FromRoute] int tagId)
        {
            var tag = await _tagService.GetTagByIdAsync(tagId);
            if (tag == null)
                return NotFound("Тег не найден");
            return Ok(tag.ToTagDto());
        }

        [HttpGet]
        public async Task<ActionResult<List<Tag>>> GetTags()
        {
            var user = await GetLoggedInUser();
            List<Tag> tags = await _tagService.GetTagsAsync();
            var tagsDtoe = tags.Select(t => t.ToTagDto()).ToList();
            return Ok(tagsDtoe);
        }


        [HttpPut("{tagId}")]
        public async Task<ActionResult<Tag>> UpdateTag([FromRoute] int tagId, [FromBody] UpdateTagDto updateTagDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tagModel = await _tagService.UpdateTagAsync(tagId, new Tag { Id = tagId, TagName = updateTagDto.TagName });

            if (tagModel == null)
            {
                return NotFound("Тег не найден");
            }

            var tag = await _tagService.GetTagByIdAsync(tagId);
            return Ok(tag.ToTagDto());
        }

        // DELETE /api/Tags/1
        [HttpDelete("{tagId}")]
        public async Task<ActionResult> DeleteTag(int tagId)
        {
            await _tagService.DeleteTagAsync(tagId);
            return Ok();
        }
    }
}
