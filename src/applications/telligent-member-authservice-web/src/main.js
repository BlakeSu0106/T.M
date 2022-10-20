import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import i18n from '@/plugins/i18n'
import VueTelInputVuetify from 'vue-tel-input-vuetify/lib';
import lodash from 'lodash';
import mixin from './mixin';
Vue.mixin(mixin);
Vue.use(VueTelInputVuetify, {
  vuetify,
  placeholder: '',  
  validCharactersOnly: true,
});

Vue.config.productionTip = false

new Vue({
  vuetify,
  VueTelInputVuetify,
  router,
  store,
  i18n,
  lodash,
  render: h => h(App)
}).$mount('#app')
