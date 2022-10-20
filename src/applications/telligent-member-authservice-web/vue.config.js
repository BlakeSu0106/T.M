const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  pages: {
    index: {
      entry: "src/main.js",
      title: "會員中心"
    }
  },
  transpileDependencies: [
    'vuetify',
    'vue-tel-input-vuetify'
  ]
})
