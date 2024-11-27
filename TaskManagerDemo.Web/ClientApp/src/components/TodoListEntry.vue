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
    <div class="card-body d-flex flex-column">
      <div class="card-text">{{ todo.title }}</div>
      <div class="card-text ms-auto fst-italic" v-if=todo.dueDate>Zu Erledigen bis: {{ todo.dueDate }}</div>
    </div>
    <div v-if="todo.status != Status.Finished"
         class="card-footer d-flex justify-content-end">
      <router-link :to="{name: 'TodoEdit', params: {id: todo.id} }"
                   class="btn btn-secondary">
        Bearbeiten
      </router-link>

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
.btn {
  margin-right: 0.25rem;
}
</style>