const path = require('path');
const VueLoaderPlugin = require('vue-loader/lib/plugin');

module.exports = {
	mode: 'development',
	entry: './wwwroot/scripts/script.js', 
	output: {
		filename: 'bundle.js', 
		path: path.resolve(__dirname, 'wwwroot/dist')
	}, 
	optimization: {
		minimize: true
	}, 
	module: {
		rules: [
			{
				test: /\.vue$/, 
				loader: 'vue-loader'
			},		
			{
				test: /\.js$/, 
				exclude: /(node_modules|bower_components)/,
				use: {
					loader: 'babel-loader', 
					options: {
						presets: ["@babel/preset-env"]
					}
				}
			},			
			{
				test: /\.css$/, 
				use: [
					'vue-style-loader', 
					'css-loader'
				]
			}			
		]
	}, 
	plugins: [
		new VueLoaderPlugin()
	]
};