using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.Interfaces;
using NotesBackend.Models;

namespace NotesBackend.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<User> _userManager;
        private readonly NotesBackendDbContext _context;

        public UserService(NotesBackendDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<User> getUserContext(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
    }
}
