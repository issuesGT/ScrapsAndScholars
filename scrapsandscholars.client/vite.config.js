import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from 'path'
import fs from 'fs'
import os from 'os'

const isWindows = os.platform() === 'win32'

const baseFolder = isWindows
  ? path.join(process.env.APPDATA, 'ASP.NET', 'https')
  : path.join(process.env.HOME, '.aspnet', 'https')

const certName = 'scrapsandscholars.client'

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: { '@': path.resolve(__dirname, './src') }
  },
  server: {
    port: 5173,
    https: {
      key: fs.readFileSync(path.join(baseFolder, `${certName}.key`)),
      cert: fs.readFileSync(path.join(baseFolder, `${certName}.pem`)),
    },
    proxy: {
      '^/weatherforecast': {
        target: 'https://localhost:7174',
        secure: false
      },
      '^/api': {
        target: 'https://localhost:7174', // <-- your backend port
        secure: false,
        changeOrigin: true
      }
    }
  }
})
