
namespace ThaiBubbles_H6.Repositories
{
    public class UserRepository : IUserRepositories
    {
        //dependency injection
        private DatabaseContext _context { get; set; }
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User newUser)
        {
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }


        public async Task<List<User>> GetAllUsers()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(e => e.UserID == userId);
        }

        public async Task<User> UpdateUser(int userId, User updateUser)
        {
            User user = await GetUserById(userId);
            if (user != null && updateUser != null)
            {
                user.UserID = updateUser.UserID;
                user.FName = updateUser.FName;
                user.LName = updateUser.LName;
                user.PhoneNr = updateUser.PhoneNr;
                user.Address = updateUser.Address;

            }



            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return await GetUserById(userId);
        }
        public async Task<User> DeleteUser(int userId)
        {
            User user = await GetUserById(userId);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }
    }
}
