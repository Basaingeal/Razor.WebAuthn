module.exports = function (api) {
  api.cache(true)

  const presets = [
    [
      '@babel/preset-env',
      {
        useBuiltIns: "usage",
        corejs: 3,
        debug: true,
        // exclude: ['es.promise']
      }
    ],
    '@babel/preset-typescript'
  ]

  const plugins = [
    '@babel/proposal-class-properties',
    '@babel/proposal-object-rest-spread'
  ]

  return { presets, plugins }
}
