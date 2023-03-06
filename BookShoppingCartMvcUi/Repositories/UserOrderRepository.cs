using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUi.Repositories
{
    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> userManager;

        public UserOrderRepository(ApplicationDbContext applicationDbContext,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.applicationDbContext = applicationDbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }
        public async Task<IEnumerable<Order>> UserOrders()
        {
            var userId = GetUserID();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("user is not logged-in");
            var orders = await applicationDbContext.Orders
                                             .Include(x => x.OrderDetailList)
                                             .Include(x => x.OrderStatus)
                                             .ThenInclude(x => x.Book)
                                             .ThenInclude(x => x.Genre)
                                             .Where(a => a.UserId == userId)
                                             .ToListAsync();
            return orders;
        }

        private string GetUserID()
        {
            var user = httpContextAccessor.HttpContext.User;
            string userId = userManager.GetUserId(user);
            return userId;
        }
    }
}
