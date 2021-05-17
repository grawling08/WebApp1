import App from '../vue/components/App.js';

const app = Vue.createApp({
    render: () => Vue.h(App),
});
app.mount('#app');