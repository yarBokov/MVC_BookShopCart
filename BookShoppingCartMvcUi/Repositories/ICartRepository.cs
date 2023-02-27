namespace BookShoppingCartMvcUi.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int bookId, int qty);
        Task<ShoppingCart> GetUserCart();
        Task<int> RemoveItem(int bookId);
        Task<int> GetItemsCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
    }
}