

namespace BookShoppingCartMvcUi.Repositories
{
    public interface IGenreRepository
    {
        Task AddGenre(Genre genre);
        Task DeleteGenre(Genre genre);
        Task<Genre?> GetGenreById(int id);
        Task<IEnumerable<Genre>> GetGenres();
        Task UpdateGenre(Genre genre);
    }
}