<script setup lang="ts">

import TodoForm from "./TodoForm.vue";
import {TodoCommandDto, TodoDto} from "../api/data-contracts.ts";
import {todoClient} from "../api/api.client.ts";
import {useRouter} from "vue-router";
import {onMounted, ref} from "vue";

const props = defineProps<{
  id: string;
}>();

const router = useRouter();

const todo = ref<TodoDto>();

onMounted(async function loadTodo() {
  const response = await todoClient.getTodo(props.id);
  todo.value = response.data;
});

async function updateTodo(todo: TodoCommandDto): Promise<void> {
  await todoClient.updateTodo(props.id, todo);
  await router.push({name: 'Todos'});
}
</script>

<template>
  <TodoForm v-if="todo" :todo="todo" :submit-fn="updateTodo"/>
</template>

<style scoped>

</style>