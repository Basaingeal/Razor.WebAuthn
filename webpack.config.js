const path = require("path");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");

module.exports = {
  entry: {
    webAuthn: "./src/WebAuthn.ts",
  },
  output: {
    filename: "[name].js",
    path: path.resolve(__dirname, "wwwroot")
  },
  module: {
    rules: [
      {
        test: /\.ts$/,
        use: "babel-loader",
        exclude: /node_modules/
      }
    ]
  },
  plugins: [
    new CleanWebpackPlugin()
  ],
  optimization: {
    
  }
};
