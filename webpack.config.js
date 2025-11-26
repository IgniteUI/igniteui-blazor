const webpack = require('webpack');
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const nodeEnv = process.env.NODE_ENV || 'development';
const isProd = nodeEnv === 'production';
const TsconfigPathsPlugin = require("tsconfig-paths-webpack-plugin");
const CopyWebpackPlugin = require('copy-webpack-plugin');
const glob = require('glob');

const bootFileContent = `export async function beforeStart(options) {
  if (window.__igLoaded) {
    return new Promise((resolve, reject) => {
        resolve();
    });
  }

  window.__igLoaded = true;
  window.__igLibraryLoad = true;
  var timestamp = new Date().getTime().toString();
  var currScriptSrc = import.meta.url;
  var entryScript = document.createElement("script");
  entryScript.async = false;
  
  if (window.__igSkipCacheBust) {
    entryScript.src = currScriptSrc.replace(/IgniteUI\\.Blazor(?:\\.[^\\.]+)*?\\.lib\\.module\\.js/, "app.bootstrap.js");
  } else {
    entryScript.src = currScriptSrc.replace(/IgniteUI\\.Blazor(?:\\.[^\\.]+)*?\\.lib\\.module\\.js/, "app.bootstrap.js?bustv2=" + timestamp);
  }

  document.body.append(entryScript);

  return new Promise((resolve, reject) => {
      function checkEntryLoaded() {
          if (window.__igEntryBundle) {
              window.__igEntryBundle.onload = () => {
                  delete window.__igEntryBundle;
                  resolve();
              };
              if (window.__igEntryBundle.readyState == 'complete' || window.__igEntryBundle.readyState == 'loaded') {
                  delete window.__igEntryBundle;
                  resolve();
              }
          }
      }

      entryScript.onload = () => {
          console.log("script loaded");
          checkEntryLoaded();
      };
      if (entryScript.readyState == 'complete' || entryScript.readyState == 'loaded') {
          console.log("script already loaded");
          checkEntryLoaded();
      }
  });
}
export async function afterStarted(blazor) {
  
}
`;

const plugins = [
  new webpack.DefinePlugin({
    'process.env': {
      NODE_ENV: JSON.stringify(nodeEnv)
    }
  }),
  new webpack.LoaderOptionsPlugin({
    options: {
      tslint: {
        emitErrors: true,
        failOnHint: true
      }
    }
  }),
  new HtmlWebpackPlugin({
      templateContent: function (params) {
        return `var currScript = document.currentScript;
var entryScript = document.createElement("script");
entryScript.async = false;
if (currScript.src.indexOf("?bustv2=") >= 0 || window.__igLibraryLoad) {
  window.__igEntryBundle = entryScript;
  if (window.__igSkipCacheBust) {
    entryScript.src = currScript.src.replace(/app\.bootstrap\.js/, "${params.htmlWebpackPlugin.files.js[0]}");
  } else {
    entryScript.src = currScript.src.replace(/app\\.bootstrap\\.js\\?bustv2=.*/, "${params.htmlWebpackPlugin.files.js[0]}");
  }
  currScript.after(entryScript);
} else {
  if (window.__igSkipCacheBust) {
    entryScript.src = currScript.src.replace(/app\.bootstrap\.js/, "${params.htmlWebpackPlugin.files.js[0]}");
  } else {
    entryScript.src = currScript.src.replace(/app\\.bootstrap\\.js\\?bust=.*/, "${params.htmlWebpackPlugin.files.js[0]}");
  }
  document.write(entryScript.outerHTML);
}
`;
      },
      filename: path.join(__dirname, './wwwroot/app.bootstrap.js'),
      inject: false,
  }),
  new HtmlWebpackPlugin({
    templateContent: function (params) {
      return `if (window.__igLoaded) {
  } else {
        window.__igLoaded = true;
        var timestamp = new Date().getTime().toString();
        var currScript = document.currentScript;
        var entryScript = document.createElement("script");
        entryScript.async = false;
        if (window.__igSkipCacheBust) {
            entryScript.src = currScript.src.replace("app.bundle.js", "app.bootstrap.js");
        } else {
            entryScript.src = currScript.src.replace("app.bundle.js", "app.bootstrap.js?bust=" + timestamp);
        }
        document.write(entryScript.outerHTML);
  }
`;
    },
    filename: path.join(__dirname, './wwwroot/app.bundle.js'),
    inject: false,
}),
new HtmlWebpackPlugin({
  templateContent: function (params) {
    return bootFileContent;
  },
  filename: path.join(__dirname, './wwwroot/IgniteUI.Blazor.lib.module.js'),
  inject: false,
}),
new HtmlWebpackPlugin({
  templateContent: function (params) {
    return bootFileContent.replace(/IgniteUI\.Blazor\.lib\.module\.js/gm, "IgniteUI.BlazorDebug.lib.module.js");
  },
  filename: path.join(__dirname, './wwwroot/IgniteUI.BlazorDebug.lib.module.js'),
  inject: false,
}),
new HtmlWebpackPlugin({
  templateContent: function (params) {
    return bootFileContent.replace(/IgniteUI\.Blazor\.lib\.module\.js/gm, "IgniteUI.Blazor.Trial.lib.module.js");
  },
  filename: path.join(__dirname, './wwwroot/IgniteUI.Blazor.Trial.lib.module.js'),
  inject: false,
}),
new HtmlWebpackPlugin({
  templateContent: function (params) {
    return bootFileContent.replace(/IgniteUI\.Blazor\.lib\.module\.js/gm, "IgniteUI.Blazor.Lite.lib.module.js");
  },
  filename: path.join(__dirname, './wwwroot/IgniteUI.Blazor.Lite.lib.module.js'),
  inject: false,
})
];

