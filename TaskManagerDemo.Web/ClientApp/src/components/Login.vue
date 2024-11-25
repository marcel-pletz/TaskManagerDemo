<script setup lang="ts">
import {onMounted, ref} from "vue";
import {UserDto} from "../api.ts";
import apiClient from "../api.client.ts";
import {useUserStore} from "../stores/userStore.ts";
import {useRouter} from "vue-router";

const { login } = useUserStore();
  
const availableUsers = ref<UserDto[]>();

const router = useRouter();
  
onMounted(async function loadAllAvailableUsers() {
  const response = await apiClient.usersList();
  availableUsers.value = response.data;
})

async function loginUser(user: UserDto) {
  if(user.userName) {
    await login(user.userName);

    await router.push({name: 'Todos'});  
  }
}
</script>

<template>
  <h2>Einloggen als:</h2>
  <div class="list-group">
      <button v-for="user in availableUsers" 
              @click="loginUser(user)"  
              class="list-group-item list-group-item-action">
        {{user.userName}}
      </button>
  </div>
</template>

<style scoped>

</style>