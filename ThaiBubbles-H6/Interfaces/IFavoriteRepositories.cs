using ThaiBubbles_H6.Model;

namespace ThaiBubbles_H6.Interfaces
{
    public interface IFavoriteRepositories
    {
        public Task<List<Favorite>> GetAllFavorites();
        public Task<Favorite> GetFavoriteById(int favoriteId);
        public Task<Favorite> CreateFavorite(Favorite favorite);
        public Task<Favorite> UpdateFavorite(Favorite favorite, int favoriteId);
        public Task<Favorite> DeleteFavorite(int favoriteId);
    }
}
