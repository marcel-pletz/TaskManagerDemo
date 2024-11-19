import {createRouter, createWebHistory, RouteRecord} from "vue-router";
import TodoList from "./components/TodoList.vue";
import Login from "./components/Login.vue";

import { useUserStore } from "./stores/userStore.ts";

const routes: RouteRecord = [
    {
        path: '/',
        name: 'Todos',
        component: TodoList
    },
    {
        path: '/login',
        name: 'Login',
        component: Login
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach(async (to, from) => {
    const userStore = useUserStore();
    await userStore.fetchCurrentUser();
    
    if(!userStore.isAuthenticated && to.name !== 'Login') {
        return { name: 'Login' };
    }
})

export { router, routes }