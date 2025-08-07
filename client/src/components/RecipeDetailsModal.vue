<script setup>
import { AppState } from "@/AppState.js";
import { ingredientsService } from "@/services/IngredientsService.js";
import { logger } from "@/utils/Logger.js";
import { Pop } from "@/utils/Pop.js";
import { computed, ref, watch } from "vue";

const activeRecipe = computed(() => AppState.activeRecipe);
const ingredients = computed(() => AppState.ingredients);
let addIngredientFormStatus = ref(false);

watch(activeRecipe, () => {
  getIngredients();
});

async function getIngredients() {
  try {
    await ingredientsService.getIngredientsForRecipe(activeRecipe.value.id);
  } catch (error) {
    Pop.error(error);
    logger.error("Could not GET Ingredients", error);
  }
}

// async function editRecipe() {
//   try {
//     await recipesService.updateRecipe(activeRecipe.value.id);
//   }
//   catch (error){
//     Pop.error(error);
//     logger.error("Could not PUT Recipe", error);
//   }
// }

let editableIngredientData = ref({
  name: '',
  quantity: '',
  recipeId: 0,
})

async function addIngredient() {
  try {
    editableIngredientData.value.recipeId = activeRecipe.value.id;
    await ingredientsService.createIngredient(editableIngredientData.value);
    editableIngredientData.value = {
      name: '',
      quantity: '',
      recipeId: 0
    }
  }
  catch (error){
    Pop.error(error);
  }
}

function openIngredientForm(){
  addIngredientFormStatus.value = !addIngredientFormStatus.value;
  logger.log(addIngredientFormStatus);
}

// function openIngredientModal(){
//   logger.log('click!');
//   Modal.getOrCreateInstance('#recipeDetailsModal').hide();
//   Modal.getOrCreateInstance('#ingredientModal').show();
// }

</script>

<template>
  <div v-if="activeRecipe" class="container-fluid p-0">
    <div class="row p-0">
      <div class="col-lg-6 p-0">
        <img
          :src="activeRecipe.imgUrl"
          :alt="`A picture of ${activeRecipe.creator.name}'s ${activeRecipe.title} Recipe`"
          class="img-fluid"
        />
        <button class="heart-button text-box">
          <i class="mdi mdi-heart-outline"
            ><span class="text-light">26</span></i
          >
          <!-- TODO v-if Favorite? -->
          <!-- <i class="mdi mdi-heart"><span class="text-light">26</span></i> -->
        </button>
      </div>
      <div class="col-lg-6 mt-4 ps-5 description-box">
        <div class="row align-items-center justify-content-start d-flex">
          <h1 class="main-font text-success col-12">
            {{ activeRecipe.title }}
          </h1>
          <!-- TODO Dropdown button delete/edit recipe Modals -->
          <!-- TODO THEME COLOR CHANGES WITH THEME CHANGE -->
        </div>
        <div class="row">
          <h5>by: {{ activeRecipe.creator.name }}</h5>
        </div>
        <div class="row align-items-center">
          <button
            class="btn btn-secondary rounded-pill category-btn col-3 mb-3"
          >
            {{ activeRecipe.category }}
          </button>
          <p
            role="button"
            class="mdi mdi-dots-horizontal fs-1 col-1"
            id="dropdownMenu"
            data-bs-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="true"
            :title="`Edit or Delete ${activeRecipe.title} recipe`"
          ></p>
          <div
            class="dropdown-menu text-start dropdown-size ps-2 bg-secondary"
            aria-labelledby="dropdownMenu"
          >
            <button class="dropdown-item p-0 mt-1 fs-4 edit-btn" type="button">
              Edit Recipe
            </button>
            <hr />
            <button
              class="dropdown-item p-0 pb-1 fs-4 delete-btn"
              type="button"
            >
              Delete Recipe
            </button>
          </div>
        </div>
        <div class="row">
          <h3>Ingredients <span @click="openIngredientForm()" role="button" class="mdi mdi-plus-thick text-success"></span></h3>
          
        </div>
        <!-- TODO V-For Ingredient in Ingredients -->
        <div
          v-if="ingredients.length > 0"
          class="row ingredients-box mb-3" :class="addIngredientFormStatus ? 'border-none' : 'border-bottom'"
        >
          <p v-for="ingredient in ingredients" :key="`ingredient-details-list-` + ingredient.id">
            {{ ingredient.quantity }} {{ ingredient.name }} <span class="mdi mdi-delete-forever" :title="`delete ingredient: ${ingredient.name}`"></span>
          </p>
        </div>
        <div v-else-if="ingredients.length <= 0 && !addIngredientFormStatus">
          <span role="button" @click="openIngredientForm()" class="text-primary">Click To Add Ingredients...</span>
        </div>
        <div v-if="addIngredientFormStatus">
          <form @submit.prevent="addIngredient()" class="row form-floating">
            <div class="col-10 form-floating mb-2">
              <textarea v-model="editableIngredientData.name" id="name" name="name" placeholder="enter ingredient name..." class="form-control" rows="1"></textarea>
              <label for="name" class="ms-2">enter ingredient name...</label>
            </div>
            <div class="col-10 form-floating">
              <textarea v-model="editableIngredientData.quantity" id="quantity" name="quantity" placeholder="enter ingredient amount..." class="form-control" rows="1"></textarea>
              <label for="quantity" class="ms-2">enter ingredient amount...</label>
              <button class="add-ingredient-btn text-primary btn" role="submit"><span class="mdi mdi-plus-thick text-success fs-1"></span>Add Ingredient...</button>
            </div>
          </form>
        </div>
        <div class="row">
          <h3>Instructions</h3>
          <p v-if="activeRecipe.instructions" class="instructions-box px-3">
            {{ activeRecipe.instructions }}
          </p>
          <p v-else class="instructions-box px-3">
            Click to Add Instructions...
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.mdi-dots-horizontal {
  left: 90%;
  top: -10px;
  color: var(--bs-secondary);

  transition: all 0.2s ease-in-out;

  &:hover {
    color: rgb(0, 0, 0);
  }

  &:active {
    color: var(--bs-secondary);
  }
}

