namespace ThaiBubbles_H6.Repositories
{
    public class LoginRepository
    {
        private DatabaseContext _context { get; set; }
        public LoginRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Login> CreateLogin(Login newLogin)
        {
            var existingLogin = await _context.Login.FirstOrDefaultAsync(e => e.Email == newLogin.Email);

            if (newLogin.RoleId.HasValue)
            {
                newLogin.RoleType = await _context.Role.FirstOrDefaultAsync(e => e.RoleID == newLogin.RoleId);
            }

            _context.Login.Add(newLogin);
            await _context.SaveChangesAsync();

            return newLogin;
        }


        public async Task<List<Login>> GetAllLogin()
        {
            //return await _context.Login.ToListAsync();
            return await _context.Login.Include(e => e.RoleType).ToListAsync();
        }

        public async Task<Login> GetLoginById(int loginId)
        {
            //return await _context.Login.FirstOrDefaultAsync(x => x.LoginID == loginId);
            return await _context.Login.Include(e => e.RoleType).FirstOrDefaultAsync(x => x.LoginID == loginId);
        }

        public async Task<Login> UpdateLogin(int loginId, Login updatelogin)
        {
            Login login = await GetLoginById(loginId);

            if (login != null)
            {
                login.Email = updatelogin.Email;
                login.Password = !string.IsNullOrEmpty(updatelogin.Password) ? updatelogin.Password : login.Password;

                if (updatelogin.RoleType != null)
                {
                    login.RoleType = await _context.Role.FirstOrDefaultAsync(e => e.RoleID == updatelogin.RoleType.RoleID);
                }
                else
                {
                    login.RoleType = null; // Clear the UserType if null is provided
                }

                _context.Entry(login).State = EntityState.Modified;


                await _context.SaveChangesAsync();
                return await GetLoginById(loginId);
            }
            return null;
        }
        public async Task<Login> DeleteLogin(int loginId)
        {
            Login login = await GetLoginById(loginId);
            if (login != null)
            {
                _context.Login.Remove(login);
                await _context.SaveChangesAsync();
            }
            return login;
        }


    }
}

