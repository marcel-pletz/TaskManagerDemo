<script setup lang="ts">

import TodoForm from "./TodoForm.vue";
import {TodoCommandDto} from "../api/data-contracts.ts";
import {todoClient} from "../api/api.client.ts";
import {useRouter} from "vue-router";

const router = useRouter();

async function createTodo(todo: TodoCommandDto): Promise<void> {
  await todoClient.createTodo(todo);
  await router.push({name: 'Todos'});
}
</script>

<template>
  <TodoForm :submit-fn="createTodo"/>
</template>

<style scoped>

</style>