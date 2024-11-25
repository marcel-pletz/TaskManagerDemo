import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import mkcert from 'vite-plugin-mkcert';

export default defineConfig({
  plugins: [vue(), mkcert()],
  server: {
    proxy: {
      "/api": {
        target: 'https://localhost:7049',
        changeOrigin: false,
        secure: false,
      },
      "/swagger": {
        target: 'https://localhost:7049',
        changeOrigin: false,
        secure: false,
      },

    },
    cors: false,
  }
});
