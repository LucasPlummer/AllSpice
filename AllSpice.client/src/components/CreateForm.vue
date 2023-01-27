<template>
    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
    <h1>Create a Recipe</h1>
    <form @submit.prevent="createRecipe()">
        <div class="modal-body">

            <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.title" type="text" required class="form-control" id="title"
                    placeholder="Title">
                <label for="title">Title</label>
            </div>

            <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.instructions" type="text" required class="form-control" id="instructions"
                    placeholder="Instructions">
                <label for="instructions">Instructions</label>
            </div>

            <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.img" type="url" required class="form-control" id="img" placeholder="Img">
                <label for="img">Img</label>
            </div>

            <div class="mb-3 elevation-5">
                <select required name="category" v-model="editable.category" id="floatingSelect">
                    <option value="Misc">Misc</option>
                    <option value="Cheese">Cheese</option>
                    <option value="Breakfast">Breakfast</option>
                    <option value="Lunch">Lunch</option>
                    <option value="Dinner">Dinner</option>
                </select>
                <label for="floatingSelect">category</label>
            </div>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary">Create</button>
        </div>
    </form>
</template>


<script>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { Modal } from 'bootstrap';
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { recipesService } from '../services/RecipesService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
export default {
    setup() {
        const editable = ref({})
        const router = useRouter()
        return {
            editable,
            async createRecipe() {
                try {
                    const recipe = await recipesService.createRecipe(editable.value)
                    editable.value = {}
                    Modal.getOrCreateInstance('#recipeModal').hide()
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