<template>
    <div v-if="recipe" class="component">
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        <button type="button" class="btn btn-danger" v-if="account.id == recipe.creatorId"
            @click="removeRecipe()">delete recipe</button>
        <h1 class="p-2">{{ recipe.title }}</h1>
        <h2 class="p-2">category: {{ recipe.category }}</h2>
        <img class="p-3 img-fluid card-img" :src="recipe.img" alt="">
        <h4 class="p-2">Instructions: {{ recipe.instructions }}</h4>
        <h5 class="p-2 mt-3">Ingredients:</h5>
        <div class="row">
            <div v-for="i in ingredients">
                <IngredientComponent :ingredient="i" />
            </div>
        </div>

        <form @submit.prevent="createIngredient()" v-if="account.id == recipe.creatorId">
            <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.name" type="text" required class="form-control" id="name" placeholder="Name">
                <label for="name">Name</label>
            </div>

            <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.quantity" type="text" required class="form-control" id="quantity"
                    placeholder="Quantity">
                <label for="Quantity">Quantity</label>
            </div>
            <div class="modal-footer">
                <button type="submit" class="btn btn-primary">
                    create ingredient
                </button>
            </div>
        </form>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js';
import { ingredientsService } from '../services/IngredientsService.js'
import IngredientComponent from '../components/IngredientComponent.vue';
export default {
    setup() {
        const editable = ref({});
        const route = useRoute();
        const router = useRouter();


        onMounted(() => {
        });
        return {
            editable,
            recipe: computed(() => AppState.activeRecipe),
            account: computed(() => AppState.account),
            ingredients: computed(() => AppState.ingredients),

            async createIngredient() {
                try {
                    editable.value.recipeId = AppState.activeRecipe.id;
                    const ingredient = await ingredientsService.createIngredient(editable.value)
                    editable.value = {}
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            },

            async removeRecipe() {
                try {
                    const recipeId = AppState.activeRecipe.id;
                    await recipesService.removeRecipe(recipeId);
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        }
    },
    components: { IngredientComponent }
};
</script>


<style lang="scss" scoped>
.card-img {
    height: 50vh;
    width: 100%;
    object-fit: cover;
}
</style>