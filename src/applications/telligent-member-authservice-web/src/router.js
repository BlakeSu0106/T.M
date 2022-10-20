import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from './views/Home.vue' 
import Login from './views/Login.vue'
import store from '@/store'
import Callback from './views/Callback.vue';
// import SilentRenew from './views/SilentRenew.vue';
Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'index',
    component: Home,
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
  },
  {
    path: '/callback',
    name: 'callback',
    component: Callback,
  },
  // {
  //   path: '/silentrenew',
  //   name: 'renew',
  //   component: SilentRenew,
  // },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
