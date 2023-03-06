using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCartMvcUi.Controllers
{
    [Authorize]
    public class UserOrderController : Controller
    {
        private readonly IUserOrderRepository userOrderRepository;

        public UserOrderController(IUserOrderRepository userOrderRepository)
        {
            this.userOrderRepository = userOrderRepository;
        }
        public async Task<IActionResult> UserOrders()
        {
            var orders = await userOrderRepository.UserOrders();
            return View(orders);
        }
    }
}
