namespace BookShoppingCartMvcUi.Repositories
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Genre>> LoadGenres();
        Task<IEnumerable<Book>> DisplayBooks(string sTerm = "", int genreId = 0);
    }
}