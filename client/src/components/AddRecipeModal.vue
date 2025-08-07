<script setup>
import { recipesService } from '@/services/RecipesService.js';
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';

const editableRecipeData = ref({
  title: '',
  instructions: '',
  imgUrl: '',
  category: '',
});



async function addRecipe() {
  try {
    const recipeId = await recipesService.AddRecipe(editableRecipeData);

    editableRecipeData.value = {
      title: '',
      instructions: '',
      imgUrl: '',
      category: ''
    }
  }
  catch (error){
    Pop.error(error);
  }
}

</script>


<template>
<h1>Hello World</h1>
</template>


<style lang="scss" scoped>

</style>