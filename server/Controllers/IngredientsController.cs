

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

  [HttpPost]
  public ActionResult<Ingredient> AddIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Ingredient ingredient = _service.AddIngredient(ingredientData);
      return Ok(ingredient);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}