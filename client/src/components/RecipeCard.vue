<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { Modal } from 'bootstrap';

const prop = defineProps({
  recipe: { type: Recipe, required: true }
})

function openDetailsPage(){
  AppState.activeRecipe = prop.recipe;
  Modal.getOrCreateInstance('#recipeDetailsModal').show();
}
</script>

<template>
<div v-if="recipe" class="shadow rounded position-relative card-wrapper">
  <!-- TODO ROUTERLINK TO RECIPE DETAILS PAGE -->
  <img @click="openDetailsPage()" :src="recipe.imgUrl" :alt="`picture of ${recipe.creator.name}'s ${recipe.title} recipe`" class="img-fluid recipe-img rounded card-shadow">
  <!-- TODO BUTTON V-IF FAVORITED? && Account? @CLICK -->
  <button class="heart-btn text-box btn">
    <i class="mdi mdi-heart fs-3 text-shadow-light"></i>
  </button>
  <!-- <button class="heart-btn text-box btn">
    <i class="mdi mdi-heart-outline fs-3 text-shadow-light"></i>
  </button> -->
  <!-- TODO BUTTON TO FILTER TO SIMILAR -->
  <button class="btn rounded-pill category-btn text-box text-shadow">{{ recipe?.category }}</button>
  <div class="col-11 text-box title-btn"><h2 class="fs-5 pt-2">{{ recipe.title }}</h2>
  </div>
</div>
</template>


<style lang="scss" scoped>
  
  .recipe-img {
    width: 100%;
    height: 32dvh;
    object-fit: cover;
  }

  .card-shadow {
  box-shadow: 0.4rem 0.6rem 1rem rgba(0, 0, 0, .4);
  }

  .category-btn {
    position: absolute;
    left: 15px;
    top: 15px;
    color: #fff;
    height: 30px;
    padding-top: 0px;
    padding-bottom: 0px;
  }

  .heart-btn {
    position: absolute;
    right: 10px;
    top: 0;
    color: rgba(178, 2, 2, 0.6);
    padding-inline: 3px;
    padding-top: 0px;
    padding-bottom: 0px;
    border-top-right-radius: 0px;
    border-top-left-radius: 0px;
    box-shadow: none;
  }

  .text-box {
    background-color: rgba(88, 88, 88, 0.507);
    backdrop-filter: blur(5px);
  }

  .title-btn {
    position: absolute;
    left: 15px;
    bottom: 5px;
    color: whitesmoke;
    border-radius: 10px;
    padding-left: 1rem;
  }

  .text-shadow-light {
    text-shadow: 2px, 2px, 3px rgba(207, 221, 10, 0.5);
  }
</style>