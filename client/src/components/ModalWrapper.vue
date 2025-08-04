<script setup>

defineProps({
  modalId: { type: String, required: true },
  modalHeader: { type: String, required: true },
});

</script>


<template>
  <div class="modal fade text-secondary" :id="modalId" tabindex="-1" :aria-labelledby="`${modalId}Label`" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body p-0 ms-2 position-relative">
          <button type="button" class="btn-close position-absolute" data-bs-dismiss="modal" aria-label="Close"></button>
          <slot></slot>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>

.btn-close {
  right: 0;
}
</style>