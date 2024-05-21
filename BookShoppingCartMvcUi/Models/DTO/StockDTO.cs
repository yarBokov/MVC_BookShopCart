using System.ComponentModel.DataAnnotations;

namespace BookShoppingCartMvcUi.Models.DTO
{
    public class StockDTO
    {
        public int BookId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Количество должно быть неотрицательным.")]
        public int Quantity { get; set; }
    }
}
