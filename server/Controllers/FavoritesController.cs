namespace allspice_cs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
  private readonly FavoritesService _favoritesService;
  private readonly Auth0Provider _auth;

  public FavoritesController(Auth0Provider auth, FavoritesService favoritesService)
  {
    _auth = auth;
    _favoritesService = favoritesService;
  }

  [HttpPost, Authorize]
  public async Task<ActionResult<FavoriteRecipe>> CreateFavorite([FromBody] Favorite favoriteData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      favoriteData.AccountId = userInfo.Id;
      FavoriteRecipe favorite = _favoritesService.createFavorite(favoriteData);
      return Ok(favorite);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpDelete("{favoriteId}"), Authorize]
  public async Task<ActionResult<string>> DeleteFavorite(int favoriteId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string deleteMsg = _favoritesService.DeleteFavorite(favoriteId, userInfo.Id);
      return Ok(deleteMsg);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}