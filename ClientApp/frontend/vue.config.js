const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
  transpileDependencies: true,
  outputDir: "../../wwwroot/",
  devServer: {
    proxy: {
      "^/": {
        target: "http://localhost:5277",
        ws: false
      }
    }
  }
});
