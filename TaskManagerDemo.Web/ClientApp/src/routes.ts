import {createRouter, createWebHistory, RouteRecordRaw} from "vue-router";
import TodoList from "./components/TodoList.vue";
import Login from "./components/Login.vue";

import {useUserStore} from "./stores/userStore.ts";
import TodoCreate from "./components/TodoCreate.vue";
import TodoEdit from "./components/TodoEdit.vue";

const routes: RouteRecordRaw[] = [
    {
        path: '/',
        name: 'Todos',
        component: TodoList
    },
    {
        path: '/todos/new',
        name: 'TodoCreate',
        component: TodoCreate,
    },
    {
        path: '/todos/:id',
        name: 'TodoEdit',
        component: TodoEdit,
        props: true
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

router.beforeEach(async (to) => {
    const userStore = useUserStore();
    await userStore.fetchCurrentUser();

    if (!userStore.isAuthenticated && to.name !== 'Login') {
        return {name: 'Login'};
    }
})

export {router, routes}