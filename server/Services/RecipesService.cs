
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
    List<Recipe> recipes = _repo.GetRecipes();
    return recipes;
  }

  public Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repo.GetRecipeById(recipeId);
    if (recipe == null) throw new Exception($"No Recipe with id: {recipeId}");
    return recipe;
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
    Recipe originalRecipe = GetRecipeById(recipeId);
    if (originalRecipe.CreatorId != userInfo.Id)
    {
      throw new Exception($"Bruh, why you tryna update a recipe that ain\'t yours? you better run for the hills, {userInfo.Name}, the cops are on their way 🚓 🚓 🚓 🚓 🚓 🚓 🚓");
    }
    originalRecipe.Title = recipeUpdateData.Title ?? originalRecipe.Title;
    originalRecipe.Instructions = recipeUpdateData.Instructions ?? originalRecipe.Instructions;
    originalRecipe.Img = recipeUpdateData.Img ?? originalRecipe.Img;
    originalRecipe.Category = recipeUpdateData.Category ?? originalRecipe.Category;


    _repo.UpdateRecipe(originalRecipe);

    return originalRecipe;
  }
}