var config = {
  devtool: isProd ? 'hidden-source-map' : 'source-map',
  context: path.resolve('./src'),
  entry: {
     app: ['./index.ts', ...glob.sync('./index.*.part.ts', { cwd: path.resolve('./src')})]
  },
  output: {
    path: path.resolve('./wwwroot'),
    filename: '[name].[contenthash].bundle.js',
    globalObject: 'this',
    library: "InfragisticsBlazor"
  },
    module: {
        rules: [
            {
                oneOf: [
                    {
                        enforce: 'pre',
                        test: /\.worker\.ts$/,
                        exclude: [/\/node_modules\//],
                        use: [{
                            loader: 'worker-loader',
                            options: { filename: 'heatWorker.js' }
                        },
                            'ts-loader', 'source-map-loader']
                    },
                    {
                        enforce: 'pre',
                        test: /\.tsx?$/,
                        exclude: [/\/node_modules\//],
                        use: ['ts-loader', 'source-map-loader']
                    }]
            },
            { test: /\.html$/, loader: 'html-loader' },
            { test: /\.css$/, use: ['style-loader', 'css-loader'] }
        ].filter(Boolean)
  },
  resolve: {
    extensions: ['.ts', '.js'],
    plugins: [new TsconfigPathsPlugin({ extensions: [".ts", ".tsx", ".js"], configFile: "./tsconfig.json" })]
  },
  plugins: plugins,
  devServer: {
    contentBase: path.join(__dirname, 'dist/'),
    compress: true,
    port: 3000,
    hot: true
  },
  // optimization: {
  //   splitChunks: {
  //     chunks: 'all'
  //   }
  // }
  optimization: {
    innerGraph: false,
    splitChunks: {
      cacheGroups: {
        igniteuiCharts: {
          test: /(igniteui-charts)/,
          name: 'igniteui-webcomponents-charts',
          chunks: 'async',
        },
        igniteuiCore: {
          test: /(igniteui-core)/,
          name: 'igniteui-webcomponents-core',
          chunks: 'async',
        },
        igniteuiGauges: {
          test: /(igniteui-gauges)/,
          name: 'igniteui-webcomponents-gauges',
          chunks: 'async',
        },
        igniteuiGrids: {
          test: /(igniteui-grids)/,
          name: 'igniteui-webcomponents-grids',
          chunks: 'async',
        },
        igniteuiDataGrids: {
          test: /(igniteui-data-grids)/,
          name: 'igniteui-webcomponents-data-grids',
          chunks: 'async',
        },
        igniteuiMaps: {
          test: /(igniteui-maps)/,
          name: 'igniteui-webcomponents-maps',
          chunks: 'async',
        },
        igniteuiExcel: {
          test: /(igniteui-excel)/,
          name: 'igniteui-webcomponents-excel',
          chunks: 'async',
        },
        igniteuiSpreadsheet: {
          test: /(igniteui-spreadsheet)/,
          name: 'igniteui-webcomponents-spreadsheet',
          chunks: 'async',
        },
        igniteuiSpreadsheetChartAdapter: {
          test: /(igniteui-spreadsheet-chart-adapter)/,
          name: 'igniteui-webcomponents-spreadsheet-chart-adapter',
          chunks: 'async',
        },
        igniteuiDataSources: {
          test: /(igniteui-datasources)/,
          name: 'igniteui-webcomponents-datasources',
          chunks: 'async',
        },
        igniteuiInputs: {
          test: /(igniteui-inputs)/,
          name: 'igniteui-webcomponents-inputs',
          chunks: 'async',
        },
        igniteuiLayouts: {
          test: /(igniteui-layouts)/,
          name: 'igniteui-webcomponents-layouts',
          chunks: 'async',
        },
        igniteuiDashboards: {
          test: /(igniteui-dashboards)/,
          name: 'igniteui-webcomponents-dashboards',
          chunks: 'async',
        },
        igniteuiDockmanager: {
          test: /(igniteui-dockmanager)/,
          name: 'igniteui-dockmanager',
          chunks: 'async',
        },
        igniteuiAngularElements: {
          test: /(igniteui-angular-elements)/,
          name: 'igniteui-angular-elements',
          chunks: 'async',
        },
        igniteuiWebComponents: {
          test: /(igniteui-webcomponents)/,
          name: 'igniteui-webcomponents',
          chunks: 'async',
        },
        igniteuiWebInputs: {
          test: /(igniteui-webinputs)/,
          name: 'igniteui-webinputs',
          chunks: 'async',
        },
        igniteuiWebGrids: {
          test: /(igniteui-webgrids)/,
          name: 'igniteui-webgrids',
          chunks: 'async',
        }
      }
    }
  }
};

module.exports = config;
