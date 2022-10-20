<template>
  <v-container fluid fill-height class="d-flex justify-center">
    <router-view />
  </v-container>
</template>

<script>
import userManager from "@/auth/authService.js";
import { config } from "../config.js";
import { getThirdPartyId } from "@/apis/configApi";
export default {
  data: function() {
    return {};
  },
  methods: {},
  computed: {},
  watch: {},
  async created() {
    let vi = this;
    try {
      let user = await userManager.getUser();
      if (!user) {
          userManager.signinRedirect({
            extraQueryParams: {
              companyId: "71ff12dd-dcc9-11ec-a719-0242ac170002",
              channelId: "7bcc25c5-dd9a-11ec-a719-0242ac170002"
            }
          });
      } else {
        vi.setUser(user);
        vi.$router.push("/");
      }
    } catch (err) {
      document.location = config.auth.post_logout_redirect_uri;
    }
  }
};
</script>

