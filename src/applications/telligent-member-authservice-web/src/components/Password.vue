<template>
  <v-card-text>
    <p class="text-h5 text--primary text-center">
      <!-- 2/4/5 -->
      <strong>{{$store.state.status.step==2?$t('password.register'):4?$t('password.login'):$t('password.set')}}</strong>
    </p>
    <p class="text-center">
      <strong>{{$store.state.status.step==2?$t('register.passwordContent'):4?$t('password.login'):$t('register.set')}}</strong>
    </p>
    <!-- <p>{{$t('validation.SMS')}}{{'+'+phoneNumber.number}}</p> -->
    <v-form ref="form" lazy-validation>
      <v-text-field
        v-if="$store.state.status.step==2"
        v-model="password"
        label="密碼"
        placeholder="請輸入密碼"
        :append-icon="value ? 'mdi-eye' : 'mdi-eye-off'"
        :type="value ? 'text' : 'password'"
        @click:append="() => (value = !value)"
        @input="progressStatus=false;"
        prepend-inner-icon="mdi-lock"
        autocomplete="off"
        autofocus
        color="rgb(255, 90, 110)"
        :rules="[rules.required(password)]"
        hide-details
        :loading="!progressStatus"
      >
        <template v-slot:progress>
          <v-progress-linear :value="passwordProgress" :color="progressColor" absolute height="7"></v-progress-linear>
        </template>
      </v-text-field>
      <v-text-field
        v-if="$store.state.status.step==2"
        v-model="confirmPassword"
        label="密碼確認"
        :append-icon="rePasswordValue ? 'mdi-eye' : 'mdi-eye-off'"
        @click:append="() => (rePasswordValue = !rePasswordValue)"
        @input="confirmProgressStatus=false"
        placeholder="請再次輸入密碼"
        prepend-inner-icon="mdi-lock"
        :type="rePasswordValue ? 'text' : 'password'"
        autocomplete="off"
        :rules="[rules.required(confirmPassword),rules.validateSame(password,confirmPassword)]"
        color="rgb(255, 90, 110)"
        :loading="!confirmProgressStatus"
      >
        <template v-slot:progress>
          <v-progress-linear :value="rePasswordProgress" color="red" absolute height="7"></v-progress-linear>
        </template>
      </v-text-field>
      <v-text-field
        v-model="account.userId"
        v-if="$store.state.status.step==4"
        label="使用者帳號"
        prepend-icon="mdi-account-circle"
        disabled
        value
        color="rgb(255, 90, 110)"
      ></v-text-field>
      <v-text-field
        v-model="password"
        v-if="$store.state.status.step==4"
        ref="password"
        label="密碼"
        prepend-icon="mdi-lock"
        :type="'password'"
        value
        autocomplete="off"
        color="rgb(255, 90, 110)"
        :rules="rulesPassword"
      ></v-text-field>
      <v-text-field
        v-if="$store.state.status.step==4"
        ref="verificationCode"
        label="驗證碼"
        prepend-icon="mdi-shield-check"
        value
        autocomplete="off"
        v-model="verificationCode"
        color="rgb(255, 90, 110)"
        :rules="rulesVerificationCode"
        append-icon="mdi-refresh"
        @click:append="refreshCode()"
        maxlength="4"
      >
        <template v-slot:append-outer>
          <div class="verificationCode">
            <verificationCode :identifyCode="identifyCode" />
          </div>
        </template>
      </v-text-field>
    </v-form>
    <div class="d-flex justify-center">
      <v-btn
        class="white--text primary ma-1"
        color="rgb(255, 90, 110)"
        @click="validatePassword"
      >{{ $t(buttonText)}}</v-btn>
    </div>
  </v-card-text>
</template>

<script>
import rules from "@/plugins/rules";
import { postLoginAsync } from "@/apis/authApi";

export default {
  data() {
    return {
      highReg: /^(?=.*\d)(?=.*[a-zA-Z])(?=.*\W).{8}$/,
      middleReg: /.*[A-Z.a-z]+.*[0-9]+|.*[0-9]+.*[A-Z.a-z]+.*/,
      lowReg: /[A-Za-z0-9]{6}.*/,
      progressStatus: true,
      confirmProgressStatus: true,
      progressColor: "grey",
      reProgressColor: "grey",
      rules: rules,
      password: "",
      confirmPassword: "",
      validate: true,
      value: false,
      rePasswordValue: false,
      sendStatus: false,
      time: 60,
      captcha: {
        key: "",
        value: ""
      },
      step: null,
      errorMessage: "",
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
  props: {
    phoneNumber: Object,
    account: Object
  },
  mounted() {
    this.refreshCode();
  },
  computed: {
    passwordProgress() {
      if (this.password.length < 6) {
        this.progressColor = "grey";
      }
      if (this.highReg.test(this.password)) {
        this.progressColor = "success";
      } else if (this.middleReg.test(this.password)) {
        this.progressColor = "warning";
      } else if (this.lowReg.test(this.password)) {
        this.progressColor = "error";
      }
      return Math.min(100, this.password.length * 13);
    },
    rePasswordProgress() {
      if (this.highReg.test(this.confirmPassword)) {
        this.reProgressColor = "success";
      } else if (this.middleReg.test(this.confirmPassword)) {
        this.reProgressColor = "warning";
      } else if (this.lowReg.test(this.confirmPassword)) {
        this.reProgressColor = "error";
      }
      return Math.min(100, this.confirmPassword.length * 13);
    },
    buttonText() {
      if (this.$store.state.status.step == 4) {
        return "btn.login";
      } else if (this.$store.state.status.step == 2) {
        return "btn.nextStep";
      }
    }
  },
  methods: {
    async validatePassword() {
      if (this.$store.state.status.step == 4) {
        await this.login();
      } else if (this.$store.state.status.step == 2) {
        let password = this.password;
        this.$emit("accountInfo", password);
        this.$store.commit("setStep", 3);
      }
    },
    randomNum(min, max) {
      return Math.floor(Math.random() * (max - min) + min);
    },
    refreshCode() {
      this.verificationCode = "";
      this.identifyCode = "";
      this.makeCode(4);
    },
    makeCode(l) {
      for (let i = 0; i < l; i++) {
        this.identifyCode += this.randomNum(0, 9);
      }
    },
    async login() {
      let returnUrl = this.getQueryVariable("ReturnUrl");
      let account = {
        companyId: this.$store.state.companyId,
        userId: this.account.userId,
        password: this.password,
        returnUrl: returnUrl
      };

      try {
        let resp = await postLoginAsync(account);
        if (
          resp.data.isOk == true &&
          !resp.data.returnUrl &&
          resp.status == 200
        ) {
          console.log("returnUrl", resp.data.redirectUrl);
          alert("loginTure")
          window.location = resp.data.redirectUrl;
          return true;
        }
      } catch (error) {
        console.log(error);
      }
    }
  },
  components: {
    ThirdParty: () => import("@/components/ThirdParty"),
    VerificationCode: () => import("@/components/VerificationCode")
  }
};
</script>