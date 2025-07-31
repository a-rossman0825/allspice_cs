
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace allspice_cs.Services;

public class RecipesService
{
  private readonly RecipesRepository _repo;

  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }


  public Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repo.CreateRecipe(recipeData);
    return recipe;
  }

  public List<Recipe> GetRecipes()
  {
    throw new NotImplementedException();
  }

  public Recipe GetRecipeById(int recipeId)
  {
    throw new NotImplementedException();
  }

  public string DeleteRecipe(int recipeId, string userId)
  {
    Recipe recipeToDelete = GetRecipeById(recipeId);

    if (recipeToDelete.CreatorId != userId)
    {
      throw new Exception("https://media.tenor.com/KXg4SlhH0BsAAAAM/mj-michael-jordan.gif");
    }

    _repo.DeleteRecipe(recipeId);

    return $"Your {recipeToDelete.Title} recipe has been deleted!";
  }

  public Recipe UpdateRecipe(int recipeId, Recipe recipeUpdateData, Account userInfo)
  {
    throw new NotImplementedException();
  }
}
