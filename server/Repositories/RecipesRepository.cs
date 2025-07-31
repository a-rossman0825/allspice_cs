
namespace allspice_cs.Repositories;

public class RecipesRepository
{

  private readonly IDbConnection _db;
  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  private Recipe MapCreator(Recipe recipe, Profile profile)
  {
    recipe.Creator = profile;
    return recipe;
  }

  public Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO recipes 
      (title, instructions, img, category, creator_id)
    VALUES
      (@title, @instructions, @img, @category, @creatorId);

    SELECT
      recipes.*,
      accounts.*
      FROM recipes
      JOIN accounts ON recipes.creator_id = accounts.id
      WHERE recipes.id = LAST_INSERT_ID()
      ;";
    Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, MapCreator, recipeData).SingleOrDefault();
    return recipe;
  }

  public List<Recipe> GetRecipes()
  {
    string sql = @"
    SELECT
      recipes.*,
      accounts.*
    FROM recipes
    JOIN accounts ON recipes.creator_id = accounts.id
    ;";
    List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, MapCreator).ToList();
    return recipes;
  }

  public Recipe GetRecipeById(int recipeId)
  {
    throw new Exception();
  }

  internal void UpdateRecipe(Recipe recipeUpdate)
  {
    throw new Exception();
  }

  internal void DeleteRecipe(int recipeId)
  {
    string sql = "DELETE FROM recipes WHERE id = @recipeId LIMIT 1;";

    _db.Execute(sql, new { recipeId });

  }
}