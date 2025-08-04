import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Recipe } from "@/models/Recipe.js";


class RecipesService {
  async getRecipes() {
    const res = await api.get('api/recipes');
    logger.log('Got Recipes!', res.data);
    AppState.recipes = res.data.map((pojo) => new Recipe(pojo));
  }


}

export const recipesService = new RecipesService();