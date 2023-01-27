import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class RecipesService {
    async getRecipes() {
        const res = await api.get('api/recipes')
        logger.log('getall recipes', res.data)
        AppState.recipes = res.data
    }

    async getRecipeById(recipeId) {
        const res = await api.get(`api/recipes/${recipeId}`)
        logger.log('getting recipe by id', res.data)
        AppState.activeRecipe = res.data
    }

    async createRecipe(body) {
        const url = `api/recipes`
        const res = await api.post(url, body)
        logger.log('create recipe', res.data)
        AppState.recipes.push(res.data)
        AppState.myRecipes.push(res.data)
        return res.data
    }

    async removeRecipe(recipeId) {
        await api.delete(`api/recipes/${recipeId}`)
        AppState.recipes = AppState.recipes.filter(r => r.id != recipeId)
    }
}

export const recipesService = new RecipesService