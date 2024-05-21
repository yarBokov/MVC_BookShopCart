using System.ComponentModel.DataAnnotations;

namespace BookShoppingCartMvcUi.Models.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string GenreName { get; set; }
    }
}
