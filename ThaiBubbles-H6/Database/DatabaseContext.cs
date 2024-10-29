using ThaiBubbles_h6.Model;

namespace ThaiBubbles_H6.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Login> Login { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductList> ProductList { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            //Database.EnsureCreated(); 
            //Database.EnsureCreated
            //Database.Migrate();
        }
    }
}
