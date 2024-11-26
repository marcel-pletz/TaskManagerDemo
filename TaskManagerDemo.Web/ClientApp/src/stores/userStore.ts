import {defineStore} from "pinia";
import {ref} from 'vue';
import {AuthenticatedUserDto} from "../api/data-contracts.ts";
import {authenticationClient} from "../api/api.client.ts";

export const useUserStore = defineStore('userStore', () => {
    const isLoading = ref(true);
    const isAuthenticated = ref(false);
    const currentUser = ref<AuthenticatedUserDto>();
    let currentUserPromise = forceFetchCurrentUser();

    async function forceFetchCurrentUser() {
        isLoading.value = true;
        const response = await authenticationClient.getCurrentUser();
        const data = response.data;
        isAuthenticated.value = data.isAuthenticated ?? false;
        currentUser.value = data.user;
        isLoading.value = false
    }

    async function fetchCurrentUser() {
        return await currentUserPromise;
    }

    async function login(userName: string) {
        isLoading.value = true;
        await authenticationClient.login({username: userName});
        currentUserPromise = forceFetchCurrentUser();
        await currentUserPromise;
        isLoading.value = false
    }

    async function logout() {
        isLoading.value = true;
        await authenticationClient.login();
        currentUserPromise = forceFetchCurrentUser();
        await currentUserPromise;
        isLoading.value = false
    }

    return {
        fetchCurrentUser,
        forceFetchCurrentUser,
        login,
        logout,
        isAuthenticated,
        isLoading,
        currentUser
    }
});