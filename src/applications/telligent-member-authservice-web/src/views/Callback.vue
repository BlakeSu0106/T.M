<template></template>

<script>
import { UserManager } from "oidc-client";
export default {
  mounted() {
    let vi = this;
    new UserManager({ response_mode: "query" })
      .signinRedirectCallback()
      .then(function(user) {        
        vi.$router.push("/");
      })
      .catch(function(e) {        
        sessionStorage.clear();
        localStorage.clear();
         vi.$router.push("/login");
      });
  }
};
</script>