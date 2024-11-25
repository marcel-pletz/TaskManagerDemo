<script setup lang="ts">
import apiClient from "../api.client.ts";
import { Status, TodoEntryDto, TodoListDto } from "../api.ts";
import { computed, onMounted, ref } from "vue";
import TodoListEntry from "./TodoListEntry.vue";

const isLoading = ref(true);

const todos = ref<TodoListDto>(); 
onMounted(async function loadTodos() {
  const response = await apiClient.todosList();
  todos.value = response.data;
  isLoading.value = false;
});

const groupedTodos = computed(() => {
  const entries = todos.value?.entries ?? [];
  const entriesByStatus = (status: Status) => entries.filter(x => x.status === status)
  const allStatus = Object.values(Status);
  return new Map(allStatus.map(status => [status, entriesByStatus(status)]));
});

const statusColumns: StatusColumns = {
  Todo: { title: "Zu Erledigen" },
  InProgress: { title: "In Arbeit" },
  Finished: { title: "Erledigt" },
}

type StatusColumns = {
  [key in keyof typeof Status]: {
    title: string,
  }
}

function updateStatus(entry: TodoEntryDto, newStatus: Status) {
  entry.status = newStatus;
}

</script>

<template>
  <h2>Todos:</h2>
  <span v-if="isLoading">Lade Todos...</span>
  <template v-else>
    <div class="row">
      <div v-for="group in groupedTodos.keys()"
           class="col">
        {{statusColumns[group].title}}
      </div>
    </div>
    <div class="row">
      <div v-for="statusGroup in groupedTodos.keys()" class="col" >
        <TodoListEntry v-for="todo in groupedTodos.get(statusGroup)"
                       :todo="todo"
                       @status-update="(newStatus) => updateStatus(todo, newStatus)" />
      </div>
    </div>
  </template>
  
</template>

<style scoped>

</style>