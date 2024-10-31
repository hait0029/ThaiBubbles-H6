
namespace ThaiBubbles_H6.Repositories
{
    public class FavoriteRepository : IFavoriteRepositories
    {
        //dependency injection
        private DatabaseContext _context { get; set; }
        public FavoriteRepository(DatabaseContext context)
        {
            _context = context;
        }
        public Task<Favorite> CreateFavorite(Favorite favorite)
        {
            throw new NotImplementedException();
        }

        public Task<Favorite> DeleteFavorite(int favoriteId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Favorite>> GetAllFavorites()
        {
            throw new NotImplementedException();
        }

        public Task<Favorite> GetFavoriteById(int favoriteId)
        {
            throw new NotImplementedException();
        }

        public Task<Favorite> UpdateFavorite(Favorite favorite, int favoriteId)
        {
            throw new NotImplementedException();
        }
    }
}
