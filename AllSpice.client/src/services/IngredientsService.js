import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class IngredientsService {
    async getIngredients(recipeId) {
        const res = await api.get(`api/recipes/${recipeId}/ingredients`)
        logger.log('getting ingredients', res.data)
        AppState.ingredients = res.data
    }

    async createIngredient(ingredientData) {
        const res = await api.post(`api/ingredients`, ingredientData)
        logger.log(AppState.ingredients)
    }

    async removeIngredient(ingredientId) {
        const index = AppState.ingredients.findIndex(i => i.id == ingredientId)
        await api.delete(`api/ingredients/${ingredientId}`)
        AppState.ingredients.splice(index, 1)
        logger.log('deleting ingredient')
    }

}

export const ingredientsService = new IngredientsService()