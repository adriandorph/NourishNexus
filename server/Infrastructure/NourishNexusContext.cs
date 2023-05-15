namespace server.Infrastructure;

public class NourishNexusContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<FoodItem> FoodItems => Set<FoodItem>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet <Category> Categories => Set<Category>();
    public DbSet <FoodItemRecipe> FoodItemRecipes => Set<FoodItemRecipe>();
    public DbSet <FoodItemMeal> FoodItemMeals => Set<FoodItemMeal>();
    public DbSet <RecipeMeal> RecipeMeals => Set<RecipeMeal>();

    public NourishNexusContext(DbContextOptions<NourishNexusContext> options) : base(options) {}

    
}