namespace allspice_cs.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repo;
  private readonly RecipesService _recipesService;

  public FavoritesService(FavoritesRepository repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }

  public FavoriteRecipe createFavorite(Favorite favoriteData)
  {
    Recipe recipe = _recipesService.GetRecipeById(favoriteData.RecipeId);
    FavoriteRecipe favorite = _repo.CreateFavorite(favoriteData);
    return favorite;
  }

  public Favorite GetFavorite(int favoriteId)
  {
    Favorite favorite = _repo.GetFavorite(favoriteId);
    if (favorite == null) throw new Exception($"No favorite with the id of: {favoriteId}");
    return favorite;
  }

  public string DeleteFavorite(int favoriteId, string userId)
  {
    Favorite favoriteToDelete = GetFavorite(favoriteId);
    if (favoriteToDelete.AccountId != userId) throw new Exception($"That's not your favorite to delete, dude!");
    _repo.DeleteFavorite(favoriteId);
    return $"Favorite with the id of {favoriteId} has been deleted permanently";
  }

  public List<FavoriteRecipe> GetFavorites(string userId)
  {
    List<FavoriteRecipe> favoriteRecipes = _repo.GetFavoriteRecipes(userId);
    return favoriteRecipes;
  }

  public List<FavoriteProfile> GetFavoriteProfiles(int recipeId)
  {
    _recipesService.GetRecipeById(recipeId);
    List<FavoriteProfile> favorites = _repo.GetFavoriteProfiles(recipeId);
    return favorites;
  }
}