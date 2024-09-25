
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesBackend.Dtos;
using NotesBackend.Extensions;
using NotesBackend.Interfaces;
using NotesBackend.Mappers;
using NotesBackend.Models;

namespace NotesBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RemindersController : ControllerBase
    {
        private readonly IReminderService _reminderService;
        private readonly UserManager<User> _userManager;

        public RemindersController(IReminderService reminderService, UserManager<User> userManager)
        {
            _reminderService = reminderService;
            _userManager = userManager;
        }

        private async Task<User> GetLoggedInUser()
        {
            var userName = User.GetLoggedInUserName();
            return await _userManager.FindByNameAsync(userName);
        }

        [HttpGet]
        public async Task<ActionResult<List<Reminder>>> GetReminders()
        {
            var user = await GetLoggedInUser();
            List<Reminder> reminders = await _reminderService.GetAllRemindersByUserIdAsync(user.Id);
            return Ok(reminders);
        }

        [HttpPost]
        public async Task<ActionResult<Reminder>> CreateReminder([FromBody] CreateReminderDto createReminderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await GetLoggedInUser();
            var model = createReminderDto.ToReminder();
            model.UserId = user.Id;
            var reminderModel = await _reminderService.CreateReminderAsync(createReminderDto.NoteId, model);
            if (reminderModel == null)
                return BadRequest("Заметка с таким ID не найдена");
            return Ok(reminderModel);
        }
    }
}