.mdi-delete-forever {

  opacity: 60%;

  transition: all .3s ease-in-out;

  &:hover {
    color: var(--bs-danger);
    font-size: 1.3rem;
    opacity: 100%;
    cursor: pointer;
  }
}

.add-ingredient-btn {
  transition: all .2s ease-in-out;

  &:hover {
    cursor: copy;
    transform: translateY(-1px);
  }

  &:active {
    cursor: default;
    transform: translateY(1px);
    color: purple !important;
  }
}

.btn-success {
  width: 8px;
  height: 50px;
}

.form-control {
  width: 75%;
}

label {
  width: 75%;
}

.description-box {
  max-height: 610px;
  overflow-y: scroll;
}


.ingredients-box {
  width: 100%;
}

.dropdown-size {
  width: 100px;
}

.dropdown-item {
  &:hover {
    background-color: var(--bs-secondary);
  }
}

.edit-btn {
  transition: all 0.2s ease-in-out;

  &:hover {
    color: #fff;
  }

  &:active {
    color: black;
  }
}

.delete-btn {
  color: var(--bs-danger);

  transition: all 0.2s ease-in-out;

  &:hover {
    color: #fff;
  }

  &:active {
    color: var(--bs-danger);
  }
}

.heart-button {
  border: none;
  font-size: 3ch;
  position: absolute;
  top: 0;
  left: 20px;
  border-bottom-left-radius: 5px;
  border-bottom-right-radius: 5px;
}

.category-btn {
  height: 30px;
  width: 90px;
  padding-bottom: 28px;
}

.text-box {
  background-color: rgba(88, 88, 88, 0.276);
  backdrop-filter: blur(5px);
}

.mdi-heart {
  color: rgba(178, 2, 2, 0.6);
}

.mdi-heart-outline {
  color: rgba(178, 2, 2, 0.6);

  transition: all 0.2s ease-in-out;

  &:hover {
    color: rgba(178, 2, 2, 1);
  }
}

.img-fluid {
  aspect-ratio: 1/1.1;
  object-fit: cover;
  border-radius: 10px;
  padding-inline-end: 12px;
  padding-inline-start: 4px;
}

@media (min-width: 991px) and (max-width: 1201px) {
  .img-fluid {
    aspect-ratio: 1/2;
    object-fit: cover;
    padding-inline-end: 12px;
    padding-inline-start: 4px;
  }
}
</style>
