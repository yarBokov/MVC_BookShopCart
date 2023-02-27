using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUi.Repositories
{
    public class CartRepository : ICartRepository
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
        public async Task<int> RemoveItem(int bookId, int qty)
        {
            string userId = GetUserID();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Invalid cart");
                var cartItem = dbContext.CartDetails.FirstOrDefault(
                    x => x.ShoppingCartId == cart.Id && x.BookId == bookId);
                if (cartItem is null)
                    throw new Exception("Not items in cart");
                else if (cartItem.Quantity == 1)
                    dbContext.CartDetails.Remove(cartItem);
                else
                    cartItem.Quantity -= 1;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetItemsCount(userId);
            return cartItemCount;
        }
        public async Task<int> AddItem(int bookId, int qty)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            string userId = GetUserID();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
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
                    x => x.ShoppingCartId == cart.Id && x.BookId == bookId);
                if (cartItem is not null)
                    cartItem.Quantity += qty;
                else
                {
                    cartItem = new CartDetail
                    {
                        BookId = bookId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty
                    };
                    dbContext.CartDetails.Add(cartItem);
                }
                dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetItemsCount(userId);
            return cartItemCount;
        }
        public async Task<ShoppingCart> GetUserCart()
        {
            var usedId = GetUserID();
            if (usedId == null)
                throw new Exception("Invalid userId");
            var shoppingCart = await dbContext.ShoppingCarts
                                        .Include(x => x.CartDetails)
                                        .ThenInclude(x => x.Book)
                                        .ThenInclude(x => x.Genre)
                                        .Where(x => x.UsedId == usedId).FirstOrDefaultAsync();
            return shoppingCart;
        }

        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await dbContext.ShoppingCarts.FirstOrDefaultAsync(x => x.UsedId == userId);
            return cart;
        }

        public async Task<int> GetItemsCount(string userId = "")
        {
            if (!string.IsNullOrEmpty(userId))
                userId = GetUserID();
            var data = await (from cart in dbContext.ShoppingCarts
                              join cartDetail in dbContext.CartDetails
                              on cart.Id equals cartDetail.ShoppingCartId
                              select new { cartDetail.Id }
                              ).ToListAsync();
            return data.Count;
        }

        private string GetUserID()
        {
            var user = contextAccessor.HttpContext.User;
            string userId = userManager.GetUserId(user);
            return userId;
        }
    }
}
