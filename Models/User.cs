using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NotesBackend.Models
{
    public class User : IdentityUser
    {
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
