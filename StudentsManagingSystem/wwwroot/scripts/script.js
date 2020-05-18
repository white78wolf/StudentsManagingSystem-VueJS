import Vue from '../vendor/vue';
import App from './App.vue';

Vue.config.devtools = true;

var app = new Vue({
    el: "#app",
    render: h => h(App)
});