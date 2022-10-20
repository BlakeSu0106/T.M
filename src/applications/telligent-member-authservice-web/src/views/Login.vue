<template>
  <v-container fluid fill-height class="d-flex justify-center">
    <v-img
      class="mr-4"
      src="https://www.telexpress.com/Images/resource/banner_telligent.jpg"
      max-width="900"
    ></v-img>
    <v-card min-width="400" class="pa-10">
      <v-card-title class="d-flex justify-center">
        <a :href="baseUrl">
          <v-img src="https://www.telexpress.com/images/logo_c.svg" max-width="300"></v-img>
        </a>
      </v-card-title>
      <v-card-text v-if="$store.state.status.step==0" class="text-center">
        <p class="text-h5 text--primary">會員登入 / 註冊</p>
        <v-form ref="form" lazy-validation>
          <v-text-field
            v-model="phoneNumber.input"
            v-if="$store.state.status.registerType==2||$store.state.status.pageType==1"
            outlined
            autofocus
            :label="$t('placeholder.phone')"
            prepend-inner-icon="mdi-cellphone"
            :rules="[rules.required(phoneNumber.input)]"
            @keydown.enter.prevent="accountChecked"
          ></v-text-field>
          <v-text-field
            v-model="account.userId"
            v-if="$store.state.status.registerType==0&&$store.state.status.pageType==0||$store.state.status.registerType==1&&$store.state.status.pageType==0"
            outlined
            autofocus
            :label="inputLabel"
            :prepend-inner-icon="$store.state.status.registerType==0?'mdi-account':1?'mdi-email':'mdi-account'"
            :rules="[rules.required(account.userId)]"
            @keydown.enter.prevent="accountChecked"
          ></v-text-field>
          <v-btn
            block
            large
            class="primary my-2"
            @click="accountChecked"
            @keyup.enter="accountChecked"
          >下一步</v-btn>
          <a v-if="$store.state.status.loginType==1" @click="$store.commit('setStep',5)">忘記密碼</a>
        </v-form>
      </v-card-text>
      <Verification :phoneNumber="phoneNumber" v-if="$store.state.status.step==1" />
      <Password
        :account="account"
        v-if="$store.state.status.step==2||$store.state.status.step==4"
        @accountInfo="updateAccountInfo"
      />
      <Restpassword v-if="$store.state.status.step==5" />
      <Register
        v-if="$store.state.status.registerStatus==0&&$store.state.status.step==3"
        :account="account"
        :phoneNumber="phoneNumber"
      />
      <ThirdParty />
      <!-- <v-card-actions></v-card-actions> -->
    </v-card>
  </v-container>
</template>

