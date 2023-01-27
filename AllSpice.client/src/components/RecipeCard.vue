<template>
    <div @click="getRecipeById">

        <div class="elevation-5 rounded bg-light selectable">
            <img :src="recipe.img" alt="" class="img-fluid card-img rounded-top">
            <div class="fw-bold p-1">
                <h3>{{ recipe.title }}</h3>
                <h4>{{ recipe.category }}</h4>
            </div>

        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { logger } from '../utils/Logger.js';
import { useRoute } from 'vue-router';
import { computed, reactive, onMounted } from 'vue';
import { recipesService } from '../services/RecipesService.js';
import Pop from '../utils/Pop.js';
import { ingredientsService } from '../services/IngredientsService.js'
export default {
    props: {
        recipe: { Type: Object, required: true }

    },
    setup(props) {


        return {
            async getIngredients() {
                try {
                    await ingredientsService.getIngredients(props.recipe.id)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            },
            async getRecipeById() {
                try {
                    await recipesService.getRecipeById(props.recipe.id);
                    this.getIngredients()
                } catch (error) {
                    logger.error(error);
                    Pop.error(error.message);
                }
            }

        }

    }
};
</script>


<style lang="scss" scoped>
.card-img {
    height: 30vh;
    width: 100%;
    object-fit: cover;
}
</style>