using NotesBackend.Models;

namespace NotesBackend.Interfaces
{
    public interface IUserService
    {
        Task<User> getUserContext(string userName);
    }
}