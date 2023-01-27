<template>
    <div class="">
        <div class="bg-white elevation-1 mx-3 p-2">
            <h4>name: {{ ingredient.name }}</h4>
            <h4>quantity: {{ ingredient.quantity }}</h4>
            <div v-if="account.id == recipe.creatorId" class="text-end">
                <i class="mdi mdi-delete selectable text-danger fs-3" @click="removeIngredient(ingredient.id)"></i>
            </div>
        </div>

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { ingredientsService } from '../services/IngredientsService.js';
import { recipesService } from '../services/RecipesService.js';
export default {
    props: {
        ingredient: { type: Object, required: true }
    },
    setup() {
        return {
            recipe: computed(() => AppState.activeRecipe),
            account: computed(() => AppState.account),
            async removeIngredient(ingredientId) {
                try {
                    await ingredientsService.removeIngredient(ingredientId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>

</style>