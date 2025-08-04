<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';


const activeRecipe = computed(()=> AppState.activeRecipe);

</script>


<template>
  <div v-if="activeRecipe" class="container-fluid p-0">
    <div class="row p-0">
      <div class="col-lg-6 p-0">
        <img :src="activeRecipe.imgUrl" :alt="`A picture of ${ activeRecipe.creator.name }'s ${ activeRecipe.title } Recipe`" class="img-fluid">
        <button class="heart-button text-box">
          <i class="mdi mdi-heart"><span class="text-light">26</span></i>
        </button>
      </div>
      <div class="col-lg-6 mt-4 ps-5">
        <div class="row align-items-center justify-content-start d-flex">
          <h1 class="main-font text-success col-12">{{ activeRecipe.title }}</h1>
          <!-- TODO Dropdown button delete/edit recipe Modals -->
          <!-- TODO THEME COLOR CHANGES WITH THEME CHANGE -->
        </div>
        <div class="row">
          <h5>by: {{ activeRecipe.creator.name }}</h5>
        </div>
        <div class="row align-items-center">
          <button class="btn btn-secondary rounded-pill category-btn col-3 mb-3">{{ activeRecipe.category }}</button>
          <p role="button" class="mdi mdi-dots-horizontal fs-1 col-1" id="dropdownMenu" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true"></p>
          <div class="dropdown-menu text-start dropdown-size ps-2 bg-secondary" aria-labelledby="dropdownMenu">
            <button class="dropdown-item p-0 mt-1 fs-4" type="button">Edit Recipe</button>
            <hr>
            <button class="dropdown-item p-0 pb-1 fs-4 text-danger" type="button">Delete Recipe</button>
          </div>
        </div>
        <div class="row">
          <h3>Ingredients</h3>
        </div>
        <!-- TODO V-For Ingredient in Ingredients -->
        <div class="row">
          <p>4 bacon strips, chopped</p>
        </div>
        <!-- TODO Scroll Area -->
        <div class="row">
          <h3>Instructions</h3>
          <p>{{ activeRecipe.instructions }}</p>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>

.mdi-dots-horizontal {
  left: 90%;
  top: -10px;
}

.dropdown-size {
  width: 100px;
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

  .img-fluid {
    aspect-ratio: 1/1;
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