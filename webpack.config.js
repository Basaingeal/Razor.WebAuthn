const path = require('path')
const { CleanWebpackPlugin } = require('clean-webpack-plugin')
const TerserJSPlugin = require('terser-webpack-plugin')

module.exports = {
  entry: {
    webAuthn: './src/WebAuthn.ts',
    'webAuthn.min': './src/WebAuthn.ts'
  },
  output: {
    filename: '[name].js',
    path: path.resolve(__dirname, 'wwwroot')
  },
  module: {
    rules: [
      {
        test: /\.ts$/,
        use: 'babel-loader',
        exclude: /node_modules/
      }
    ]
  },
  plugins: [new CleanWebpackPlugin()],
  optimization: {
    minimizer: [
      new TerserJSPlugin({
        include: /\.min\.js$/
      })
    ]
  }
}
