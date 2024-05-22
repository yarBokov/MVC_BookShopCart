
namespace BookShoppingCartMvcUi.Repositories
{
    public interface IReportRepository
    {
        Task<IEnumerable<TopNSoldBookModel>> GetTopNSellingBooksByDate(DateTime startDate, DateTime endDate);
    }
}