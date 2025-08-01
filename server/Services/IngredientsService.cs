

namespace allspice_cs.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repo;

  public IngredientsService(IngredientsRepository repo)
  {
    _repo = repo;
  }

  public Ingredient AddIngredient(Ingredient ingredientData)
  {
    
    Ingredient ingredient = _repo.AddIngredient(ingredientData);
    return ingredient;
  }
}