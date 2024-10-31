using ThaiBubbles_H6.Model;

namespace ThaiBubbles_H6.Interfaces
{
    public interface IUserRepositories
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserById(int userId);
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(int userId, User user);
        public Task<User> DeleteUser(int userId);
    }
}
