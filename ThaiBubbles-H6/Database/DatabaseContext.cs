
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
        // Seed initial data
        public static void SeedData(DatabaseContext context)
        {

            if (!context.City.Any())
            {
                context.City.AddRange(
                    new City { CityName = "Copenhagen S", ZIPCode =2300},
                    new City { CityName = "Copenhagen N", ZIPCode = 2200},
                    new City { CityName = "Copenhagen NV", ZIPCode = 2400 },
                    new City { CityName = "Copenhagen SV", ZIPCode = 2450},
                    new City { CityName = "Copenhagen Ø", ZIPCode = 2100 },
                    new City { CityName = "Valby", ZIPCode = 2500 },
                    new City { CityName = "Glostrup", ZIPCode = 2600 }
                    );

            }

            // Seed Roles
            if (!context.Role.Any())
            {
                context.Role.AddRange(
                    new Role { RoleType = "Admin" },
                    new Role { RoleType = "Customer" }
                );
                context.SaveChanges();
            }


            // Seed Categories
            if (!context.Category.Any())
            {
                context.Category.AddRange(
                    new Category { CategoryName = "Fruity Tea" },
                    new Category { CategoryName = "Fruity Milk" },
                    new Category { CategoryName = "Milk Tea" },
                    new Category { CategoryName = "Speciality" },
                    new Category { CategoryName = "Brown Sugar" },
                    new Category { CategoryName = "Pure Tea" }
                );
                context.SaveChanges();
            }

            // Seed other entities if needed

            if (!context.Product.Any())
            {
                context.Product.AddRange(
                new Product { Name = "Passionfruit", Price = 40, CategoryId = 1 },
                new Product { Name = "Mango", Price = 40, CategoryId = 1 },
                new Product { Name = "Strawberry", Price = 40, CategoryId = 1 },
                new Product { Name = "Blueberry", Price = 40, CategoryId = 1 },
                new Product { Name = "Pomegranate", Price = 40, CategoryId = 1 },
                new Product { Name = "Lychee", Price = 40, CategoryId = 1 },
                new Product { Name = "Green Apple", Price = 40, CategoryId = 1 },
                new Product { Name = "Pineapple", Price = 40, CategoryId = 1 },
                new Product { Name = "Watermelon", Price = 40, CategoryId = 1 },
                new Product { Name = "Cherry", Price = 40, CategoryId = 1 },
                new Product { Name = "Grapefruit", Price = 40, CategoryId = 1 },
                new Product { Name = "Cranberry", Price = 40, CategoryId = 1 },
                new Product { Name = "White Grape", Price = 40, CategoryId = 1 },
                new Product { Name = "Grape", Price = 40, CategoryId = 1 },
                new Product { Name = "Kiwi", Price = 40, CategoryId = 1 },
                new Product { Name = "Pear", Price = 40, CategoryId = 1 },
                new Product { Name = "Red Guava", Price = 40, CategoryId = 1 },
                new Product { Name = "Peach", Price = 40, CategoryId = 1 },
                new Product { Name = "Hibiscus", Price = 40, CategoryId = 1 },
                new Product { Name = "Lemon", Price = 40, CategoryId = 1 },
                new Product { Name = "Kumquat Lemon", Price = 40, CategoryId = 1 },
                new Product { Name = "Honeydew Melon", Price = 40, CategoryId = 1 },
                new Product { Name = "Passionfruit", Price = 40, CategoryId = 2 },
                new Product { Name = "Mango", Price = 40, CategoryId = 2 },
                new Product { Name = "Strawberry", Price = 40, CategoryId = 2 },
                new Product { Name = "Blueberry", Price = 40, CategoryId = 2 },
                new Product { Name = "Pomegranate", Price = 40, CategoryId = 2 },
                new Product { Name = "Lychee", Price = 40, CategoryId = 2 },
                new Product { Name = "Green Apple", Price = 40, CategoryId = 2 },
                new Product { Name = "Pineapple", Price = 40, CategoryId = 2 },
                new Product { Name = "Watermelon", Price = 40, CategoryId = 2 },
                new Product { Name = "Cherry", Price = 40, CategoryId = 2 },
                new Product { Name = "Grapefruit", Price = 40, CategoryId = 2 },
                new Product { Name = "Cranberry", Price = 40, CategoryId = 2 },
                new Product { Name = "Green Apple", Price = 40, CategoryId = 2 }, // Duplicate Green Apple
                new Product { Name = "White Grape", Price = 40, CategoryId = 2 },
                new Product { Name = "Grape", Price = 40, CategoryId = 2 },
                new Product { Name = "Kiwi", Price = 40, CategoryId = 2 },
                new Product { Name = "Pear", Price = 40, CategoryId = 2 },
                new Product { Name = "Red Guava", Price = 40, CategoryId = 2 },
                new Product { Name = "Peach", Price = 40, CategoryId = 2 },
                new Product { Name = "Hibiscus", Price = 40, CategoryId = 2 },
                new Product { Name = "Lemon", Price = 40, CategoryId = 2 },
                new Product { Name = "Honeydew Melon", Price = 40, CategoryId = 2 },
                new Product { Name = "Taro", Price = 40, CategoryId = 3 },
                new Product { Name = "Taiwan Assam Milk Tea", Price = 40, CategoryId = 3 },
                new Product { Name = "Jasmine Green Milk Tea", Price = 40, CategoryId = 3 },
                new Product { Name = "Osmanthus Oolong Milk Tea", Price = 40, CategoryId = 3 },
                new Product { Name = "Roasted Oolong Milk Tea", Price = 40, CategoryId = 3 },
                new Product { Name = "Taro Coconut", Price = 40, CategoryId = 3 },
                new Product { Name = "Thai Milk Tea", Price = 40, CategoryId = 3 },
                new Product { Name = "Matcha", Price = 40, CategoryId = 3 },
                new Product { Name = "Coffee", Price = 40, CategoryId = 3 },
                new Product { Name = "Caramel", Price = 40, CategoryId = 3 },
                new Product { Name = "Hazelnut", Price = 40, CategoryId = 3 },
                new Product { Name = "Vanilla", Price = 40, CategoryId = 3 },
                new Product { Name = "Almond", Price = 40, CategoryId = 3 },
                new Product { Name = "Wintermelon", Price = 40, CategoryId = 3 },
                new Product { Name = "Coconut", Price = 40, CategoryId = 3 },
                new Product { Name = "Chocolate", Price = 40, CategoryId = 3 },
                new Product { Name = "Surprise Me", Price = 40, CategoryId = 4 },
                new Product { Name = "Green Energy", Price = 40, CategoryId = 4 },
                new Product { Name = "Sweet Memories", Price = 40, CategoryId = 4 },
                new Product { Name = "Dreaming Berry", Price = 40, CategoryId = 4 },
                new Product { Name = "Energize Me", Price = 40, CategoryId = 4 },
                new Product { Name = "Peanut Coffee Milk Tea", Price = 40, CategoryId = 4 },
                new Product { Name = "Matcha Coffee Milk Tea", Price = 40, CategoryId = 4 },
                new Product { Name = "Caramel Oolong Milk Tea", Price = 40, CategoryId = 4 },
                new Product { Name = "Brown Sugar Milk", Price = 40, CategoryId = 5 },
                new Product { Name = "Brown Sugar Assam Milk Tea", Price = 40, CategoryId = 5 },
                new Product { Name = "Brown Sugar Oolong Milk Tea", Price = 40, CategoryId = 5 },
                new Product { Name = "Brown Sugar Wintermelon", Price = 40, CategoryId = 5 },
                new Product { Name = "Brown Sugar Matcha Milk", Price = 40, CategoryId = 5 },
                new Product { Name = "Brown Sugar Taro", Price = 40, CategoryId = 5 },
                new Product { Name = "Brown Sugar Chocolate", Price = 40, CategoryId = 5 },
                new Product { Name = "Brown Sugar Peanut", Price = 40, CategoryId = 5 },
                new Product { Name = "Taiwan Assam Black Tea", Price = 40, CategoryId = 6 },
                new Product { Name = "Roasted Oolong Tea", Price = 40, CategoryId = 6 },
                new Product { Name = "Wintermelon Black Tea", Price = 40, CategoryId = 6 },
                new Product { Name = "Jasmine Green Tea", Price = 40, CategoryId = 6 },
                new Product { Name = "Osmanthus Oolong Tea", Price = 40, CategoryId = 6 },
                new Product { Name = "Sakura Green Tea", Price = 40, CategoryId = 6 }

                );
                context.SaveChanges();
            }
        }

    }
}


