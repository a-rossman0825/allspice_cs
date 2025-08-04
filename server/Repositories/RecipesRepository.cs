
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
    LIMIT 10 OFFSET 0
    ;";
    List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, MapCreator).ToList();
    return recipes;
  }

  public Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
    SELECT
      recipes.*,
      accounts.*
    FROM recipes
    JOIN accounts ON recipes.creator_id = accounts.id
    WHERE recipes.id = @recipeId
    ;";

    Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, MapCreator, new { recipeId }).SingleOrDefault();
    return recipe;
  }

  internal void UpdateRecipe(Recipe recipeUpdate)
  {
    string sql = @"
    UPDATE recipes SET
      title = @title,
      instructions = @instructions,
      img = @img,
      category = @category,
      creator_id = @creatorId
    WHERE id = @id
    ;";
    int rowsAffected = _db.Execute(sql, recipeUpdate);
    if (rowsAffected > 1) throw new Exception($"oh no, you fucked up! rows affected: { rowsAffected }");
  }

  internal void DeleteRecipe(int recipeId)
  {
    string sql = "DELETE FROM recipes WHERE id = @recipeId LIMIT 1;";

    _db.Execute(sql, new { recipeId });

  }
}