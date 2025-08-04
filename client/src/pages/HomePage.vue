<script setup >
import { AppState } from '@/AppState.js';
import ModalWrapper from '@/components/ModalWrapper.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import RecipeDetailsModal from '@/components/RecipeDetailsModal.vue';
import { AuthService } from '@/services/AuthService.js';
import { loadState, saveState } from '@/utils/Store.js';
import { computed, ref, watch } from 'vue';


const identity = computed(() => AppState.identity);
const account = computed(() => AppState.account);


function login(){
  AuthService.loginWithRedirect();
}

function logout(){
  AuthService.logout();
}

const theme = ref(loadState('theme') || 'light')

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
}

watch(theme, () => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}, { immediate: true })


</script>

<template>
  <div class="ms-auto">
    <button class="btn text-light" @click="toggleTheme"
      :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
      <i v-if="theme == 'dark'" class="mdi mdi-weather-sunny"></i>
      <i v-if="theme == 'light'" class="mdi mdi-weather-night text-dark"></i>
    </button>
  </div>
  <div class="container-fluid">
    <!-- SECTION HERO LANDING -->
    <div class="hero-bg mx-lg-3 rounded py-2 px-2 mt-3 card-shadow">
      <div class="d-flex align-items-center mt-2 justify-content-end">
        <form class="row d-flex">
          <div class="position-relative">
            <input class="form-control me-2 w-100" type="search" placeholder="Search..." aria-label="Search">
            <button class="btn search-btn" type="submit">
              <i class="mdi mdi-magnify"></i>
            </button>
          </div>
        </form>
        <!-- TODO LOGOUT/EDIT PROFILE MODAL ON PFP CLICK -->
        <i v-if="!identity && !account" @click="login()" role="button" class="ms-2 mdi mdi-login text-light me-3 fs-1"></i>
        <img v-else role="button" :src="account?.picture" :alt="`${identity?.name}'s profile picture`" class="profile-img" id="dropdownMenu" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
        <div class="dropdown-menu text-start dropdown-size ps-2 bg-secondary" aria-labelledby="dropdownMenu">
            <!-- TODO CREATE ACCOUNT INFO EDIT MODAL -->
            <button class="dropdown-item p-0 mt-1 fs-4" type="button">Edit Profile</button>
            <hr>
            <button @click="logout()" class="dropdown-item p-0 pb-1 fs-4 text-danger" type="button">Logout</button>
          </div>
      </div>
      <div class="row text-center mt-5 title-font text-light text-shadow mb-5">
        <h1 class="display-2">All-Spice</h1>
        <h2 class="h1">Cherish Your Family</h2>
        <h2 class="h1">And Their Cooking</h2>
      </div>
      <!-- SECTION FILTER BUTTONS -->
      <div class="row justify-content-center position-relative pt-5">
        <div class="col-12 col-sm-6 col-lg-5 ps-2  pt-3 pb-2 bg-light text-center card-shadow rounded position-absolute nav-card">
          <div class="row d-flex align-items-center justify-content-between text-success title-font">
            <div class="col-4">
              <!-- TODO FILTER FOR HOME (ALL RECIPES), MY RECIPES, AND FAVORITE RECIPES -->
              <h2 class="card-size">Home</h2>
            </div>
            <div class="col-4">
              <h2 class="card-size">My Recipes</h2>
            </div>
            <div class="col-4">
              <h2 class="card-size">Favorites</h2>
            </div>
          </div>
        </div>
      </div>
      <!-- !SECTION -->
    </div>
    <!-- !SECTION -->
  </div>
  <!-- SECTION RECIPE CARDS -->
  <div class="container mt-5">
    <div class="row mt-5 justify-content-center">
      <div v-for="i in 9" :key="i" class="recipe-card-container g-5">
        <RecipeCard />
      </div>
    </div>
  </div>
  <!-- !SECTION -->
  <!-- SECTION Add Recipe Button -->
  <!-- TODO CREATE RECIPE BUTTON -->
  <button class="add-recipe-btn btn btn-success rounded-circle">
    <i class="mdi mdi-plus-thick fs-2"></i>
  </button>
  <!-- !SECTION -->
  
  <!-- SECTION MODAL WRAPPERS -->
  <ModalWrapper modalId="recipeDetailsModal" modalHeader="recipe details">
    <RecipeDetailsModal />
  </ModalWrapper>
  <!-- !SECTION -->
</template>

<style scoped lang="scss">

  .hero-bg {
    background-image: url('../assets/img/hero-bg.jpg');
    background-size: cover;
    
  }

  .search-btn {
    position: absolute;
    right: 0;
    top: -10px;
  }

  .dropdown-item {

    &:hover {
      background-color: var(--bs-secondary);
    }
  }

  .nav-card {
    position: absolute;
    right: 50%;
    top: 50%;
    Transform: translateX(50%);
  }

  .mdi {
    font-size: 2rem;
  }

  .profile-img {
    height: 55px;
    border-radius: 50%;
    margin-left: 2rem;
    margin-right: 1rem;
  }

.card-shadow {
  box-shadow: 0.4rem 0.6rem 1rem rgba(0, 0, 0, .4);
}

.recipe-card {
  height: 400px;
  width: 400px;
  background-position: center;
  background-repeat: no-repeat;
  position: relative;
}

.recipe-card-container {
  max-height: 401px;
  max-width: 401px;
}

.add-recipe-btn {
  position: sticky;
  width: 60px;
  height: 60px;
  left: 88%;
  bottom: 20px;
}


@media (min-width: 424px) and (max-width: 769px) {
  .card-size {
    font-size: 1.3rem;
  }
}

</style>
