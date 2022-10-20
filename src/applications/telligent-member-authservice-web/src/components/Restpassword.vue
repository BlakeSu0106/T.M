<template>
  <div>
    <v-card-text>
      <p class="text-h5 text--primary text-center">
        <strong>{{$t('password.reset')}}</strong>
      </p>
      <p class="text-center">
        <strong>{{$store.state.status.step==2?$t('register.passwordContent'):4?$t('password.login'):$t('register.reset')}}</strong>
      </p>
      <v-form ref="form" lazy-validation>
        <v-row class="ma-0 pa-0">
          <v-text-field
            v-model="mobile"
            class="md-9"
            label="手機號碼"
            value
            color="rgb(255, 90, 110)"
            dense
            outlined
            @keyup="mobileChecked(mobile)"
            :error-messages="text"
            :disabled="validate||sendStatus"
          ></v-text-field>
          <v-btn
            class="md-3"
            color="success"
            :disabled="!mobileStatus||sendStatus||validate"
            @click="sendSMS(mobile)"
          >發送驗證碼</v-btn>
        </v-row>
        <v-text-field
          class="pb-3"
          v-model="captcha.value"
          label="驗證碼"
          value
          color="rgb(255, 90, 110)"
          dense
          outlined
          hide-details
          :counter="6"
          @keyup="validateCaptcha()"
          :error-messages="captchaMessage"
          :disabled="validate||respStatus!=='SUCCESS'"
        ></v-text-field>
        <v-text-field
          v-model="userId"
          v-if="validate"
          class="pb-3"
          color="rgb(255, 90, 110)"
          label="使用者帳號"
          placeholder="使用者帳號"
          hide-details
          outlined
          dense
        ></v-text-field>
        <v-text-field
          v-if="validate"
          class="pt-3"
          v-model="password"
          label="密碼"
          placeholder="請輸入密碼"
          :append-icon="passwordIconValue ? 'mdi-eye' : 'mdi-eye-off'"
          :type="passwordIconValue ? 'text' : 'password'"
          @click:append="() => (passwordIconValue = !passwordIconValue)"
          @input="passwordProgressStatus=false"
          autocomplete="off"
          color="rgb(255, 90, 110)"
          :rules="[rules.required(password)]"
          hide-details
          outlined
          dense
          :loading="!passwordProgressStatus"
        >
          <template v-slot:progress>
            <v-progress-linear
              buffer-value="100"
              class="pt-1"
              style="font-size: 0.75em;"
              background-color="#e9ecef"
              :value="passwordProgress"
              :color="passwordProgressColor"
              absolute
              rounded
              height="18"
            >{{passwordProgressMessage}}</v-progress-linear>
          </template>
        </v-text-field>
        <v-text-field
          v-if="validate"
          class="mt-3 pt-3"
          v-model="confirmPassword"
          label="再次確認密碼"
          :append-icon="confirmPasswordIconValue ? 'mdi-eye' : 'mdi-eye-off'"
          @click:append="() => (confirmPasswordIconValue = !confirmPasswordIconValue)"
          @input="confirmPasswordProgressStatus=false"
          placeholder="請再次輸入密碼"
          :type="confirmPasswordIconValue ? 'text' : 'password'"
          autocomplete="off"
          :rules="[rules.required(confirmPassword),rules.validateSame(confirmPassword)]"
          color="rgb(255, 90, 110)"
          outlined
          dense
          :loading="!confirmPasswordProgressStatus"
        >
          <template v-slot:progress>
            <v-progress-linear
              buffer-value="100"
              class="pt-1"
              style="font-size: 0.75em;"
              background-color="#e9ecef"
              :value="confirmPasswordProgress"
              :color="confirmPasswordProgressColor"
              absolute
              rounded
              height="18"
            >{{confirmPasswordProgressMessage}}</v-progress-linear>
          </template>
        </v-text-field>
      </v-form>
      <div class="d-flex justify-center">
        <v-btn
          class="white--text primary ma-2"
          color="rgb(255, 90, 110)"
          @click="validatePassword"
        >{{$t('password.reset')}}</v-btn>
      </div>
    </v-card-text>
  </div>
</template>

