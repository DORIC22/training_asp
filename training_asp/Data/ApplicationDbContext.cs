using Microsoft.EntityFrameworkCore;
using training_asp.Models;  // Для доступа к модели User

namespace training_asp.Data  // Пространство имен должно быть training_asp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
