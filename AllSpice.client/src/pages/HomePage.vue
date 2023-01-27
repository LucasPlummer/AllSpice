<template>
  <div class="container-fluid">
    <!-- SECTION button row -->
    <div class="row justify-content-center d-flex">
      <div class="col-8 bg-light rounded d-flex elevation-5 p-2 rounded justify-content-evenly m-2">
        <button v-if="account.id" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#createModal">New
          Recipe</button>
        <button class="btn btn-success fw-bold p-2" @click="getRecipes()">All</button>
        <button class="btn btn-success fw-bold p-2">Favorites</button>
        <button class="btn btn-success fw-bold p-2" @click="getAccountRecipes()">Your recipes</button>
      </div>
    </div>
    <!-- SECTION recipe modal stuff -->

    <div class="row">
      <div v-for="r in recipes" class="col-12 col-md-3 mb-3 p-4">
        <RecipeCard :recipe="r" data-bs-toggle="modal" data-bs-target="#recipeModal" />
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted } from 'vue';
import { computed } from '@vue/reactivity';
import Pop from '../utils/Pop.js';
import RecipeCard from '../components/RecipeCard.vue'
import { recipesService } from '../services/RecipesService.js';
import { AppState } from '../AppState.js';
import { accountService } from '../services/AccountService.js';

export default {
  setup() {


    async function getRecipes() {
      try {
        await recipesService.getRecipes()
      } catch (error) {
        Pop.error
      }
    }
    onMounted(() => {
      getRecipes();
    })
    return {
      account: computed(() => AppState.account),
      recipes: computed(() => AppState.recipes),

      async getAccountRecipes() {
        try {
          await accountService.getAccountRecipes();
        } catch (error) {
          Pop.error
        }
      },

      async getRecipes() {
        try {
          await recipesService.getRecipes()
        } catch (error) {
          Pop.error
        }
      }
    }


  },
  components: { RecipeCard }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
