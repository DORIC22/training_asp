using System.ComponentModel.DataAnnotations.Schema;

namespace training_asp.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int GoodsId { get; set; }

        public int Count { get; set; }

        // Навигационные свойства
        public User User { get; set; }
        public Goods Goods { get; set; }
    }
}
