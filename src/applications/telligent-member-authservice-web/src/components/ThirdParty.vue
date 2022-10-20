<template>
  <div class="v-container">
    <div class="d-flex justify-center pb-2">透過其他平台登入帳號</div>
    <div class="d-flex justify-center">
      <v-btn class="ma-2" icon sm>
        <v-avatar size="48" @click="googleLogin">
          <img src="@/assets/images/icons/third-party-google.png" alt="Google" />
        </v-avatar>
      </v-btn>
      <v-btn class="ma-2" icon sm>
        <v-avatar size="42" @click="fbLogin">
          <v-icon large color="blue darken-2">mdi-facebook</v-icon>
        </v-avatar>
      </v-btn>
      <v-btn class="ma-2" icon sm>
        <v-avatar size="36" @click="linelogin">
          <img src="@/assets/images/icons/third-party-line.png" alt="Line" />
        </v-avatar>
      </v-btn>
    </div>
  </div>
</template>
<script>
export default {
  methods: {
    async linelogin() {
      const url = new URL(window.location.href);
      const urlparam = encodeURIComponent(url.search);
      window.location.href = `https://access.line.me/oauth2/v2.1/authorize?response_type=code&client_id=${this.$store.state.thirdPartyChannelInfo.thirdPartyChannelId}&redirect_uri=${window.location
        .origin +
        window.location.pathname.replace(
          "login",
          "thirdlogin"
        )}&state=${urlparam}&scope=profile%20openid%20email`;
        console.log("url",`redirect_uri=${window.location
        .origin +
        window.location.pathname.replace(
          "login",
          "thirdlogin"
        )}&state=${urlparam}&scope=profile%20openid%20email`)
    },

    async googleLogin() {
    },

    async fbLogin() {
    }
  },
  components: {}
};
</script>

<style>
</style>