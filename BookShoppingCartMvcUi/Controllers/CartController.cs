using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCartMvcUi.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        public IActionResult AddItem(int bookId, int qty=1)
        {
            return View();
        }

        public IActionResult RemoveItem(int bookId)
        {
            return View();
        }

        public IActionResult GetUserCart()
        {
            return View();
        }

        public IActionResult GetTotalItemInCart()
        {
            return View();
        }
    }
}
