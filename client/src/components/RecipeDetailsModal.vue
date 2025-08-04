<script setup>

</script>


<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-6 p-0">
        <img src="https://plus.unsplash.com/premium_photo-1673108852141-e8c3c22a4a22?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8Zm9vZHxlbnwwfHwwfHx8MA%3D%3D" alt="" class="img-fluid">
        <button class="heart-button text-box">
          <i class="mdi mdi-heart">26</i>
        </button>
      </div>
      <div class="col-md-6">
        <div class="row align-items-center justify-content-start">
          <h1 class="main-font text-success">Lorem ipsum dolor sit amet consectetur adipisicing elit. Accusantium, tempora.</h1>
          <!-- TODO Dropdown button delete/edit recipe -->
          <i role="button" class="mdi mdi-dots-horizontal fs-1 position-absolute" id="dropdownMenu" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true"></i>
          <div class="dropdown-menu text-start dropdown-size ps-2 bg-secondary" aria-labelledby="dropdownMenu">
            <button class="dropdown-item p-0 mt-1 fs-4" type="button">Edit Recipe</button>
            <hr>
            <button class="dropdown-item p-0 pb-1 fs-4 text-danger" type="button">Delete Recipe</button>
          </div>
        </div>
        <div class="row">
          <h5>by: Millie Sheppard</h5>
        </div>
        <div class="row">
          <button class="btn btn-secondary rounded-pill category-btn">Lunch</button>
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
          <p>1. Lorem, ipsum dolor sit amet consectetur adipisicing elit. Fugit, vel eos facilis qui aut similique rem tempore voluptatum earum quasi voluptate reprehenderit magnam, tempora ipsam inventore alias omnis eius numquam quibusdam itaque aliquid amet optio! Sequi ipsam blanditiis illum ipsa.</p>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>

.mdi-dots-horizontal {
  left: 90%;
  top: -10px;
  color: black;
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

  .text-box {
    background-color: rgba(88, 88, 88, 0.276);
    backdrop-filter: blur(5px);
  }




</style>