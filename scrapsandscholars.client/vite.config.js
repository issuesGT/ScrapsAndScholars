import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from 'path'
import fs from 'fs'
import os from 'os'

const isWindows = os.platform() === 'win32'

const baseFolder = isWindows
  ? path.join(process.env.APPDATA || '', 'ASP.NET', 'https')
  : path.join(process.env.HOME || '', '.aspnet', 'https')

const certName = 'scrapsandscholars.client'

const keyPath = path.join(baseFolder, `${certName}.key`)
const certPath = path.join(baseFolder, `${certName}.pem`)

let https = false

if (fs.existsSync(keyPath) && fs.existsSync(certPath)) {
  https = {
    key: fs.readFileSync(keyPath),
    cert: fs.readFileSync(certPath)
  }
}

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: { '@': path.resolve(__dirname, './src') }
  },
  server: {
    port: 5173,
    https,
    proxy: {
      '^/weatherforecast': {
        target: 'https://localhost:7174',
        secure: false
      },
      '^/api': {
        target: 'https://localhost:7174',
        secure: false,
        changeOrigin: true
      }
    }
  }
})
