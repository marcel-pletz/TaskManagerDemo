import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"
import {createApp} from 'vue';
import {createPinia} from "pinia";
import App from './App.vue';

import {router} from "./routes.ts";

createApp(App)
    .use(createPinia())
    .use(router)
    .mount('#app');
