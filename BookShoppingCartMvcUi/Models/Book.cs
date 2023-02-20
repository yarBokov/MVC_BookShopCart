using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShoppingCartMvcUi.Models
{
    [Table("Book")]
    public class Book
    {
        public int Id { get; set; }

        [Required, MaxLength(40)]
        public string? BookName { get; set; }
        [Required, MaxLength(60)]
        public string? AuthorName { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<OrderDetail> OrderDetailList { get; set; }
        public List<CartDetail> CartDetailList { get; set; }
    }
}
