<script setup lang="ts">

import {todoClient} from "../api/api.client.ts";
import {Status, TodoEntryDto} from "../api/data-contracts.ts";

const props = defineProps<{
  todo: TodoEntryDto
}>();

const emit = defineEmits<{
  (event: 'status-update', newStatus: Status): void;
}>();

async function startProgress() {
  await todoClient.startProgress(props.todo.id);
  emit('status-update', Status.InProgress);
}

async function finish() {
  await todoClient.finish(props.todo.id);
  emit('status-update', Status.Finished);
}
</script>

<template>
  <div class="card m-1">
    <div class="card-body">
      <div class="card-text">{{ todo.title }}</div>
      <button v-if="todo.status === Status.Todo"
              class="btn btn-primary"
              @click="startProgress">
        Starten
      </button>
      <button v-if="todo.status === Status.InProgress"
              class="btn btn-primary"
              @click="finish">
        Abschließen
      </button>
    </div>
  </div>
</template>

<style scoped>

</style>