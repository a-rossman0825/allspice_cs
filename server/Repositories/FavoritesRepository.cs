

namespace allspice_cs.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  public FavoriteRecipe CreateFavorite(Favorite favoriteData)
{
  string sql = @"
  INSERT INTO favorites
  (account_id, recipe_id)
  VALUES
  (@AccountId, @RecipeId);

  SELECT
    favorites.id AS favoriteId,
    favorites.account_id,
    recipes.*,
    accounts.*
  FROM favorites
  JOIN recipes ON favorites.recipe_id = recipes.id
  JOIN accounts ON recipes.creator_id = accounts.id
  WHERE favorites.id = LAST_INSERT_ID()
  ;";

  FavoriteRecipe favorite = _db.Query<FavoriteRecipe, Profile, FavoriteRecipe>(
    sql,
    (favorite, profile) =>
    {
      favorite.Creator = profile;
      return favorite;
    },
    favoriteData,
    splitOn: "id"
  ).SingleOrDefault();

  return favorite;
}

  internal void DeleteFavorite(int favoriteId)
  {
    string sql = @"
    DELETE FROM favorites
    WHERE id = @favoriteId
    LIMIT 1
    ;";
    _db.Execute(sql, new { favoriteId });
  }

  public List<FavoriteRecipe> GetFavoriteRecipes(string userId)
  {
    string sql = @"
    SELECT
      favorites.id AS favoriteId,
      favorites.account_id,
      recipes.*,
      accounts.*
    FROM favorites
    JOIN recipes ON favorites.recipe_id = recipes.id
    JOIN accounts ON recipes.creator_id = accounts.id
    WHERE account_id = @userId
    ;";
    List<FavoriteRecipe> favoriteRecipes = _db.Query(sql, (FavoriteRecipe favoriteRecipe, Profile profile) =>
    {
      favoriteRecipe.Creator = profile;
      return favoriteRecipe;
    }, new { userId }).ToList();
    return favoriteRecipes;
  }

  internal List<FavoriteProfile> GetFavoriteProfiles(int recipeId)
  {
    string sql = @"
    SELECT
      favorites.id AS favoriteId,
      favorites.recipe_id,
      accounts.*
    FROM favorites
    JOIN accounts ON favorites.account_id = accounts.id
    WHERE recipe_id = @recipeId
    ;";
    List<FavoriteProfile> favorites = _db.Query<FavoriteProfile>(sql, new { recipeId }).ToList();
    return favorites;
  }

  internal Favorite GetFavorite(int favoriteId)
  {
    string sql = @"
    SELECT
    *
    FROM favorites
    WHERE id = @favoriteId
    ;";
    Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).SingleOrDefault();
    return favorite; 
  }
}