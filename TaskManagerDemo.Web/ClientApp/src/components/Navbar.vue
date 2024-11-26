<script setup lang="ts">
import {storeToRefs} from "pinia";
import {useUserStore} from "../stores/userStore.ts";
import {useRouter} from "vue-router";

const router = useRouter();
const {logout} = useUserStore();
const {isLoading, isAuthenticated, currentUser} = storeToRefs(useUserStore());

async function logoutUser() {
  await logout();

  await router.push({name: 'Login'});
}
</script>

<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
    <div class="container">
      <span class="ms-auto navbar-text d-inline-flex">
        <span v-if="isLoading" class="navbar-text">Lade User</span>
      
        <template v-else-if="isAuthenticated && currentUser">
          <span>
            Eingeloggt als: {{ currentUser.userName }}
          </span>
          <button type="button"
                  class="btn btn-link link-secondary py-0"
                  @click="logoutUser">
            (ausloggen)
          </button>
        </template>

        <span v-else class="navbar-text">Nicht eingeloggt</span>
      </span>
    </div>
  </nav>
</template>

<style scoped lang="scss">

</style>