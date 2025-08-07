import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Recipe } from "@/models/Recipe.js";


class RecipesService {



  async AddRecipe(recipeData) {
    const res = await api.post(`api/recipes`, recipeData);
    logger.log('Added Recipe!', res.data);
    const recipe = new Recipe(res.data);
    AppState.recipes.push(recipe);
    return recipe.id;
  }
  
  async getRecipes() {
    const res = await api.get('api/recipes');
    logger.log('Got Recipes!', res.data);
    AppState.recipes = res.data.map((pojo) => new Recipe(pojo));
  }

  async getRecipeById(recipeId){
    const res = await api.get(`api/recipes/${recipeId}`);
    logger.log('Got Recipe By Id: ', res.data);
    const recipe = res.data.map((pojo) => new Recipe(pojo));
    return recipe;
  }

  updateRecipe(recipeId) {
    throw new Error("new error", recipeId);
  }

}

export const recipesService = new RecipesService();