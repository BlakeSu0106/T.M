<template>
  <v-card-text>
    <p class="text-h5 text--primary text-center">
      <strong>{{$t('validation.tiitle')}}</strong>
    </p>
    <p>{{$t('validation.SMS')}}{{phoneNumber.formattedNumber}}</p>
    <v-form ref="form" v-model="validate" lazy-validation>
      <v-text-field
        v-model="captcha.value"
        :counter="6"
        placeholder="請輸入驗證碼"
        outlined
        @keyup="validateCaptcha()"
        :error-messages="errorMessage"
      ></v-text-field>
    </v-form>
    <div class="d-flex justify-center">
      <v-btn
        class="white--text primary ma-1"
        color="rgb(255, 90, 110)"
        @click="sendSMS"
        :disabled="sendStatus"
      >{{text}}</v-btn>
      <v-btn sm class="success ma-1">回首頁</v-btn>
    </div>
  </v-card-text>
</template>

<script>
import { postCaptchaAsync, postCaptchaValidateAsync } from "@/apis/captchaApi";
import { postLoginAsync } from "@/apis/authApi";
import VerificationCodeVue from "./VerificationCode.vue";
export default {
  data() {
    return {
      validate: true,
      result: true,
      sendStatus: false,
      time: 60,
      captcha: {
        key: "",
        value: ""
      },
      step: null,
      errorMessage: "",
      userInfo: {}
    };
  },
  props: {
    phoneNumber: Object
  },
  created() {
    this.sendSMS();
  },
  computed: {
    text: function() {
      if (this.time == 60 || this.time == 0) {
        this.sendStatus = false;
        return "寄送簡訊";
      } else if (this.time < 60 && this.time !== 0) {
        return this.time + "秒後，可重新寄送簡訊";
      }
    }
  },
  methods: {
    async sendSMS() {
      let i = 0;
      this.sendStatus = true;
      this.timer();
      try {
        let resp = await postCaptchaAsync({
          key: this.phoneNumber.captchaKey
        });
      } catch (error) {
        connsole.log(error);
      }
    },
    timer() {
      if (this.time > 0) {
        this.time--;
        setTimeout(this.timer, 1000);
      } else if (this.time == 0) {
        this.respStatus = Boolean;
      }
    },
    // debounce(func, delay = 8000) {
    //   let timer = null;
    //   return () => {
    //     let context = this;
    //     let args = arguments;
    //     clearTimeout(timer);
    //     timer = setTimeout(() => {
    //       func.apply(context, args);
    //     }, delay);
    //   };
    // },
    async validateCaptcha() {
      if (this.captcha.value.length == 6) {
        try {
          let resp = await postCaptchaValidateAsync({
            key: this.phoneNumber.captchaKey,
            value: this.captcha.value
          });
          if (resp.data == false && resp.status == 200) {
            this.errorMessage = this.$t("message.SMS.error");
            this.$refs.form.reset();
          } else if (resp.data == true) {
            if (this.$store.state.status.SMSType == 0) {
              await postLoginAsync(this.userInfo);
            } else if (this.$store.state.status.SMSType == 1) {
              this.$store.commit("setStep", 2);
            } else if (this.$store.state.status.SMSType == 2) {
              this.$store.commit("setStep", 4);
            } else if (this.$store.state.status.SMSType == 3) {
              this.$store.commit("setStep", 4);
            }
          }
        } catch (error) {
          console.log("error", error);
        }
      }
    }
  },
  components: {
    ThirdParty: () => import("@/components/ThirdParty")
  }
};
</script>