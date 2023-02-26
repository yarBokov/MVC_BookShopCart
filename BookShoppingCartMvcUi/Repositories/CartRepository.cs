using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUi.Repositories
{
    public class CartRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHttpContextAccessor contextAccessor;

        public CartRepository(ApplicationDbContext dbContext, 
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor contextAccessor)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.contextAccessor = contextAccessor;
        }
        public async Task<bool> AddItem(int bookId, int qty)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            try
            {

                string userId = GetUserID();
                if (string.IsNullOrEmpty(userId))
                    return false;
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UsedId = userId
                    };
                    dbContext.ShoppingCarts.Add(cart);
                }
                dbContext.SaveChanges();
                var cartItem = dbContext.CartDetails.FirstOrDefault(
                    x => x.ShoppingCartId== cart.Id && x.BookId == bookId );
                if (cartItem is not null) 
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    cartItem = new CartDetail
                    {
                        BookId = bookId,
                        ShoppingCartId= cart.Id,
                        Quantity= qty
                    };
                    dbContext.CartDetails.Add(cartItem);
                }
                dbContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await dbContext.ShoppingCarts.FirstOrDefaultAsync(x => x.UsedId == userId);
            return cart;
        }

        private string GetUserID()
        {
            var user = contextAccessor.HttpContext.User;
            string userId = userManager.GetUserId(user);
            return userId;
        }
    }
}
