namespace ThaiBubbles_H6.Interfaces
{
    public interface ILoginRepositories
    {
        public Task<List<Login>> GetAllLogin();
        public Task<Login> GetLoginById(int loginId);
        public Task<Login> CreateLogin(Login login);
        public Task<Login> UpdateLogin(int loginId, Login login);
        public Task<Login> DeleteLogin(int loginId);

    }
}
