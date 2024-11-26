<script setup lang="ts">
import {onMounted, ref} from "vue";

import {useUserStore} from "../stores/userStore.ts";
import {useRouter} from "vue-router";
import {UserDto} from "../api/data-contracts.ts";
import {userClient} from "../api/api.client.ts";

const {login} = useUserStore();

const availableUsers = ref<UserDto[]>();

const router = useRouter();

onMounted(async function loadAllAvailableUsers() {
  const response = await userClient.getAllUsers();
  availableUsers.value = response.data;
})

async function loginUser(user: UserDto) {
  if (user.userName) {
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
      {{ user.userName }}
    </button>
  </div>
</template>

<style scoped>

</style>