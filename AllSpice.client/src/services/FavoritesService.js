import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js"


class FavoritesService {

    async getFavorites() {
        const res = await api.get('api/favorites')
        logger.log('getall favorites', res.data)
        AppState.recipes = res.data
    }

    async favoriteRecipe(recipe) {
        const res = await api.post(`api/favorites`, recipe)
        logger.log('favorite a recipe', res.data)
        AppState.favorites = res.data
    }

    async removeFavorite(favoriteId)
}


export const favoritesService = new FavoritesService()