<script setup lang="ts">
import { onMounted } from "vue";
import { useUserStore } from "./stores/userStore.ts";
import Navbar from "./components/Navbar.vue";
const { fetchCurrentUser } = useUserStore();

onMounted(fetchCurrentUser);
</script>

<template>
  <div>
    <Navbar />
    <div class="container">
      <router-view />
    </div>
  </div>
</template>

<style lang="scss" >
.t {
  background-color: pink;
}
</style>