<script>
import rules from "@/plugins/rules";
import { postLoginAsync, postRestPasswordAsync } from "@/apis/authApi";
import { postCaptchaAsync, postCaptchaValidateAsync } from "@/apis/captchaApi";
export default {
  data() {
    return {
      mobile: "",
      userId: "",
      captcha: "",
      highReg: /^(?=.*[^a-zA-Z0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$/,
      middleReg: /.*[A-Z.a-z]+.*[0-9]+|.*[0-9]+.*[A-Z.a-z]+.*/,
      lowReg: /[A-Za-z0-9]{6}.*/,
      TWRegular: /^09[0-9]{8}$/,
      SGPRegular: /^[89][0-9]{7}$/,
      MORegular: /^6[0-9]{7}$/,
      //再次輸入密碼
      confirmPassword: "",
      confirmPasswordProgressMessage: "",
      confirmPasswordProgressColor: "",
      confirmPasswordIconValue: false,
      confirmPasswordProgressStatus: true,
      mobileStatus: false,
      rules: rules,
      //密碼
      password: "",
      passwordIconValue: false,
      passwordProgressMessage: "",
      passwordProgressColor: "",
      passwordProgressStatus: true,
      validate: false,
      sendStatus: false,
      respStatus: "",
      time: 10,
      captcha: {
        key: "",
        value: ""
      },
      step: null,
      captchaMessage: "",
      userInfo: {},
      identifyCode: "",
      verificationCode: "",
      rulesUserName: [v => !!v || "請輸入使用者信箱"],
      rulesPassword: [v => !!v || "請輸入密碼"],
      rulesVerificationCode: [
        v => !!v || "請輸入驗證碼",
        v => (v && v === this.identifyCode) || "驗證碼不相符"
      ]
    };
  },
  mounted() {},
  computed: {
    passwordProgress() {
      if (this.password.length < 6) {
        this.passwordProgressMessage = "";
      }
      if (this.highReg.test(this.password)) {
        this.passwordProgressColor = "success";
        this.passwordProgressMessage = "密碼安全度: 強";
      } else if (this.middleReg.test(this.password)) {
        this.passwordProgressColor = "warning";
        this.passwordProgressMessage = "密碼安全度: 中";
      } else if (this.lowReg.test(this.password)) {
        this.passwordProgressColor = "error";
        this.passwordProgressMessage = "密碼安全度: 弱";
      }
      return Math.min(100, this.password.length * 13);
    },
    confirmPasswordProgress() {
      if (this.confirmPassword.length < 6) {
        this.confirmPasswordProgressMessage = "";
      }
      console.log("confirmPasswordProgress");
      if (this.highReg.test(this.confirmPassword)) {
        this.confirmPasswordProgressColor = "success";
        this.confirmPasswordProgressMessage = "密碼安全度: 強";
      } else if (this.middleReg.test(this.confirmPassword)) {
        this.confirmPasswordProgressColor = "warning";
        this.confirmPasswordProgressMessage = "密碼安全度: 中";
      } else if (this.lowReg.test(this.confirmPassword)) {
        console.log("密碼安全度: 弱");
        this.confirmPasswordProgressColor = "error";
        this.confirmPasswordProgressMessage = "密碼安全度: 弱";
      }
      return Math.min(100, this.confirmPassword.length * 13);
    },
    text: function() {
      if (this.time == 10 || this.time == 0) {
        this.sendStatus = false;
        return "";
      } else if (this.time < 10 && this.time !== 0) {
        return this.time + "秒後，可重新寄送簡訊";
      }
    }
  },
  methods: {
    mobileChecked(mobile) {
      let formatNumber = "";
      let countryCode = "";
      if (
        this.TWRegular.test(mobile) ||
        this.SGPRegular.test(mobile) ||
        this.MORegular.test(mobile)
      ) {
        if (this.TWRegular.test(mobile)) {
          formatNumber = mobile.replace(/\+/, "").replace(/^0/, "");
          countryCode = 886;
          this.captcha.key = countryCode + formatNumber;
        } else if (this.SGPRegular.test(mobile)) {
          countryCode = 65;
          this.captcha.key = countryCode + mobile;
        } else if (this.MORegular.test(mobile)) {
          countryCode = 853;
          this.captcha.key = countryCode + mobile;
        }
        this.mobileStatus = true;
      } else {
        this.mobileStatus = false;
      }
    },
    async validatePassword() {
      // if (this.$store.state.status.step == 4) {
      let account = {
        companyId: this.$store.state.companyId,
        userId: this.userId,
        password: this.password,
        key: this.captcha.key,
        value: this.captcha.value
      };
      try {
        let resp = await postRestPasswordAsync(account);
        if (resp.data == true && resp.status == 200) {
          this.loginAccount();
        }
      } catch (error) {
        console.log("error", error);
      } finally {
      }
    },
    async validateCaptcha() {
      if (this.captcha.value.length == 6) {
        try {
          let resp = await postCaptchaValidateAsync({
            key: this.captcha.key,
            value: this.captcha.value
          });
          if (resp.data == false && resp.status == 200) {
            this.$refs.form.reset();
            this.captchaMessage = this.$t("message.SMS.error");
          } else if (resp.data == true) {
            this.validate = true;
          }
        } catch (error) {
          console.log("error", error);
        }
      }
    },
    async sendSMS() {
      if (this.mobileStatus == true) {
        try {
          let resp = await postCaptchaAsync({
            key: this.captcha.key
          });
          this.respStatus = resp.data.message;
          if (this.respStatus == "SUCCESS") {
            this.sendStatus = true;
            this.time = 10;
            this.timer();
          }
        } catch (error) {
          connsole.log(error);
        }
      }
    },
    timer() {
      if (this.time > 0) {
        this.time--;
        setTimeout(this.timer, 1000);
      } else if (this.time == 0) {
        this.sendStatus = false;
        // this.respStatus = "";
      }
    },
    async loginAccount() {
      let returnUrl = this.getQueryVariable("ReturnUrl");
      console.log("returnUrl", returnUrl);
      // const url = new URL(window.location.href);
      // let decodeUri = new URL(decodeURIComponent(url)).searchParams;
      // let redirect_uri = decodeUri.get("redirect_uri");
      let account = {
        companyId: this.$store.state.companyId,
        userId: this.userId,
        password: this.password,
        returnUrl: returnUrl
      };
      try {
        let resp = await postLoginAsync(account);
        if (resp.data.isOk == true && resp.status == 200) {
          window.location = resp.data.redirectUrl;
        }
      } catch (error) {
        console.log(error);
      }
    }
  }
};
</script>
<style scoped>
/* .border {
  border: 1px solid #a8adb1 !important;
  border-radius: 5px;
}
.v-text-field--outlined >>> fieldset {
  border-color: white;
} */
</style>