<script>
import userManager from "@/auth/authService.js";
import { config } from "../config.js";
import Verification from "@/components/Verification";
import Password from "@/components/Password";
import Restpassword from "@/components/Restpassword";
import Register from "@/components/Register";
import ThirdParty from "@/components/ThirdParty";
import { postValidatedAccountAsync } from "@/apis/authApi";
import rules from "@/plugins/rules";
import { getChannelSettingAsync } from "@/apis/channelSettingApi";
import { getThirdPartyId } from "@/apis/configApi";
export default {
  data: function() {
    return {
      baseUrl: window.location.href,
      btnStatus: false,
      email: "",
      account: {},
      rules: rules,
      /**registerType 帳戶輸入方法 0:通用(手機/email),1:email,2:手機 */
      registerType: this.$store.state.status.registerType,
      /**loginType 登入方法 0:快速登入,1:一般登入*/
      loginType: this.$store.state.status.loginType,
      /**pageType 0:輸入帳號(email),1:輸入手機*/
      pageType: this.$store.state.status.pageType,
      /**step 0:Home,1:SMS,2:RegisterPassword,3:RegisterUserInfo,4:login,5:ForgotPassword*/
      step: this.$store.state.status.step,
      /**registerStatus 0:false,1:true*/
      registerStatus: this.$store.state.status.registerStatus,
      companyId: "33104D76-30BE-4E36-95F8-7CD5A4BA6190",
      channelId: "0efa73ee-9ad5-11ec-a1e2-42010a29f03c",
      phoneNumber: {
        input: null, //  raw data
        number: "", //substr
        valid: false,
        countryCode: "", //
        captchaKey: "", // countryCode+number
        formattedNumber: "" //(+countryCode)+number
      },
      verifyCode: null,
      phone: null,
      userInfo: {},
      message: "",
      password: ""
    };
  },
  async mounted() {},
  methods: {
    async validatedAccount(accountType) {
      this.setAccountInfo(accountType);
      try {
        let resp = await postValidatedAccountAsync(this.account);
        if (resp.data.isExist) {
          this.$store.commit("setRegisterStatus", 1);
        } else if (!resp.data.isExist) {
          this.$store.commit("setRegisterStatus", 1);
        }
      } catch (error) {
        console.log("error", error);
      }
      // if (resp.data == false && resp.status == 200) {
      //   this.$store.commit("setRegisterStatus", 0);
      // } else if (resp.status == 200) {
      // }
    },
    async accountChecked() {
      const TWRegular = /^09[0-9]{8}$/;
      const SGPRegular = /^[89][0-9]{7}$/;
      const MORegular = /^6[0-9]{7}$/;
      if (
        TWRegular.test(this.phoneNumber.input || this.account.userId) ||
        SGPRegular.test(this.phoneNumber.input || this.account.userId) ||
        MORegular.test(this.phoneNumber.input || this.account.userId)
      ) {
        if (!this.phoneNumber.input) {
          this.phoneNumber.input = this.account.userId;
        }
        if (
          this.$store.state.status.registerType == 0 ||
          this.$store.state.status.registerType == 2
        ) {
          await this.validatedAccount(2);
        }
        if (TWRegular.test(this.phoneNumber.input || this.account.userId)) {
          this.phoneNumber.number = this.phoneNumber.input
            .replace(/\+/, "")
            .replace(/^0/, "");
          this.phoneNumber.countryCode = 886;
          this.phoneNumber.formattedNumber =
            "+(" + this.phoneNumber.countryCode + ")" + this.phoneNumber.number;
          this.phoneNumber.captchaKey =
            this.phoneNumber.countryCode + this.phoneNumber.number;
        } else if (
          SGPRegular.test(this.phoneNumber.input || this.account.userId)
        ) {
          this.phoneNumber.countryCode = 65;
          this.phoneNumber.number = this.phoneNumber.input;
          this.phoneNumber.formattedNumber =
            "+(" + this.phoneNumber.countryCode + ")" + this.phoneNumber.input;
          this.phoneNumber.captchaKey =
            this.phoneNumber.countryCode + this.phoneNumber.input;
        } else if (
          MORegular.test(this.phoneNumber.input || this.account.userId)
        ) {
          this.phoneNumber.countryCode = 853;
          this.phoneNumber.number = this.phoneNumber.input;
          this.phoneNumber.formattedNumber =
            "+(" + this.phoneNumber.countryCode + ")" + this.phoneNumber.input;
          this.phoneNumber.captchaKey =
            this.phoneNumber.countryCode + this.phoneNumber.input;
        }
        console.log("middle", this.$store.state.status.registerStatus);
        if (this.$store.state.status.registerStatus == 0) {
          this.$store.commit("setSMSType", 1);
        } else if (this.$store.state.status.registerStatus == 1) {
          console.log("registerStatus==1");
          if (this.$store.state.status.loginType == 0) {
            this.$store.commit("setSMSType", 0);
          } else if (this.$store.state.status.loginType == 1) {
            console.log("loginType==1");
            this.$store.commit("setSMSType", 2);
          }
        }
        this.$store.commit("setStep", 1);
      } else if (
        /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(
          this.account.userId
        )
      ) {
        this.validatedAccount(1);
        this.$store.commit("setPageType", 1);
        // this.$store.commit("setRegisterType", 0);
        // this.$refs.form.reset();
      }
    },
    phoneNumberChecked(formattedNumber, { number, valid, country }) {
      if (valid) console.log("number", number);
      this.phoneNumber.input = number.input;
      this.phoneNumber.formattedNumber = number.significant;
      this.phoneNumber.valid = valid;
      this.phoneNumber.countryCode = country.dialCode;
      console.log("country", country);
    },
    porgotPassword() {
      this.$store.commit("setSMSStep", 1);
      this.$store.commit("setStep", 5);
      this.$forceUpadte();
    },
    updateAccountInfo(password) {
      this.account.password = password;
    },
    setAccountInfo(accountType) {
      // if (this.$store.state.status.registerType == 2) {
      // this.account.userId = this.phoneNumber.input;
      // }
      // this.registerType == 2 ? this.phoneNumber.input : this.account.userId;
      this.account.companyId = this.$store.state.companyId;
      this.account.accountType = accountType;
    },
    async intiChannel() {
      let locale = new URL(window.location);
      let decodeUri = new URL(decodeURIComponent(locale)).searchParams;

      // 透過物件的解構賦值，取出 URL 物件的屬性值
      let companyId = decodeUri.get("companyId");
      this.$store.commit("setCompanyId", companyId);
      let channelId = decodeUri.get("channelId");
      let resp = await getChannelSettingAsync(channelId);
      this.$store.commit("setChannel", resp.data);
    },
    async initThirdPartyChannel() {
      // console.log("initThirdPartyChannel");
      // let params = new URL(window.location.href).searchParams;
      // let companyId = params.get("companyid");
      try {
        let resp = await getThirdPartyId(this.$store.state.companyId, 1);
        console.log("resp", resp);
        this.$store.commit("setThirdPartyChannelInfo", resp.data);
      } catch (error) {
        console.log(error);
      }
    },
    async init() {
      await this.intiChannel();
      await this.initThirdPartyChannel();
    }
  },
  components: {
    Verification,
    ThirdParty,
    Register,
    Password,
    Restpassword
  },
  computed: {
    phoneMessage() {
      if (!this.phoneNumber.valid && this.phoneNumber.input.length > 0) {
        this.message = this.$t("phone.error");
        return this.message;
      }
    },
    inputLabel() {
      if (this.$store.state.status.registerType == 0) {
        return this.$t("placeholder.normal");
      } else if (this.$store.state.status.registerType == 1) {
        return this.$t("placeholder.email");
      } else if (this.$store.state.status.registerType == 2) {
        return this.$t("placeholder.phone");
      }
    }
  },
  async created() {
    // let vi = this;
    // try {
    //   let user = await userManager.getUser();
    //   if (!user) {
    //     userManager.signinRedirect({
    //       extraQueryParams: {
    //         companyId: "71ff12dd-dcc9-11ec-a719-0242ac170002",
    //         channelId: "7bcc25c5-dd9a-11ec-a719-0242ac170002"
    //       }
    //     });
    //   } else {
    //     vi.setUser(user);
    //     vi.$router.push("/");
    //   }
    // } catch (err) {
    //   document.location = config.auth.post_logout_redirect_uri;
    // }
    await this.init();
  }
};
</script>
<style scoped>
/* .v-text-field .v-input__control .v-input__slot {
  min-height: 40px !important;
  display: flex !important;
  align-items: center !important;
} */
</style>