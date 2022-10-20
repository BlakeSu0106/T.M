export const config = {
  auth: {
    authority: process.env.VUE_APP_AUTHSVR_AUTHORITY,
    client_id: process.env.VUE_APP_CLIENT_ID,
    redirect_uri: '/callback',
    post_logout_redirect_uri: '/',
    response_type: 'code',
    scope: 'openid profile IdentityServerApi telligent.scope',
    silent_redirect_uri: '/SilentRenew',
    automaticSilentRenew: true,
  },
  api: process.env.VUE_APP_AUTH_API,  
};

export default {
  install(Vue) {
    Vue.prototype.$conf = config;
  },
};
