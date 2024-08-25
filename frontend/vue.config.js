const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
devServer: {
  proxy: {
    '/api': {
      target: 'https://localhost:7061',
      changeOrigin: true, // This helps in handling cross-origin requests
      secure: false, // Set to false if you're working with self-signed certificates
      logLevel: 'debug' // Optional, helps in debugging proxy issues
    }}
}});
