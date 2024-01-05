const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
  transpileDependencies: true,
  outputDir: "../../wwwroot/",
  devServer: {
    proxy: "http://localhost:5277"
  }
});
