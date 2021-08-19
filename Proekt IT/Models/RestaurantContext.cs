using System.Data.Entity;

namespace Proekt_IT.Models
{
    public class RestaurantContext: DbContext
    {
        public DbSet<Table> tables { get; set; }
        public DbSet<Menu> menu { get; set; }
        public DbSet<ShoppingCart> shoppingCart { get; set; }

        public RestaurantContext() : base("DefaultConnection") { }
        public static RestaurantContext Create()
        {
            return new RestaurantContext();
        }
    }
}