import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Ingredient } from "@/models/Ingredient.js";



class IngredientsService {
  async createIngredient(ingredientData) {
    const res = await api.post(`api/ingredients`, ingredientData);
    logger.log('Added Ingredient', res.data);
    const ingredient = new Ingredient(res.data);
    AppState.ingredients.push(ingredient);
  }

  async getIngredientsForRecipe(recipeId){
    const res = await api.get(`api/recipes/${recipeId}/ingredients`);
    logger.log('Got Ingredients for Recipe!', res.data);
    AppState.ingredients = res.data.map((pojo) => new Ingredient(pojo));
  }

}

export const ingredientsService = new IngredientsService();