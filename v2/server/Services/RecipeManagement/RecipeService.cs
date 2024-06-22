using server.Core.Services.RecipeManagement;
using server.Core.Infrastructure.MongoDB;

namespace server.Services.RecipeManagement;

public class RecipeService(IRecipeRepository recipeRepository) : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;

    public Task<Recipe> CreateRecipe()
    {
        var recipe = new Recipe
        {
            AuthorId = "123",
            Title = "Buns",
            Description = "This is my recipe for buns",
            Ingredients =
            [
                new Ingredient
                {
                    Name = "Flour",
                    Quantity = 400,
                    Unit = Unit.Gram,
                    Nutrients = [
                        new Nutrient
                        {
                            NutrientType = NutrientType.Carbohydrates,
                            Amount = 100,
                            Unit = Unit.Gram
                        }
                    ]
                },
                new Ingredient
                {
                    Name = "Water",
                    Quantity = 200,
                    Unit = Unit.Gram,
                    Nutrients = [
                        new Nutrient
                        {
                            NutrientType = NutrientType.Energy,
                            Amount = 0,
                            Unit = Unit.Kcal
                        }
                    ]
                }
            ],
            TotalNutrients =
            [
                new Nutrient
                {
                    NutrientType = NutrientType.Carbohydrates,
                    Amount = 75,
                    Unit = Unit.Gram
                },
                new Nutrient
                {
                    NutrientType = NutrientType.Protein,
                    Amount = 25,
                    Unit = Unit.Gram
                },
            ]
        };


        return _recipeRepository.CreateRecipe(recipe);

    }
}