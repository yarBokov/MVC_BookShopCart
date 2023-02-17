using System.ComponentModel.DataAnnotations;

namespace BookShoppingCartMvcUi.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string? StatusName { get; set; }
    }
}
