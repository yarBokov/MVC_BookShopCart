﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShoppingCartMvcUi.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        [Required]
        public int OrderStatusId { get; set; }
        public bool IsDeleted { get; set; } = false;
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(30)]
        public string? Email { get; set; }
        [Required]
        public string? MobileNumber { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }
        [Required]
        [MaxLength(30)]
        public string? PaymentMethod { get; set; }
        public bool IsPaid { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
}
