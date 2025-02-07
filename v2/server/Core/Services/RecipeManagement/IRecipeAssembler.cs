using server.Core.Services.RecipeManagement.DTOs;

namespace server.Core.Services.RecipeManagement;

public interface IRecipeAssembler
{
    Task<RecipeDTO> AssembleAsync(Recipe recipe);
}