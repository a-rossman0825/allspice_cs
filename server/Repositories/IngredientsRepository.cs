

namespace allspice_cs.Repositories;



public class IngredientsRepository
{

  private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Ingredient AddIngredient(Ingredient ingredientData)
  {
    string sql = @"
      INSERT INTO ingredients
      (name, quantity, recipe_id)
      VALUES
      (@Name, @Quantity, @RecipeId);

      SELECT 
        ingredients.*,
        recipes.*
      FROM ingredients
      JOIN recipes ON ingredients.recipe_id = recipes.id
      WHERE ingredients.id = LAST_INSERT_ID()
    ;";

    Ingredient ingredient = _db.Query(sql, (Ingredient ingredient, Recipe recipe) =>
    {
      ingredient.RecipeId = recipe.Id;
      return ingredient;
    }, ingredientData).SingleOrDefault();
    return ingredient;
  }

  public List<Ingredient> GetIngredientsForRecipe(int recipeId)
  {
    string sql = @"
    SELECT
        recipes.id AS recipeId,
        ingredients.*,
        recipes.*
    FROM ingredients
    JOIN recipes ON ingredients.recipe_id = recipes.id
    WHERE recipe_id = @recipeId
    ;";

    List<Ingredient> ingredients = _db.Query(sql, (Ingredient ingredient, Recipe recipe) =>
    {
      ingredient.RecipeId = recipe.Id;
      return ingredient;
    }, new { recipeId }).ToList();
    return ingredients;
  }

  public void DeleteIngredient(int ingredientId)
  {
    throw new NotImplementedException();
  }
}