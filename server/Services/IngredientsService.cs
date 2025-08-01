


using Microsoft.AspNetCore.Http.HttpResults;

namespace allspice_cs.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repo;
  private readonly RecipesService _recipesService;

  public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }

  public Ingredient AddIngredient(Ingredient ingredientData, Account userInfo)
  {
    Recipe recipe = _recipesService.GetRecipeById(ingredientData.RecipeId);
    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception("This ain't your Recipe, Hombré, what're you trying to do here? 🔫🤠");
    }

    Ingredient ingredient = _repo.AddIngredient(ingredientData);
    return ingredient;
  }

  public List<Ingredient> GetIngredientsForRecipe(int recipeId)
  {
    List<Ingredient> ingredients = _repo.GetIngredientsForRecipe(recipeId);
    return ingredients;
  }

  internal string DeleteIngredient(int ingredientId, Account userInfo)
  {
    Ingredient ingredient = FindIngredientById(ingredientId);
    Recipe recipe = _recipesService.GetRecipeById(ingredient.RecipeId);

    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception($"This Ain't Your Recipe Ingredient, {userInfo.Name}, get lost! 😡😡😡😡😡");
    }
    
    _repo.DeleteIngredient(ingredientId);
    
    return $"Your ingredient: \"{ingredient.Name}\" has been deleted!";
  }

  private Ingredient FindIngredientById(int ingredientId)
  {
    Ingredient ingredient = _repo.FindIngredientById(ingredientId);

    if (ingredient == null)
    {
      throw new Exception($"No Ingredient with the id: {ingredientId}");
    }

    return ingredient;
  }
}