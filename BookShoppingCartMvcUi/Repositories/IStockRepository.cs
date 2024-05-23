

namespace BookShoppingCartMvcUi.Repositories
{
    public interface IStockRepository
    {
        Task<Stock?> GetStockByBookId(int bookId);
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "");
        Task ManageStock(StockDTO stockToManage);
    }
}