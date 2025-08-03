

namespace allspice_cs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  private readonly IngredientsService _service;
  private readonly Auth0Provider _auth;

  public IngredientsController(IngredientsService service, Auth0Provider auth)
  {
    _service = service;
    _auth = auth;
  }

  [HttpPost, Authorize]
  public async Task<ActionResult<Ingredient>> AddIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Ingredient ingredient = _service.AddIngredient(ingredientData, userInfo);
      return ingredient;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpDelete("{ingredientId}"), Authorize]
  public async Task<ActionResult<string>> DeleteIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string deleteMsg = _service.DeleteIngredient(ingredientId, userInfo);
      return Ok(deleteMsg);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}