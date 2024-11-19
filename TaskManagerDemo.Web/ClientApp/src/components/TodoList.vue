<script setup lang="ts">
import apiClient from "../api.client.ts";
import {TodoListDto} from "../api.ts";
import {onMounted, ref} from "vue";

const isLoading = ref(true);

const todos = ref<TodoListDto>(); 
onMounted(async function loadTodos() {
  const response = await apiClient.todosList();
  
  todos.value = response.data;
})
</script>

<template>
{{todos}}
</template>

<style scoped>

</style>