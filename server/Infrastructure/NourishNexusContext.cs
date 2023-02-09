namespace server.Infrastructure;

public class NourishNexusContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    /*
    public DbSet<Avoidance> Avoidances => Set<Avoidance>();
    public DbSet<FoodItem> FoodItems => Set<FoodItem>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet <NutritionInfo> NutritionInfos => Set<NutritionInfo>();
    public DbSet <Category> Categories => Set<Category>();
    */


    public NourishNexusContext(DbContextOptions<NourishNexusContext> options) : base(options) {}
}